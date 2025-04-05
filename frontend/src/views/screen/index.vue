<script>
import * as echarts from "echarts";

export default {
  name: "Dashboard",
  data() {
    return {
      chart: null,
      pieChart: null,
      flowChart: null,
      h2sChart: null,

      // 基础信息
      basicInfo: {
        projectName: "某污水处理厂",
        location: "江苏省南京市",
        capacity: "50000吨/日",
        processType: "A2O工艺",
        operationTime: "2020-01-01",
      },

      // 运行状态信息
      operationStatus: {
        runningDays: 365,
        totalFlow: "1826.5万吨",
        energyConsumption: "890.3万度",
        alarmCount: 12,
      },

      // 系统公告
      announcements: [
        { id: 1, title: "设备年度检修通知", time: "2024-01-20", level: "重要" },
        {
          id: 2,
          title: "水质在线监测仪表校准",
          time: "2024-01-19",
          level: "普通",
        },
        { id: 3, title: "工艺参数优化调整", time: "2024-01-18", level: "普通" },
      ],

      // 监测指标
      metrics: [
        {
          id: 1,
          name: "进水水位",
          value: "4.9",
          unit: "m",
          color: "#00B7FF",
          label: "当前水位",
        },
        {
          id: 2,
          name: "水温",
          value: "25.6",
          unit: "°C",
          color: "#FF6B6B",
          label: "实时温度",
        },
        {
          id: 3,
          name: "pH值",
          value: "7.16",
          unit: "",
          color: "#47CF73",
          label: "pH值",
        },
        {
          id: 4,
          name: "溶解氧",
          value: "5.69",
          unit: "mg/L",
          color: "#FFB957",
          label: "DO值",
        },
        {
          id: 5,
          name: "COD",
          value: "45.8",
          unit: "mg/L",
          color: "#B47FFF",
          label: "化学需氧量",
        },
        {
          id: 6,
          name: "氨氮",
          value: "2.34",
          unit: "mg/L",
          color: "#FF9F7F",
          label: "NH3-N",
        },
      ],

      // 设备状态
      devices: [
        { id: "pump1", name: "1号进水泵", status: true, runningTime: "168h" },
        { id: "pump2", name: "2号进水泵", status: false, runningTime: "142h" },
        { id: "blower1", name: "1号鼓风机", status: true, runningTime: "196h" },
        { id: "blower2", name: "2号鼓风机", status: true, runningTime: "180h" },
        { id: "mixer1", name: "1号搅拌机", status: true, runningTime: "200h" },
        { id: "valve1", name: "回流阀门", status: true, runningTime: "168h" },
      ],

      // 工单信息
      workOrders: [
        {
          type: "设备维修",
          status: "处理中",
          priority: "高",
          result: "更换轴承",
          time: "2024-01-20 10:30",
        },
        {
          type: "日常巡检",
          status: "已完成",
          priority: "中",
          result: "正常",
          time: "2024-01-20 09:15",
        },
        {
          type: "故障报警",
          status: "待处理",
          priority: "高",
          result: "-",
          time: "2024-01-20 08:45",
        },
        {
          type: "预防性维护",
          status: "已完成",
          priority: "中",
          result: "已校准",
          time: "2024-01-19 16:20",
        },
        {
          type: "水质异常",
          status: "已处理",
          priority: "高",
          result: "已调整",
          time: "2024-01-19 14:30",
        },
      ],

      // 运维记录
      maintenanceRecords: [
        {
          time: "2024-01-20",
          operator: "张工",
          content: "完成1号鼓风机维护保养",
        },
        { time: "2024-01-19", operator: "李工", content: "DO仪表校准" },
        { time: "2024-01-18", operator: "王工", content: "调整污泥回流比" },
        { time: "2024-01-17", operator: "赵工", content: "更换2号水泵轴承" },
      ],

      // 图表配置
      flowChartOption: {
        title: { text: "今日流量趋势", textStyle: { color: "#fff" } },
        tooltip: { trigger: "axis" },
        legend: {
          data: ["进水流量", "出水流量"],
          textStyle: { color: "#fff" },
        },
        xAxis: {
          type: "category",
          data: [
            "00:00",
            "03:00",
            "06:00",
            "09:00",
            "12:00",
            "15:00",
            "18:00",
            "21:00",
          ],
          axisLabel: { color: "#fff" },
        },
        yAxis: {
          type: "value",
          name: "流量(m³/h)",
          axisLabel: { color: "#fff" },
        },
        series: [
          {
            name: "进水流量",
            type: "line",
            smooth: true,
            data: [2100, 1900, 1700, 2300, 2500, 2300, 2400, 2200],
            itemStyle: { color: "#00B7FF" },
          },
          {
            name: "出水流量",
            type: "line",
            smooth: true,
            data: [2000, 1800, 1600, 2200, 2400, 2200, 2300, 2100],
            itemStyle: { color: "#47CF73" },
          },
        ],
      },

      h2sChartOption: {
        title: { text: "硫化氢浓度监测", textStyle: { color: "#fff" } },
        tooltip: { trigger: "axis" },
        xAxis: {
          type: "category",
          data: [
            "00:00",
            "03:00",
            "06:00",
            "09:00",
            "12:00",
            "15:00",
            "18:00",
            "21:00",
          ],
          axisLabel: { color: "#fff" },
        },
        yAxis: {
          type: "value",
          name: "ppm",
          axisLabel: { color: "#fff" },
        },
        series: [
          {
            data: [5, 8, 12, 6, 4, 7, 9, 6],
            type: "line",
            smooth: true,
            areaStyle: {
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                { offset: 0, color: "rgba(255, 107, 107, 0.5)" },
                { offset: 1, color: "rgba(255, 107, 107, 0.1)" },
              ]),
            },
            itemStyle: { color: "#FF6B6B" },
          },
        ],
      },

      pieOption: {
        title: { text: "工单类型分布", textStyle: { color: "#fff" } },
        tooltip: { trigger: "item" },
        legend: {
          orient: "vertical",
          right: 10,
          top: "center",
          textStyle: { color: "#fff" },
        },
        series: [
          {
            name: "工单类型",
            type: "pie",
            radius: ["40%", "70%"],
            avoidLabelOverlap: false,
            itemStyle: {
              borderRadius: 10,
              borderColor: "#fff",
              borderWidth: 2,
            },
            label: { show: false },
            emphasis: {
              label: { show: true, fontSize: 20, fontWeight: "bold" },
            },
            data: [
              { value: 35, name: "设备维修" },
              { value: 25, name: "日常巡检" },
              { value: 20, name: "故障报警" },
              { value: 15, name: "预防性维护" },
              { value: 5, name: "水质异常" },
            ],
          },
        ],
      },
    };
  },
  mounted() {
    this.initCharts();
    // this.startDataPolling();
    window.addEventListener("resize", this.resizeCharts);
  },
  beforeUnmount() {
    this.stopDataPolling();
    window.removeEventListener("resize", this.resizeCharts);
    this.disposeCharts();
  },
  methods: {
    initCharts() {
      this.flowChart = echarts.init(this.$refs.flowChartRef);
      this.flowChart.setOption(this.flowChartOption);

      this.h2sChart = echarts.init(this.$refs.h2sChartRef);
      this.h2sChart.setOption(this.h2sChartOption);

      this.pieChart = echarts.init(this.$refs.pieChartRef);
      this.pieChart.setOption(this.pieOption);
    },
    resizeCharts() {
      this.flowChart?.resize();
      this.h2sChart?.resize();
      this.pieChart?.resize();
    },
    disposeCharts() {
      this.flowChart?.dispose();
      this.h2sChart?.dispose();
      this.pieChart?.dispose();
    },
    startDataPolling() {
      this.pollingTimer = setInterval(this.updateData, 5000);
    },
    stopDataPolling() {
      if (this.pollingTimer) {
        clearInterval(this.pollingTimer);
      }
    },
    getStatusType(status) {
      const statusMap = {
        待处理: "warning",
        处理中: "primary",
        已完成: "success",
        已处理: "success",
      };
      return statusMap[status] || "info";
    },

    getPriorityType(priority) {
      const priorityMap = {
        高: "danger",
        中: "warning",
        低: "info",
      };
      return priorityMap[priority] || "info";
    },
    async updateData() {
      try {
        const response = await fetch("/api/monitor/status");
        const data = await response.json();

        // 更新各类数据
        if (data.metrics) this.updateMetrics(data.metrics);
        if (data.devices) this.updateDevices(data.devices);
        if (data.flowData) this.updateFlowChart(data.flowData);
        if (data.h2sData) this.updateH2sChart(data.h2sData);
        if (data.workOrders) this.updateWorkOrders(data.workOrders);
      } catch (error) {
        console.error("数据更新失败:", error);
      }
    },
    updateMetrics(data) {
      this.metrics.forEach((metric) => {
        if (data[metric.id]) {
          metric.value = data[metric.id].toFixed(2);
        }
      });
    },
    updateDevices(data) {
      this.devices.forEach((device) => {
        if (data[device.id]) {
          device.status = data[device.id].status;
          device.runningTime = data[device.id].runningTime;
        }
      });
    },
    updateFlowChart(data) {
      this.flowChart.setOption({
        series: [
          {
            data: data.inFlow,
          },
          {
            data: data.outFlow,
          },
        ],
      });
    },
    updateH2sChart(data) {
      this.h2sChart.setOption({
        series: [
          {
            data: data.concentration,
          },
        ],
      });
    },
    updateWorkOrders(data) {
      this.workOrders = data;
      const pieData = this.calculatePieData(data);
      this.pieChart.setOption({
        series: [
          {
            data: pieData,
          },
        ],
      });
    },
    calculatePieData(workOrders) {
      const typeCount = {};
      workOrders.forEach((order) => {
        typeCount[order.type] = (typeCount[order.type] || 0) + 1;
      });
      return Object.entries(typeCount).map(([name, value]) => ({
        name,
        value,
      }));
    },
  },
};
</script>

<template>
  <div class="dashboard-container">
    <!-- 炫酷标题部分 -->
    <div class="dashboard-header">
      <div class="title-container">
        <h1>环境监测系统</h1>
        <div class="title-decoration"></div>
      </div>
    </div>

    <!-- 三列布局 -->
    <div class="dashboard-content">
      <!-- 第一列：信息展示 -->
      <div class="content-column">
        <div class="info-block">
          <h3>基础信息</h3>
          <div class="info-content">
            <div class="info-item">
              <span class="info-label">项目名称：</span>
              <span class="info-value">{{ basicInfo.projectName }}</span>
            </div>
            <div class="info-item">
              <span class="info-label">所在地：</span>
              <span class="info-value">{{ basicInfo.location }}</span>
            </div>
            <div class="info-item">
              <span class="info-label">处理能力：</span>
              <span class="info-value">{{ basicInfo.capacity }}</span>
            </div>
            <div class="info-item">
              <span class="info-label">工艺类型：</span>
              <span class="info-value">{{ basicInfo.processType }}</span>
            </div>
            <div class="info-item">
              <span class="info-label">运行时间：</span>
              <span class="info-value">{{ basicInfo.operationTime }}</span>
            </div>
          </div>
        </div>
        <div class="info-block">
          <h3>运行状态</h3>
          <div class="info-content">
            <div class="status-item">
              <div class="status-icon">
                <i class="el-icon-time"></i>
              </div>
              <div class="status-info">
                <div class="status-label">运行天数</div>
                <div class="status-value">
                  {{ operationStatus.runningDays }}天
                </div>
              </div>
            </div>
            <div class="status-item">
              <div class="status-icon">
                <i class="el-icon-water-cup"></i>
              </div>
              <div class="status-info">
                <div class="status-label">累计处理水量</div>
                <div class="status-value">{{ operationStatus.totalFlow }}</div>
              </div>
            </div>
            <div class="status-item">
              <div class="status-icon">
                <i class="el-icon-lightning"></i>
              </div>
              <div class="status-info">
                <div class="status-label">累计耗电量</div>
                <div class="status-value">
                  {{ operationStatus.energyConsumption }}
                </div>
              </div>
            </div>
            <div class="status-item">
              <div class="status-icon warning">
                <i class="el-icon-warning"></i>
              </div>
              <div class="status-info">
                <div class="status-label">报警次数</div>
                <div class="status-value">{{ operationStatus.alarmCount }}</div>
              </div>
            </div>
          </div>
        </div>
        <div class="info-block">
          <h3>系统公告</h3>
          <div class="info-content">
            <div
              v-for="notice in announcements"
              :key="notice.id"
              class="announcement-item"
            >
              <div class="announcement-title">
                <span class="announcement-level" :class="notice.level">
                  {{ notice.level }}
                </span>
                {{ notice.title }}
              </div>
              <div class="announcement-time">{{ notice.time }}</div>
            </div>
          </div>
        </div>
      </div>

      <!-- 第二列：监测数据 -->
      <div class="content-column">
        <!-- 水位、温度、pH值等指标卡片 -->
        <div class="metric-grid">
          <div class="metric-card" v-for="metric in metrics" :key="metric.id">
            <div class="metric-title">{{ metric.name }}</div>
            <div class="metric-value" :style="{ color: metric.color }">
              {{ metric.value }}
              <span class="metric-unit">{{ metric.unit }}</span>
            </div>
          </div>
        </div>

        <!-- 今日流量图表 -->
        <div class="chart-container">
          <h3>今日流量</h3>
          <div ref="flowChartRef" class="chart"></div>
        </div>

        <!-- 硫化氢浓度图表 -->
        <div class="chart-container">
          <h3>硫化氢浓度</h3>
          <div ref="h2sChartRef" class="chart"></div>
        </div>
      </div>

      <!-- 第三列：工单信息 -->
      <div class="content-column">
        <!-- 工单列表 -->
        <div class="work-order-container">
          <h3>工单信息</h3>
          <el-table
            :data="workOrders"
            style="width: 100%"
            :header-cell-style="{
              background: 'rgba(255, 255, 255, 0.1)',
              color: '#fff',
            }"
            :cell-style="{ background: 'transparent', color: '#fff' }"
          >
            <el-table-column prop="type" label="类型" width="100">
              <template #default="scope">
                <span>{{ scope.row.type }}</span>
              </template>
            </el-table-column>
            <el-table-column prop="status" label="状态" width="80">
              <template #default="scope">
                <el-tag :type="getStatusType(scope.row.status)" size="small">
                  {{ scope.row.status }}
                </el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="priority" label="优先级" width="80">
              <template #default="scope">
                <el-tag
                  :type="getPriorityType(scope.row.priority)"
                  size="small"
                >
                  {{ scope.row.priority }}
                </el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="time" label="时间" />
          </el-table>
        </div>

        <!-- 工单统计饼图 -->
        <div class="pie-container">
          <h3>工单处理情况</h3>
          <div ref="pieChartRef" class="pie-chart"></div>
        </div>

        <!-- 运维记录 -->
        <div class="maintenance-container">
          <h3>运维记录</h3>
          <div class="maintenance-content">运维记录展示区域</div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* ... 原有样式保持不变 ... */

.info-content {
  padding: 10px 0;
}

.info-item {
  display: flex;
  margin-bottom: 12px;
  align-items: center;
}

.info-label {
  width: 100px;
  color: #a8b6c8;
  font-size: 14px;
}

.info-value {
  flex: 1;
  color: #fff;
  font-size: 14px;
}

.status-item {
  display: flex;
  align-items: center;
  margin-bottom: 15px;
  padding: 10px;
  background: rgba(0, 0, 0, 0.2);
  border-radius: 8px;
}

.status-icon {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background: rgba(102, 166, 255, 0.2);
  display: flex;
  align-items: center;
  justify-content: center;
  margin-right: 15px;
  font-size: 20px;
  color: #66a6ff;
}

.status-icon.warning {
  background: rgba(255, 107, 107, 0.2);
  color: #ff6b6b;
}

.status-info {
  flex: 1;
}

.status-label {
  font-size: 14px;
  color: #a8b6c8;
  margin-bottom: 4px;
}

.status-value {
  font-size: 18px;
  font-weight: bold;
  color: #fff;
}

.announcement-list {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.announcement-item {
  padding: 12px;
  background: rgba(0, 0, 0, 0.2);
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.3s;
}

.announcement-item:hover {
  background: rgba(0, 0, 0, 0.3);
}

.announcement-title {
  font-size: 14px;
  color: #fff;
  margin-bottom: 6px;
  display: flex;
  align-items: center;
}

.announcement-level {
  padding: 2px 6px;
  border-radius: 4px;
  font-size: 12px;
  margin-right: 8px;
}

.announcement-level.重要 {
  background: rgba(255, 107, 107, 0.2);
  color: #ff6b6b;
}

.announcement-level.普通 {
  background: rgba(102, 166, 255, 0.2);
  color: #66a6ff;
}

.announcement-time {
  font-size: 12px;
  color: #a8b6c8;
}

.metric-title {
  font-size: 14px;
  color: #a8b6c8;
  margin-bottom: 8px;
}

.metric-value {
  font-size: 24px;
  font-weight: bold;
}

.metric-unit {
  font-size: 14px;
  margin-left: 4px;
}

.maintenance-content {
  padding: 15px;
  background: rgba(0, 0, 0, 0.2);
  border-radius: 8px;
  max-height: 200px;
  overflow-y: auto;
}

/* 自定义滚动条样式 */
::-webkit-scrollbar {
  width: 6px;
  height: 6px;
}

::-webkit-scrollbar-track {
  background: rgba(255, 255, 255, 0.1);
  border-radius: 3px;
}

::-webkit-scrollbar-thumb {
  background: rgba(255, 255, 255, 0.2);
  border-radius: 3px;
}

::-webkit-scrollbar-thumb:hover {
  background: rgba(255, 255, 255, 0.3);
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.dashboard-content {
  animation: fadeIn 0.5s ease-out;
}


</style>

<style scoped>
.dashboard-container {
  min-height: 100vh;
  background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
  padding: 20px;
  color: #fff;
}

.dashboard-header {
  text-align: center;
  margin-bottom: 30px;
  position: relative;
}

.title-container {
  display: inline-block;
  position: relative;
  padding: 0 50px;
}

.title-container h1 {
  font-size: 32px;
  margin: 0;
  background: linear-gradient(120deg, #89f7fe 0%, #66a6ff 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  position: relative;
  z-index: 1;
}

.title-decoration {
  position: absolute;
  bottom: -5px;
  left: 0;
  width: 100%;
  height: 2px;
  background: linear-gradient(90deg, transparent, #66a6ff, transparent);
  animation: shine 2s infinite;
}

.dashboard-content {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 20px;
  margin-top: 20px;
}

.content-column {
  display: grid;
  grid-template-rows: 1fr 1fr 2fr; /* 最后一行高度加倍 */
  gap: 10px;
  height: calc(100vh - 210px); /* 调整总高度，减去头部和边距 */
}

.info-block,
.chart-container,
.work-order-container,
.pie-container,
.maintenance-container {
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  border-radius: 15px;
  padding: 20px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  border: 1px solid rgba(255, 255, 255, 0.1);
}

.metric-grid {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 15px;
}

.metric-card {
  background: rgba(0, 0, 0, 0.2);
  border-radius: 10px;
  padding: 15px;
  text-align: center;
}

.chart,
.pie-chart {
  height: 300px;
  width: 100%;
}

h3 {
  color: #fff;
  margin: 0 0 15px 0;
  font-size: 18px;
  position: relative;
  padding-left: 15px;
}

h3::before {
  content: "";
  position: absolute;
  left: 0;
  top: 50%;
  transform: translateY(-50%);
  width: 4px;
  height: 16px;
  background: #66a6ff;
  border-radius: 2px;
}

@keyframes shine {
  0% {
    transform: translateX(-100%);
  }
  100% {
    transform: translateX(100%);
  }
}

:deep(.el-table) {
  background-color: transparent !important;
  color: #fff;
}

:deep(.el-table th) {
  background-color: rgba(255, 255, 255, 0.1) !important;
  color: #fff;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

:deep(.el-table td) {
  background-color: transparent;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

:deep(.el-table--enable-row-hover .el-table__body tr:hover > td) {
  background-color: rgba(255, 255, 255, 0.1) !important;
}
</style>
<style scoped>
/* ... table ... */

:deep(.el-table) {
  --el-table-border-color: rgba(255, 255, 255, 0.1);
  --el-table-bg-color: transparent !important;
  --el-table-tr-bg-color: transparent !important;
  --el-table-header-bg-color: rgba(255, 255, 255, 0.1) !important;
}

:deep(.el-table th.el-table__cell) {
  background-color: rgba(255, 255, 255, 0.1) !important;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

:deep(.el-table td.el-table__cell) {
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

:deep(.el-table--enable-row-hover .el-table__body tr:hover > td.el-table__cell) {
  background-color: rgba(255, 255, 255, 0.1) !important;
}
</style>