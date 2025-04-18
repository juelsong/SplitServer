const parts = require.context("./", true, /^\.\/(((?!index).)*)\.js$/);

let msgs = {
  placeholder: {
    username: "用户名",
    password: "密码",
    captcha: "验证码",
    include: "包含",
    select: "请选择{0}",
    icon: "图标",
    location: "区域",
  },
  confirm: {
    batchDelete: "是否删除选定的{0}条数据？",
    delete: "是否要删除数据？",
    title: "确认",
  },
  prompt: {
    success: "操作成功",
    failed: "操作失败",
    failedReason: "操作失败，{0}",
    pleaseSelectData: "请选择要操作的数据",
    canNotConnectServer: "无法连接服务器",
    pleaseSelectDeletintData: "请选择要删除的数据",
    refreshToken: "会话即将过期，是否继续，点击确定刷新",
    invalidToken: "超时，请重新登录",
    invalidPermission: "没有权限",
  },
  label: {
    login: "登录",
    yes: "是",
    no: "否",
    save: "保存",
    return: "返回",
    export: "导出",
    legend: "图例",
    sizeSelection: {
      large: "大",
      medium: "中",
      small: "小",
    },
    detail: {
      title: "详细信息",
      creator: "创建者",
      creatDate: "创建日期",
      updated: "更新者",
      updateDate: "更新日期",
    },
    dateRange: {
      start: "开始日期",
      separator: "至",
      end: "结束日期",
    },
    history: "历史记录",
  },
  template: {
    close: "关闭",
    search: "查询",
    export: "导出",
    reset: "重置",
    advanced: "高级",
    add: "添加",
    edit: "编辑",
    commit: "提交",
    detail: "详情",
    delete: "删除",
    result: "结果",
    batchDelete: "批量删除",
    disable: "禁用",
    enable: "启用",
    operation: "操作",
    showDisabled: "显示禁用",
    addSuccess: "添加成功",
    updateSuccess: "更新成功",
    updateFail: "更新失败",
    accept: "确定",
    cancel: "取消",
    reject: "驳回",
    new: "新建",
    include: "包含{0}",
    select: "选择{0}",
    beforeClose: "是否关闭对话框?",
    true: "是",
    false: "否",
    tips: "提示",
    beforeDelete: "是否删除选定的数据？",
  },
  tooltip: {
    globalSize: "全局大小",
    language: "语言",
  },
  validator: {
    login: {
      username: "请输入正确的用户名",
      password: "密码必填",
      captcha: "请输入验证码",
    },
    template: {
      required: "{0} 必填",
      exist: "{0} 已存在",
      isNumber: "{0} 应为数字",
      formatWrong: "{0} 格式错误 {1}",
      typeWrong: "{0} 类型错误 {1}",
      min: "{0}必须小于{1}",
      max: "{0}必须大于{1}",
      range: "{0}必须在{1},{2}之间",
      email: "邮件格式错误",
    },
  },
  contextMenu: {
    refresh: "刷新",
    close: "关闭",
    closeOthers: "关闭其他",
    closeAll: "全部关闭",
  },
  menu: {
    dashboard: "常用图表",
    system: "系统管理",
    region: "检测区域",
    production: "关联信息",
    "Product:Add": "添加产品",
    "Product:Edit": "编辑产品",
    "Product:Disable": "禁用产品",
    testMethod: "检测方法",
    device: "仪器设备",
    medium: "试剂耗材",
    microorganism: "菌种鉴定",
    security: "账户管理",
    department: "部门管理",
    user: "用户管理",
    role: "角色管理",
    booking: "预订管理",
    auditRecord: "审计追踪",
    log: "数据日志",
    visualization: "可视化",
    map: "地图管理",
    visualizations: "可视化呈现",
    orgnismVisualizations:"菌种可视化呈现",
    inspectionPlan: "检测计划",
    inspectionPlanCalenderView: "计划日历",
    inspectionExecution: "任务执行",
    missions: "任务管理",
    executingMissions: "执行中任务",
    noTestMissions: "无需测试任务",
    missionsCollection: "任务领取",
    myMissions: "我的任务",
    sampleCollection: "采集样本",
    experimentalOperations: "实验操作",
    incubation: "培养孵育",
    inspectionRecord: "结果判读",
    missionCalender: "任务日历",
    sampleAuditPrompt: "复核批准",
    sampleAudit: "结果复核",
    samplePrompt: "结果批准",
    sampleCompleted: "已完成",
    analyse: "分析报表",
    settings: "系统设置",
    particleCounterData: "未匹配数据查询",
  },
  permission: {
    system: "系统管理",
    region: "检测区域",
    "Classification:Add": "添加洁净级别",
    "Classification:Edit": "编辑洁净级别",
    "Classification:Disable": "禁用洁净级别",
    "SiteType:Add": "添加采样点类型",
    "SiteType:Edit": "编辑采样点类型",
    "SiteType:Disable": "禁用采样点类型",
    "LocationType:Add": "添加区域类型",
    "LocationType:Edit": "编辑区域类型",
    "LocationType:Disable": "禁用区域类型",
    "Location:Add": "添加区域",
    "Location:Edit": "编辑区域",
    "Location:Disable": "禁用区域",
    production: "关联信息",
    "ProductType:Add": "添加关联类型",
    "ProductType:Edit": "编辑关联类型",
    "ProductType:Disable": "禁用关联类型",
    "Product:Add": "添加关联项",
    "Product:Edit": "编辑关联项",
    "Product:Disable": "禁用关联项",
    testMethod: "检测方法",
    "TestMethod:Add": "添加检测方法",
    "TestMethod:Edit": "编辑检测方法",
    "TestMethod:Disable": "禁用检测方法",
    "TestMethod:Copy": "复制检测方法",
    "TestMethod:Workflow": "检测方法流程设置",
    device: "仪器设备",
    "Equipment:Add": "添加设备",
    "Equipment:Edit": "编辑设备",
    "Equipment:Disable": "禁用设备",
    "Equipment:UpdateConfig": "设备配置上传",
    medium: "试剂耗材",
    "Media:Add": "添加试剂耗材",
    "Media:Edit": "编辑试剂耗材",
    "Media:Disable": "禁用试剂耗材",
    microorganism: "菌种鉴定",
    "Organism:Add": "添加菌种",
    "Organism:Edit": "编辑菌种",
    "Organism:Disable": "禁用菌种",
    security: "账户管理",
    department: "部门管理",
    "Department:Add": "添加部门",
    "Department:Edit": "编辑部门",
    "Department:Disable": "禁用部门",
    user: "用户管理",
    "User:Add": "添加用户",
    "User:Edit": "编辑用户",
    "User:Disable": "禁用用户",
    "User:Password": "修改密码",
    role: "角色管理",
    "Role:Add": "添加角色",
    "Role:Edit": "编辑角色",
    "Role:Disable": "禁用角色",
    booking: "预订管理",
    auditRecord: "审计追踪",
    log: "数据日志",
    visualization: "可视化",
    map: "地图管理",
    "Map:Add": "添加地图",
    "Map:Disable": "禁用地图",
    "Map:Edit": "编辑地图",
    "MapCategory:Add": "添加地图分类",
    "MapCategory:Disable": "禁用地图分类",
    "MapCategory:Edit": "编辑地图分类",
    visualizations: "可视化呈现",
    orgnismVisualizations:"菌种可视化呈现",
    inspectionPlan: "检测计划",
    inspectionExecution: "任务执行",
    missionManagements: "任务管理",
    executingMissions: "执行中任务管理",
    noTestMissions: "无需测试任务管理",
    "MissionManagement:Assign": "分配",
    "MissionManagement:Return": "回收",
    "MissionManagement:NoTest": "无需测试",
    "MissionManagement:Edit": "编辑",
    "MissionManagement:Resume": "恢复",
    inspectionRecord: "结果录入",
    audioPrompt: "审核批准",
    analyse: "分析报表",
    "analyse:compliance_view": "查看合规性报表",
    "analyse:compliance_export": "导出合规性报表",
    "analyse:resultsTrending_view": "查看趋势分析报表",
    "analyse:resultsTrending_export": "导出趋势分析报表",
    "analyse:testResult01_view": "查看检验结果报表01",
    "analyse:testResult01_export": "导出检验结果报表01",
    "analyse:testResult02_view": "查看检验结果报表02",
    "analyse:testResult02_export": "导出检验结果报表02",
    "analyse:review_view": "查看空气净化系统监测数据趋势分析报表",
    "analyse:review_export": "导出空气净化系统监测数据趋势分析报表",
    "analyse:testResultExcel_view": "查看导出Excel结果报表",
    "analyse:testResultExcel_exportExcel": "导出Excel结果报表",
    "Subscription:Add": "添加警告订阅",
    "Subscription:Edit": "编辑警告订阅",
    "Subscription:Disable": "删除警告订阅",
    plan: "检测计划",
    "Plan:Add": "计划添加",
    "Plan:Edit": "计划编辑",
    "Plan:Commit": "提交计划",
    "Plan:Delete": "计划删除",
    "Plan:Active": "计划启用",
    "Plan:Effective": "计划生效",
    "Plan:AddToPool": "任务添加",
    "Plan:Audit": "计划审核",
    "Plan:Review": "计划复核",
    "Plan:Approve": "计划批准",
    "Plan:Retire": "计划退役",
    "Plan:Enable": "计划启用和挂起",
    planCalender: "计划日历",
    missionsCollection:"任务领取",
    "MissionsCollection:Receive":"领取",
    myMissions:"我的任务",
    "MyMissions:Execute":"执行",
    "MyMissions:NoTest":"无需测试",
    "MyMissions:Export":"导出",
    "MyMissions:Return":"退回",
    sampleCollection:"采集样本",
    "SampleCollection:Execute":"执行",
    "SampleCollection:NoTest":"无需测试",
    experimentalOperations:"实验操作",
    "ExperimentalOperations:Execute":"执行",
    "ExperimentalOperations:NoTest":"无需测试",
    incubation:"培养孵育",
    "Incubation:Execute":"执行",
    "Incubation:NoTest":"无需测试",
    inspectionRecord:"结果判断",
    "InspectionRecord:Execute":"执行",
    "InspectionRecord:NoTest":"无需测试",
    "InspectionRecord:EditDeviceImport":"设备数据导入",
    "InspectionRecord:Compare":"设备数据匹配",
    missionsCalender: "任务日历",
    particleCounterData: "未匹配数据查询",
    sampleAuditPrompt: "审核批准",
    sampleAudit: "结果复核",
    "SampleAudit:Review": "复核",
    "SampleAudit:Edit": "编辑",
    samplePrompt: "结果批准",
    "SamplePrompt:Approval": "批准",
    sampleCompleted: "已完成",
    "SampleCompleted:ReApprove": "重新打开",
    "AuditPrompt:ReTest": "复原无需测试",
    "AuditPrompt:Approve": "批准",
    "AuditPrompt:Review": "复核",
    "AuditPrompt:Edit": "编辑",
    "AuditPrompt:Notest": "无需测试",
    settings: "系统设置",
    "Security:Config": "安全设置",
    "Security:Password": "密码配置",
    "Security:Email": "邮箱配置",
    "Security:ImportExport": "导入导出",
    "Import:Location":"导入区域",
    "Export:Location":"导出区域",
    "Import:Site":"导入采样点",
    "Export:Site":"导出采样点",
    "Import:Media":"导入试剂耗材",
    "Export:Media":"导出试剂耗材",
    "Import:Equipment":"导入设备",
    "Export:Equipment":"导出设备",
    "Import:Organism":"导入菌种",
    "Export:Organism":"导出菌种",
  },
};

parts
  .keys()
  .map((str) => {
    let strs = str.split("/");
    let part = strs[strs.length - 1].split(".")[0];
    return {
      part,
      module: parts(str),
    };
  })
  .reduce((accu, val) => {
    if (val.module.default) {
      accu[val.part] = val.module.default;
    }
    return accu;
  }, msgs);

export default msgs;
