<template>
  <div class="dashboard-container">
    <el-row :gutter="20">
      <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
        <el-card class="header-card">
          <div class="dashboard-header">
            <h2>系统运行监控看板</h2>
            <div class="dashboard-actions">
              <span class="last-update">最后更新: {{ lastUpdateTime }}</span>
              <el-button type="primary" size="small" @click="refreshData">
                <el-icon><Refresh /></el-icon>
                刷新数据
              </el-button>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <el-row :gutter="20" class="data-row">
      <!-- 数据概览卡片 -->
      <el-col
        :xs="24"
        :sm="12"
        :md="12"
        :lg="6"
        :xl="6"
        v-for="(item, index) in statCards"
        :key="index"
      >
        <el-card class="stat-card" :body-style="{ padding: '10px' }">
          <div class="stat-content">
            <el-icon :size="36" :class="item.iconClass">
              <component :is="item.icon" />
            </el-icon>
            <div class="stat-info">
              <div class="stat-title">{{ item.title }}</div>
              <div class="stat-value">{{ item.value }}</div>
              <div class="stat-change" :class="item.trend">
                <el-icon v-if="item.trend === 'up'"><ArrowUp /></el-icon>
                <el-icon v-else><ArrowDown /></el-icon>
                {{ item.change }}
              </div>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <el-row :gutter="20" class="chart-row">
      <!-- 折线图 -->
      <el-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12">
        <el-card class="chart-card">
          <template #header>
            <div class="card-header">
              <span>设备运行状态趋势</span>
              <el-dropdown>
                <span class="el-dropdown-link">
                  {{ timeRangeText }}
                  <el-icon class="el-icon--right"><arrow-down /></el-icon>
                </span>
                <template #dropdown>
                  <el-dropdown-menu>
                    <el-dropdown-item @click="changeTimeRange('day')">
                      今日
                    </el-dropdown-item>
                    <el-dropdown-item @click="changeTimeRange('week')">
                      本周
                    </el-dropdown-item>
                    <el-dropdown-item @click="changeTimeRange('month')">
                      本月
                    </el-dropdown-item>
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
            </div>
          </template>
          <div class="chart-container">
            <div ref="lineChartRef" class="chart"></div>
          </div>
        </el-card>
      </el-col>

      <!-- 饼图 -->
      <el-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12">
        <el-card class="chart-card">
          <template #header>
            <div class="card-header">
              <span>设备类型分布</span>
            </div>
          </template>
          <div class="chart-container">
            <div ref="pieChartRef" class="chart"></div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <el-row :gutter="20" class="chart-row">
      <!-- 曲线图 -->
      <el-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12">
        <el-card class="chart-card">
          <template #header>
            <div class="card-header">
              <span>水质参数监测</span>
            </div>
          </template>
          <div class="chart-container">
            <div ref="areaChartRef" class="chart"></div>
          </div>
        </el-card>
      </el-col>

      <!-- 设备列表 -->
      <el-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12">
        <el-card class="list-card">
          <template #header>
            <div class="card-header">
              <span>设备状态列表</span>
              <el-input
                v-model="searchQuery"
                placeholder="搜索设备"
                prefix-icon="Search"
                clearable
                size="small"
                style="width: 200px"
              />
            </div>
          </template>
          <el-table
            :data="filteredDeviceList"
            stripe
            style="width: 100%"
            height="300"
          >
            <el-table-column prop="name" label="设备名称" />
            <el-table-column prop="type" label="设备类型" />
            <el-table-column prop="location" label="安装位置" />
            <el-table-column prop="status" label="状态">
              <template #default="scope">
                <el-tag :type="getStatusType(scope.row.status)">
                  {{ scope.row.status }}
                </el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="lastUpdate" label="最后更新" />
          </el-table>
          <div class="pagination-container">
            <el-pagination
              background
              layout="prev, pager, next"
              :total="deviceList.length"
              :page-size="5"
              @current-change="handleCurrentChange"
            />
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts">
import {
  defineComponent,
  ref,
  onMounted,
  onBeforeUnmount,
  computed,
} from "vue";
import * as echarts from "echarts";
import {
  Refresh,
  ArrowUp,
  ArrowDown,
  Monitor,
  Cpu,
  Connection,
  DataAnalysis,
  Search,
} from "@element-plus/icons-vue";

export default defineComponent({
  name: "Dashboard",
  components: {
    Refresh,
    ArrowUp,
    ArrowDown,
    Monitor,
    Cpu,
    Connection,
    DataAnalysis,
    Search,
  },
  setup() {
    // 图表引用
    const lineChartRef = ref<HTMLElement | null>(null);
    const pieChartRef = ref<HTMLElement | null>(null);
    const areaChartRef = ref<HTMLElement | null>(null);

    // 图表实例
    let lineChart: echarts.ECharts | null = null;
    let pieChart: echarts.ECharts | null = null;
    let areaChart: echarts.ECharts | null = null;

    // 数据状态
    const lastUpdateTime = ref(new Date().toLocaleString());
    const timeRange = ref("day");
    const timeRangeText = computed(() => {
      const map: Record<string, string> = {
        day: "今日",
        week: "本周",
        month: "本月",
      };
      return map[timeRange.value];
    });

    // 统计卡片数据
    const statCards = ref([
      {
        title: "设备总数",
        value: "42",
        change: "2.5%",
        trend: "up",
        icon: "Monitor",
        iconClass: "blue-icon",
      },
      {
        title: "在线设备",
        value: "36",
        change: "1.2%",
        trend: "up",
        icon: "Connection",
        iconClass: "green-icon",
      },
      {
        title: "告警设备",
        value: "3",
        change: "0.5%",
        trend: "down",
        icon: "Cpu",
        iconClass: "red-icon",
      },
      {
        title: "数据点数",
        value: "1,286",
        change: "12.8%",
        trend: "up",
        icon: "DataAnalysis",
        iconClass: "purple-icon",
      },
    ]);

    // 设备列表数据
    const deviceList = ref([
      {
        name: "消毒剂搅拌机",
        type: "搅拌设备",
        location: "一号处理区",
        status: "正常",
        lastUpdate: "2023-05-15 10:30",
      },
      {
        name: "调节池提升泵",
        type: "水泵",
        location: "进水区",
        status: "正常",
        lastUpdate: "2023-05-15 10:28",
      },
      {
        name: "污泥回流泵",
        type: "水泵",
        location: "二号处理区",
        status: "正常",
        lastUpdate: "2023-05-15 10:25",
      },
      {
        name: "鼓风机",
        type: "风机",
        location: "曝气区",
        status: "告警",
        lastUpdate: "2023-05-15 10:15",
      },
      {
        name: "PH值检测仪",
        type: "检测设备",
        location: "出水区",
        status: "正常",
        lastUpdate: "2023-05-15 10:10",
      },
      {
        name: "溶解氧检测仪",
        type: "检测设备",
        location: "曝气区",
        status: "正常",
        lastUpdate: "2023-05-15 10:05",
      },
      {
        name: "流量计",
        type: "检测设备",
        location: "进水区",
        status: "离线",
        lastUpdate: "2023-05-15 09:50",
      },
      {
        name: "紫外线消毒设备",
        type: "消毒设备",
        location: "出水区",
        status: "正常",
        lastUpdate: "2023-05-15 09:45",
      },
      {
        name: "污泥浓度计",
        type: "检测设备",
        location: "污泥区",
        status: "告警",
        lastUpdate: "2023-05-15 09:40",
      },
      {
        name: "加药装置",
        type: "投加设备",
        location: "混凝区",
        status: "正常",
        lastUpdate: "2023-05-15 09:35",
      },
    ]);

    // 搜索和分页
    const searchQuery = ref("");
    const currentPage = ref(1);

    const filteredDeviceList = computed(() => {
      const query = searchQuery.value.toLowerCase();
      if (!query) {
        return deviceList.value.slice(
          (currentPage.value - 1) * 5,
          currentPage.value * 5
        );
      }

      return deviceList.value.filter(
        (device) =>
          device.name.toLowerCase().includes(query) ||
          device.type.toLowerCase().includes(query) ||
          device.location.toLowerCase().includes(query) ||
          device.status.toLowerCase().includes(query)
      );
    });

    // 方法
    const handleCurrentChange = (val: number) => {
      currentPage.value = val;
    };

    const getStatusType = (status: string) => {
      const map: Record<string, string> = {
        正常: "success",
        告警: "danger",
        离线: "info",
      };
      return map[status] || "info";
    };

    const changeTimeRange = (range: string) => {
      timeRange.value = range;
      initCharts();
    };

    // 初始化图表
    const initLineChart = () => {
      if (!lineChartRef.value) return;

      lineChart = echarts.init(lineChartRef.value);

      // 根据时间范围生成不同的数据
      const days =
        timeRange.value === "day" ? 24 : timeRange.value === "week" ? 7 : 30;

      const xData = Array.from({ length: days }, (_, i) => {
        if (timeRange.value === "day") return `${i}:00`;
        if (timeRange.value === "week")
          return `周${["日", "一", "二", "三", "四", "五", "六"][i]}`;
        return `${i + 1}日`;
      });

      const generateData = () =>
        Array.from({ length: days }, () => Math.floor(Math.random() * 100));

      const option = {
        tooltip: {
          trigger: "axis",
        },
        legend: {
          data: ["运行设备数", "告警次数", "数据传输量"],
        },
        grid: {
          left: "3%",
          right: "4%",
          bottom: "3%",
          containLabel: true,
        },
        xAxis: {
          type: "category",
          boundaryGap: false,
          data: xData,
        },
        yAxis: {
          type: "value",
        },
        series: [
          {
            name: "运行设备数",
            type: "line",
            data: generateData(),
            smooth: true,
            lineStyle: {
              width: 3,
              shadowColor: "rgba(0,0,0,0.3)",
              shadowBlur: 10,
              shadowOffsetY: 8,
            },
          },
          {
            name: "告警次数",
            type: "line",
            data: generateData().map((v) => v * 0.3),
            smooth: true,
            lineStyle: {
              width: 3,
              shadowColor: "rgba(0,0,0,0.3)",
              shadowBlur: 10,
              shadowOffsetY: 8,
            },
          },
          {
            name: "数据传输量",
            type: "line",
            data: generateData().map((v) => v * 1.5),
            smooth: true,
            lineStyle: {
              width: 3,
              shadowColor: "rgba(0,0,0,0.3)",
              shadowBlur: 10,
              shadowOffsetY: 8,
            },
          },
        ],
      };

      lineChart.setOption(option);
    };

    const initPieChart = () => {
      if (!pieChartRef.value) return;

      pieChart = echarts.init(pieChartRef.value);

      const option = {
        tooltip: {
          trigger: "item",
          formatter: "{a} <br/>{b}: {c} ({d}%)",
        },
        legend: {
          orient: "vertical",
          left: 10,
          data: [
            "水泵",
            "风机",
            "检测设备",
            "消毒设备",
            "投加设备",
            "其他设备",
          ],
        },
        series: [
          {
            name: "设备类型",
            type: "pie",
            radius: ["50%", "70%"],
            avoidLabelOverlap: false,
            itemStyle: {
              borderRadius: 10,
              borderColor: "#fff",
              borderWidth: 2,
            },
            label: {
              show: false,
              position: "center",
            },
            emphasis: {
              label: {
                show: true,
                fontSize: "18",
                fontWeight: "bold",
              },
            },
            labelLine: {
              show: false,
            },
            data: [
              { value: 12, name: "水泵" },
              { value: 8, name: "风机" },
              { value: 15, name: "检测设备" },
              { value: 5, name: "消毒设备" },
              { value: 7, name: "投加设备" },
              { value: 3, name: "其他设备" },
            ],
          },
        ],
      };

      pieChart.setOption(option);
    };

    const initAreaChart = () => {
      if (!areaChartRef.value) return;

      areaChart = echarts.init(areaChartRef.value);

      const generateData = () => {
        return Array.from({ length: 24 }, () => {
          return (Math.random() * 10).toFixed(2);
        });
      };

      const option = {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            type: "cross",
            label: {
              backgroundColor: "#6a7985",
            },
          },
        },
        legend: {
          data: ["PH值", "溶解氧", "浊度", "氨氮"],
        },
        grid: {
          left: "3%",
          right: "4%",
          bottom: "3%",
          containLabel: true,
        },
        xAxis: [
          {
            type: "category",
            boundaryGap: false,
            data: Array.from({ length: 24 }, (_, i) => `${i}:00`),
          },
        ],
        yAxis: [
          {
            type: "value",
          },
        ],
        series: [
          {
            name: "PH值",
            type: "line",
            stack: "总量",
            areaStyle: {},
            emphasis: {
              focus: "series",
            },
            data: generateData().map((v) => parseFloat(v) + 7),
          },
          {
            name: "溶解氧",
            type: "line",
            stack: "总量",
            areaStyle: {},
            emphasis: {
              focus: "series",
            },
            data: generateData(),
          },
          {
            name: "浊度",
            type: "line",
            stack: "总量",
            areaStyle: {},
            emphasis: {
              focus: "series",
            },
            data: generateData().map((v) => parseFloat(v) * 0.5),
          },
          {
            name: "氨氮",
            type: "line",
            stack: "总量",
            areaStyle: {},
            emphasis: {
              focus: "series",
            },
            data: generateData().map((v) => parseFloat(v) * 0.2),
          },
        ],
      };

      areaChart.setOption(option);
    };

    const initCharts = () => {
      initLineChart();
      initPieChart();
      initAreaChart();
    };

    // 刷新数据
    const refreshData = () => {
      // 更新统计卡片数据
      statCards.value.forEach((card) => {
        const randomChange = (Math.random() * 5).toFixed(1);
        card.change = `${randomChange}%`;
        card.trend = Math.random() > 0.5 ? "up" : "down";
      });

      // 更新设备列表
      deviceList.value.forEach((device) => {
        if (Math.random() > 0.8) {
          device.status = ["正常", "告警", "离线"][
            Math.floor(Math.random() * 3)
          ];
          device.lastUpdate = new Date().toLocaleString();
        }
      });

      // 更新图表
      initCharts();

      // 更新时间
      lastUpdateTime.value = new Date().toLocaleString();
    };

    // 自动刷新
    let refreshTimer: number | null = null;

    onMounted(() => {
      initCharts();

      // 窗口大小变化时重新调整图表大小
      window.addEventListener("resize", handleResize);

      // 设置自动刷新
      refreshTimer = window.setInterval(refreshData, 60000); // 每分钟刷新一次
    });

    onBeforeUnmount(() => {
      window.removeEventListener("resize", handleResize);

      if (refreshTimer) {
        clearInterval(refreshTimer);
      }

      // 销毁图表实例
      if (lineChart) lineChart.dispose();
      if (pieChart) pieChart.dispose();
      if (areaChart) areaChart.dispose();
    });

    const handleResize = () => {
      lineChart?.resize();
      pieChart?.resize();
      areaChart?.resize();
    };

    return {
      lastUpdateTime,
      timeRange,
      timeRangeText,
      statCards,
      deviceList,
      filteredDeviceList,
      searchQuery,
      lineChartRef,
      pieChartRef,
      areaChartRef,
      refreshData,
      changeTimeRange,
      handleCurrentChange,
      getStatusType,
    };
  },
});
</script>

<style scoped>
.dashboard-container {
  padding: 20px;
  height: 100%;
  overflow-y: auto;
}

.dashboard-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.dashboard-actions {
  display: flex;
  align-items: center;
  gap: 15px;
}

.last-update {
  color: #909399;
  font-size: 14px;
}

.data-row,
.chart-row {
  margin-top: 20px;
}

.stat-card {
  height: 120px;
  transition: all 0.3s;
}

.stat-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
}

.stat-content {
  display: flex;
  align-items: center;
  height: 100%;
}

.stat-info {
  margin-left: 15px;
  flex: 1;
}

.stat-title {
  font-size: 14px;
  color: #909399;
}

.stat-value {
  font-size: 24px;
  font-weight: bold;
  margin: 5px 0;
}

.stat-change {
  font-size: 12px;
  display: flex;
  align-items: center;
}

.stat-change.up {
  color: #67c23a;
}

.stat-change.down {
  color: #f56c6c;
}

.blue-icon {
  color: #409eff;
}

.green-icon {
  color: #67c23a;
}

.red-icon {
  color: #f56c6c;
}

.purple-icon {
  color: #9254de;
}

.chart-card,
.list-card {
  height: 400px;
  margin-bottom: 20px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.chart-container {
  height: 320px;
}

.chart {
  width: 100%;
  height: 100%;
}

.pagination-container {
  margin-top: 15px;
  display: flex;
  justify-content: center;
}

.el-dropdown-link {
  cursor: pointer;
  color: #409eff;
  display: flex;
  align-items: center;
}
</style>
