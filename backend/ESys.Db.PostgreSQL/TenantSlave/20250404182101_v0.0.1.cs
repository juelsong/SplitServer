using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ESys.Db.PostgreSQL.TenantSlave
{
    public partial class v001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Property = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigItemAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Property = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigItemAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ManagerId = table.Column<int>(type: "integer", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DepartFormatter = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Permission_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ManagerId = table.Column<int>(type: "integer", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Department_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Account = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    RealName = table.Column<string>(type: "text", nullable: true),
                    EmployeeId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Title = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    EMail = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    LastMonitoredDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    InitialQualificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NextQualificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PasswordExpiryPeriod = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastPasswordModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Logined = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPasswordHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswordHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPasswordHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DashboardConfig = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Locale = table.Column<string>(type: "text", nullable: true),
                    UserSettings = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConfigItem",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "Property", "UpdateBy", "UpdatedTime", "Value" },
                values: new object[] { 1, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "LOCATION_TYPE_WEIGHT_LEVEL_COUNT", null, null, "8" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Code", "DepartFormatter", "Description", "Order", "ParentId", "Type" },
                values: new object[,]
                {
                    { 1, "system", null, "系统管理", 1, null, 1 },
                    { 58, "visualization", null, "可视化", 58, null, 1 },
                    { 67, "inspectionExecution", null, "检验执行", 67, null, 1 },
                    { 84, "auditPrompt", null, "审核批准", 84, null, 1 },
                    { 90, "plan", null, "检验计划", 90, null, 1 },
                    { 98, "analyse", null, "分析报表", 98, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "Description", "IsActive", "IsHidden", "Name", "UpdateBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, true, true, "超级管理员", null, null },
                    { 2, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, true, false, "管理员", null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Account", "CreateBy", "CreatedTime", "DepartmentId", "EMail", "EmployeeId", "Gender", "InitialQualificationDate", "IsActive", "IsHidden", "LastMonitoredDate", "LastPasswordModified", "NextQualificationDate", "Password", "PasswordExpiryPeriod", "Phone", "RealName", "Salt", "Title", "UpdateBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, "super", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, null, null, null, null, true, true, null, null, null, "l8YyUDcfKuGGgLaMKT4E6mg/8ClZOK8tczolqvakrA8=", null, null, null, "f5ZTUveTZ6szf7wR3qCmvg==", null, null, null },
                    { 2, "ESys_Admin", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, null, null, null, null, true, true, null, null, null, "gIbsDs1b73xjUyptJmm1RjjX8HiJ3ubnt1F/mS6mzio=", null, null, null, "kCXBD/crXrivLJwcfWEoHQ==", null, null, null },
                    { 3, "Admin", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, null, null, null, null, true, false, null, null, null, "QnVVjchwQROIbFPB7PrnYD3htnV5F4AJrfWnsej4JQk=", null, null, "Admin", "tJ48HTFvFBHIWRnO8pxLpQ==", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Code", "DepartFormatter", "Description", "Order", "ParentId", "Type" },
                values: new object[,]
                {
                    { 2, "region", null, "区域", 2, 1, 1 },
                    { 15, "production", null, "产品", 15, 1, 1 },
                    { 19, "testMethod", null, "测试方法", 19, 1, 1 },
                    { 23, "device", null, "设备", 23, 1, 1 },
                    { 28, "medium", null, "培养基", 28, 1, 1 },
                    { 32, "microorganism", null, "微生物", 32, 1, 1 },
                    { 36, "security", null, "安全", 36, 1, 1 },
                    { 53, "settings", null, "系统设置", 53, 1, 1 },
                    { 56, "auditRecord", null, "审计追踪", 56, 1, 1 },
                    { 57, "log", null, "日志", 57, 1, 1 },
                    { 59, "map", null, "地图管理", 59, 58, 1 },
                    { 66, "visualizations", null, "可视化呈现", 66, 58, 1 },
                    { 68, "missions", null, "任务管理", 68, 67, 1 },
                    { 77, "inspectionRecord", null, "结果录入", 77, 67, 1 },
                    { 85, "AuditPrompt:ReTest", null, "审核批准再测试", 85, 84, 2 },
                    { 86, "AuditPrompt:Notest", null, "审核批准无需测试", 86, 84, 1 },
                    { 87, "AuditPrompt:Approve", null, "批准", 87, 84, 2 },
                    { 88, "AuditPrompt:Review", null, "审核", 88, 84, 2 },
                    { 89, "AuditPrompt:Edit", null, "复核修改数据", 89, 84, 2 },
                    { 91, "Plan:Add", null, "计划添加", 91, 90, 2 },
                    { 92, "Plan:Edit", null, "计划编辑", 92, 90, 2 },
                    { 93, "Plan:Approve", null, "计划批准", 93, 90, 2 },
                    { 94, "Plan:Effective", null, "计划激活", 94, 90, 2 },
                    { 95, "Plan:Retire", null, "计划废弃", 95, 90, 2 },
                    { 96, "Plan:AddToPool", null, "计划添加到任务列表", 96, 90, 2 },
                    { 97, "Plan:Calender", null, "计划日历", 97, 90, 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "PermissionId", "RoleId", "UpdateBy", "UpdatedTime" },
                values: new object[] { 1, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, null, null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "RoleId", "UpdateBy", "UpdatedTime", "UserId" },
                values: new object[,]
                {
                    { 1, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, null, null, 1 },
                    { 2, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Code", "DepartFormatter", "Description", "Order", "ParentId", "Type" },
                values: new object[,]
                {
                    { 3, "Classification:Add", null, "添加洁净级别", 3, 2, 2 },
                    { 4, "Classification:Edit", null, "编辑洁净级别", 4, 2, 2 },
                    { 5, "Classification:Disable", null, "禁用洁净级别", 5, 2, 2 },
                    { 6, "SiteType:Add", null, "添加采样点类型", 6, 2, 2 },
                    { 7, "SiteType:Edit", null, "编辑采样点类型", 7, 2, 2 },
                    { 8, "SiteType:Disable", null, "禁用采样点类型", 8, 2, 2 },
                    { 9, "LocationType:Add", null, "添加区域类型", 9, 2, 2 },
                    { 10, "LocationType:Edit", null, "编辑区域类型", 10, 2, 2 },
                    { 11, "LocationType:Disable", null, "禁用区域类型", 11, 2, 2 },
                    { 12, "Location:Add", null, "添加区域", 12, 2, 2 },
                    { 13, "Location:Edit", null, "编辑区域", 13, 2, 2 },
                    { 14, "Location:Disable", null, "禁用区域", 14, 2, 2 },
                    { 16, "Product:Add", null, "添加产品", 16, 15, 2 },
                    { 17, "Product:Edit", null, "编辑产品", 17, 15, 2 },
                    { 18, "Product:Disable", null, "禁用产品", 18, 15, 2 },
                    { 20, "TestMethod:Add", null, "添加测试方法", 20, 19, 2 },
                    { 21, "TestMethod:Edit", null, "编辑测试方法", 21, 19, 2 },
                    { 22, "TestMethod:Disable", null, "禁用测试方法", 22, 19, 2 },
                    { 24, "Equipment:Add", null, "添加设备", 24, 23, 2 },
                    { 25, "Equipment:Edit", null, "编辑设备", 25, 23, 2 },
                    { 26, "Equipment:Disable", null, "禁用设备", 26, 23, 2 },
                    { 27, "Equipment:UpdateConfig", null, "上传文件", 27, 23, 2 },
                    { 29, "Media:Add", null, "添加培养基", 29, 28, 2 },
                    { 30, "Media:Edit", null, "编辑培养基", 30, 28, 2 },
                    { 31, "Media:Disable", null, "禁用培养基", 31, 28, 2 },
                    { 33, "Organism:Add", null, "添加微生物", 33, 32, 2 },
                    { 34, "Organism:Edit", null, "编辑微生物", 34, 32, 2 },
                    { 35, "Organism:Disable", null, "禁用微生物", 35, 32, 2 },
                    { 37, "department", null, "部门管理", 37, 36, 1 },
                    { 41, "user", null, "用户管理", 41, 36, 1 },
                    { 46, "role", null, "角色管理", 46, 36, 1 },
                    { 50, "booking", null, "预订管理", 50, 36, 1 },
                    { 54, "Security:Password", null, "配置密码", 54, 53, 1 },
                    { 55, "Security:Email", null, "配置邮箱", 55, 53, 1 },
                    { 60, "Map:Add", null, "添加地图", 60, 59, 2 },
                    { 61, "Map:Disable", null, "禁用地图", 61, 59, 2 },
                    { 62, "Map:Edit", null, "编辑地图", 62, 59, 2 },
                    { 63, "MapCategory:Add", null, "添加地图分类", 63, 59, 2 },
                    { 64, "MapCategory:Disable", null, "禁用地图分类", 64, 59, 2 },
                    { 65, "MapCategory:Edit", null, "编辑地图分类", 65, 59, 2 },
                    { 69, "Missions:Receive", null, "任务管理领取", 69, 68, 2 },
                    { 70, "Missions:Assign", null, "任务管理分配", 70, 68, 2 },
                    { 71, "Missions:Copy", null, "任务管理复制", 71, 68, 2 },
                    { 72, "Missions:Return", null, "任务管理退回", 72, 68, 2 },
                    { 73, "Missions:Execute", null, "任务管理执行", 73, 68, 2 },
                    { 74, "Missions:NoTest", null, "任务管理无需测试", 74, 68, 2 },
                    { 75, "Missions:Calender", null, "任务日历", 75, 68, 1 },
                    { 76, "Missions:Printer", null, "任务条码打印", 76, 68, 2 },
                    { 78, "InspectionRecord:Sampling", null, "采样", 78, 77, 1 },
                    { 79, "InspectionRecord:Incubation", null, "孵化", 79, 77, 1 },
                    { 80, "InspectionRecord:Testing", null, "测试", 80, 77, 1 },
                    { 81, "InspectionRecord:ResultEntry", null, "录入", 81, 77, 1 },
                    { 82, "InspectionRecord:EditDeviceImport", null, "编辑设备导入数据", 82, 77, 1 },
                    { 83, "InspectionRecord:Notest", null, "结果录入无需测试", 83, 77, 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "PermissionId", "RoleId", "UpdateBy", "UpdatedTime" },
                values: new object[] { 2, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 2, null, null });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Code", "DepartFormatter", "Description", "Order", "ParentId", "Type" },
                values: new object[,]
                {
                    { 38, "Department:Add", null, "添加部门", 38, 37, 2 },
                    { 39, "Department:Edit", null, "编辑部门", 39, 37, 2 },
                    { 40, "Department:Disable", null, "禁用部门", 40, 37, 2 },
                    { 42, "User:Add", null, "添加用户", 42, 41, 2 },
                    { 43, "User:Edit", null, "编辑用户", 43, 41, 2 },
                    { 44, "User:Disable", null, "禁用用户", 44, 41, 2 },
                    { 45, "User:Password", null, "修改密码", 45, 41, 2 },
                    { 47, "Role:Add", null, "添加角色", 47, 46, 2 },
                    { 48, "Role:Edit", null, "编辑角色", 48, 46, 2 },
                    { 49, "Role:Disable", null, "禁用角色", 49, 46, 2 },
                    { 51, "Subscription:Edit", null, "编辑警告订阅", 51, 50, 2 },
                    { 52, "Subscription:Disable", null, "禁用警告订阅", 52, 50, 2 }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "PermissionId", "RoleId", "UpdateBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 3, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3, 2, null, null },
                    { 4, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4, 2, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigItemAudit_EntityId",
                table: "ConfigItemAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ManagerId",
                table: "Department",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Name",
                table: "Department",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentId",
                table: "Department",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentAudit_EntityId",
                table: "DepartmentAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Code",
                table: "Permission",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ParentId",
                table: "Permission",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAudit_EntityId",
                table: "RoleAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionAudit_EntityId",
                table: "RolePermissionAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Account",
                table: "User",
                column: "Account",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistory_UserId",
                table: "UserHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswordHistory_UserId",
                table: "UserPasswordHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserId",
                table: "UserProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleAudit_EntityId",
                table: "UserRoleAudit",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_User_ManagerId",
                table: "Department",
                column: "ManagerId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_ManagerId",
                table: "Department");

            migrationBuilder.DropTable(
                name: "ConfigItem");

            migrationBuilder.DropTable(
                name: "ConfigItemAudit");

            migrationBuilder.DropTable(
                name: "DepartmentAudit");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "RoleAudit");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "RolePermissionAudit");

            migrationBuilder.DropTable(
                name: "UserHistory");

            migrationBuilder.DropTable(
                name: "UserPasswordHistory");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserRoleAudit");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
