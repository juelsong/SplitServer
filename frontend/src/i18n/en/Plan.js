export default {
  entity: "TestPlan",
  filter: {
    IsActive: "IsActive",
    Status: "Status",
    TestSite: "Location",
  },
  column: {
    Name: "Name",
    Description: "Description",
    Version: "Version",
    Status: "Status",
    Environment: "Environment",
    Frequency: "Frequency",
    LocationName: "LocationName",
  },
  status: {
    Draft: "Draft",
    Audit: "审核中",
    Approving: "审批中",
    Approved: "已批准",
    Effective: "Effective",
    Superceded: "Superceded",
    Retired: "Retired",
  },
  button: {
    Audit: "Audit",
    Approve: "Approve",
    Effect: "Effect",
    Suspend: "Suspend",
    Save: "Save",
    Retire: "Retire",
    Pending: "Pending",
    ClearSelect: "ClearSelect",
  },
  editor: {
    Name: "Name",
    Description: "Description",
    DefaultWorkGroup: "DefaultWorkGroup",
    Version: "Version",
    ApproveDate: "ApproveDate",
    FillEffectiveDate: "FillEffectiveDate",
    EffectiveDate: "EffectiveDate",
    Environment: "Environment",
    CurrentFirstDay: "CurrentFirstDay",
    Region: "Region",
    Barcode: "Barcode",
    SampleFrequency: "SampleFrequency",
    StandardOperatingrocedures: "StandardOperatingrocedures",
    Frequency: "Frequency",
    Limit: "Limit",
    Every: "Every",
    MinutesOfHour: "MinutesOfHour",
    FillLength: "FillLength",
    Time: "Time",
    FillDay: "FillDay",
    FillFrequency: "FillFrequency",
    HoursToRun: "HoursToRun",
    SelectTime: "SelectTime",
    User: "User",
    TestCount: "Test Count",
    SiteCount: "Site Count",
    ApprovingComment: "Comment",
    CommitUser: "Commit User",
    CommitDate: "Commit Date",
    CommitComment: "Commit Comment",
    CurrentOperator: "Operator",
    ApprovingStatus: "Status",
    Workflow: "Workflow",
    AuditUser: "审核人",
    ApprovingUser: "审批人",
  },
  lable: {
    changeRow: "Confirm that the switching plan has been saved",
    Tips: "Tips",
    delete: "Delete the selected data?",
    minDate: "the planned date should be after that time",
    minRetiredDate: "the retired date should be after that time",
  },
  legend: {
    Invalid: "Invalid",
    Valid: "Valid",
    Ineffective: "Ineffective",
  },
  title: {
    Commit: "检测计划申请",
    Audit: "处理审核",
    Approving: "处理审批",
  },
};