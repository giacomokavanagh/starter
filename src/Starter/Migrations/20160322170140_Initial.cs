using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace Starter.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });
            migrationBuilder.CreateTable(
                name: "Library",
                columns: table => new
                {
                    LibraryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library", x => x.LibraryID);
                });
            migrationBuilder.CreateTable(
                name: "MasterKey",
                columns: table => new
                {
                    MasterKeyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MasterKeyString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterKey", x => x.MasterKeyID);
                });
            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    PlatformID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.PlatformID);
                });
            migrationBuilder.CreateTable(
                name: "TestEnvironment",
                columns: table => new
                {
                    TestEnvironmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    XMLFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEnvironment", x => x.TestEnvironmentID);
                });
            migrationBuilder.CreateTable(
                name: "TestRunnerGroup",
                columns: table => new
                {
                    TestRunnerGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRunnerGroup", x => x.TestRunnerGroupID);
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    CollectionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.CollectionID);
                    table.ForeignKey(
                        name: "FK_Collection_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    SectionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    LibraryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.SectionID);
                    table.ForeignKey(
                        name: "FK_Section_Library_LibraryID",
                        column: x => x.LibraryID,
                        principalTable: "Library",
                        principalColumn: "LibraryID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PlatformID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaID);
                    table.ForeignKey(
                        name: "FK_Area_Platform_PlatformID",
                        column: x => x.PlatformID,
                        principalTable: "Platform",
                        principalColumn: "PlatformID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TestRunnerGroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Project_TestRunnerGroup_TestRunnerGroupID",
                        column: x => x.TestRunnerGroupID,
                        principalTable: "TestRunnerGroup",
                        principalColumn: "TestRunnerGroupID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "TestRunner",
                columns: table => new
                {
                    TestRunnerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClearRemoteLogOnNextAccess = table.Column<bool>(nullable: false),
                    ConfigurationDetails = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TakeScreenshots = table.Column<bool>(nullable: false),
                    TestRunnerGroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRunner", x => x.TestRunnerID);
                    table.ForeignKey(
                        name: "FK_TestRunner_TestRunnerGroup_TestRunnerGroupID",
                        column: x => x.TestRunnerGroupID,
                        principalTable: "TestRunnerGroup",
                        principalColumn: "TestRunnerGroupID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    SetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectionID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.SetID);
                    table.ForeignKey(
                        name: "FK_Set_Collection_CollectionID",
                        column: x => x.CollectionID,
                        principalTable: "Collection",
                        principalColumn: "CollectionID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Suite",
                columns: table => new
                {
                    SuiteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SectionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suite", x => x.SuiteID);
                    table.ForeignKey(
                        name: "FK_Suite_Section_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Section",
                        principalColumn: "SectionID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Component",
                columns: table => new
                {
                    ComponentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_Component_Area_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Area",
                        principalColumn: "AreaID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    FolderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false),
                    TestRunnerGroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.FolderID);
                    table.ForeignKey(
                        name: "FK_Folder_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Folder_TestRunnerGroup_TestRunnerGroupID",
                        column: x => x.TestRunnerGroupID,
                        principalTable: "TestRunnerGroup",
                        principalColumn: "TestRunnerGroupID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "DerivedKey",
                columns: table => new
                {
                    DerivedKeyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DerivedKeyString = table.Column<string>(nullable: true),
                    MasterKeyID = table.Column<int>(nullable: false),
                    TestRunnerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerivedKey", x => x.DerivedKeyID);
                    table.ForeignKey(
                        name: "FK_DerivedKey_MasterKey_MasterKeyID",
                        column: x => x.MasterKeyID,
                        principalTable: "MasterKey",
                        principalColumn: "MasterKeyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DerivedKey_TestRunner_TestRunnerID",
                        column: x => x.TestRunnerID,
                        principalTable: "TestRunner",
                        principalColumn: "TestRunnerID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "TestRunnerLog",
                columns: table => new
                {
                    TestRunnerLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    Filename = table.Column<string>(nullable: true),
                    TestRunnerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRunnerLog", x => x.TestRunnerLogID);
                    table.ForeignKey(
                        name: "FK_TestRunnerLog_TestRunner_TestRunnerID",
                        column: x => x.TestRunnerID,
                        principalTable: "TestRunner",
                        principalColumn: "TestRunnerID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Procedure",
                columns: table => new
                {
                    ProcedureID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure", x => x.ProcedureID);
                    table.ForeignKey(
                        name: "FK_Procedure_Set_SetID",
                        column: x => x.SetID,
                        principalTable: "Set",
                        principalColumn: "SetID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    ProcessID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComponentID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.ProcessID);
                    table.ForeignKey(
                        name: "FK_Process_Component_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Component",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    FolderID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TestRunnerGroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupID);
                    table.ForeignKey(
                        name: "FK_Group_Folder_FolderID",
                        column: x => x.FolderID,
                        principalTable: "Folder",
                        principalColumn: "FolderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Group_TestRunnerGroup_TestRunnerGroupID",
                        column: x => x.TestRunnerGroupID,
                        principalTable: "TestRunnerGroup",
                        principalColumn: "TestRunnerGroupID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "ProcessStep",
                columns: table => new
                {
                    ProcessStepID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Attribute = table.Column<string>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ProcessID = table.Column<int>(nullable: false),
                    Static = table.Column<bool>(nullable: false),
                    StepID = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessStep", x => x.ProcessStepID);
                    table.ForeignKey(
                        name: "FK_ProcessStep_Process_ProcessID",
                        column: x => x.ProcessID,
                        principalTable: "Process",
                        principalColumn: "ProcessID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Run",
                columns: table => new
                {
                    RunID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    GroupID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TestRunnerGroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Run", x => x.RunID);
                    table.ForeignKey(
                        name: "FK_Run_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Run_TestRunnerGroup_TestRunnerGroupID",
                        column: x => x.TestRunnerGroupID,
                        principalTable: "TestRunnerGroup",
                        principalColumn: "TestRunnerGroupID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "ProcedureStep",
                columns: table => new
                {
                    ProcedureStepID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Attribute = table.Column<string>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    MatchesProcessStep = table.Column<bool>(nullable: true),
                    Method = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ProcedureID = table.Column<int>(nullable: false),
                    ProcessStepID = table.Column<int>(nullable: true),
                    Static = table.Column<bool>(nullable: false),
                    StepID = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureStep", x => x.ProcedureStepID);
                    table.ForeignKey(
                        name: "FK_ProcedureStep_Procedure_ProcedureID",
                        column: x => x.ProcedureID,
                        principalTable: "Procedure",
                        principalColumn: "ProcedureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedureStep_ProcessStep_ProcessStepID",
                        column: x => x.ProcessStepID,
                        principalTable: "ProcessStep",
                        principalColumn: "ProcessStepID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "AvailableMethod",
                columns: table => new
                {
                    AvailableMethodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StoredScreenshotStoredScreenshotDetailsID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableMethod", x => x.AvailableMethodID);
                });
            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    TestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvailableMethodAvailableMethodID = table.Column<int>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    ExcelFilePath = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    RunRunID = table.Column<int>(nullable: true),
                    SuiteID = table.Column<int>(nullable: false),
                    TestDataSource = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.TestID);
                    table.ForeignKey(
                        name: "FK_Test_AvailableMethod_AvailableMethodAvailableMethodID",
                        column: x => x.AvailableMethodAvailableMethodID,
                        principalTable: "AvailableMethod",
                        principalColumn: "AvailableMethodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Test_Run_RunRunID",
                        column: x => x.RunRunID,
                        principalTable: "Run",
                        principalColumn: "RunID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Test_Suite_SuiteID",
                        column: x => x.SuiteID,
                        principalTable: "Suite",
                        principalColumn: "SuiteID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Attribute = table.Column<string>(nullable: true),
                    AvailableMethodAvailableMethodID = table.Column<int>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    StepID = table.Column<string>(nullable: false),
                    TestID = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Step_AvailableMethod_AvailableMethodAvailableMethodID",
                        column: x => x.AvailableMethodAvailableMethodID,
                        principalTable: "AvailableMethod",
                        principalColumn: "AvailableMethodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Step_Test_TestID",
                        column: x => x.TestID,
                        principalTable: "Test",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Dependency",
                columns: table => new
                {
                    DependencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DependencyGroupID = table.Column<int>(nullable: false),
                    TestRunID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependency", x => x.DependencyID);
                });
            migrationBuilder.CreateTable(
                name: "DependencyGroup",
                columns: table => new
                {
                    DependencyGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    TestRunTestRunID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependencyGroup", x => x.DependencyGroupID);
                });
            migrationBuilder.CreateTable(
                name: "TestRun",
                columns: table => new
                {
                    TestRunID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Browser = table.Column<string>(nullable: false),
                    DependencyGroupID = table.Column<int>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Retries = table.Column<int>(nullable: true),
                    RetriesLeft = table.Column<int>(nullable: true),
                    RunID = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    TestEnvironmentID = table.Column<int>(nullable: true),
                    TestID = table.Column<int>(nullable: false),
                    TestRunner = table.Column<int>(nullable: true),
                    TestRunnerGroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRun", x => x.TestRunID);
                    table.ForeignKey(
                        name: "FK_TestRun_DependencyGroup_DependencyGroupID",
                        column: x => x.DependencyGroupID,
                        principalTable: "DependencyGroup",
                        principalColumn: "DependencyGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestRun_Run_RunID",
                        column: x => x.RunID,
                        principalTable: "Run",
                        principalColumn: "RunID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestRun_TestEnvironment_TestEnvironmentID",
                        column: x => x.TestEnvironmentID,
                        principalTable: "TestEnvironment",
                        principalColumn: "TestEnvironmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestRun_Test_TestID",
                        column: x => x.TestID,
                        principalTable: "Test",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestRun_TestRunnerGroup_TestRunnerGroupID",
                        column: x => x.TestRunnerGroupID,
                        principalTable: "TestRunnerGroup",
                        principalColumn: "TestRunnerGroupID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    ResultID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvailableMethodAvailableMethodID = table.Column<int>(nullable: true),
                    Duration = table.Column<DateTime>(nullable: false),
                    ResultDirectory = table.Column<string>(nullable: true),
                    ResultName = table.Column<string>(nullable: true),
                    ScreenshotsDirectory = table.Column<string>(nullable: true),
                    StepsBlocked = table.Column<int>(nullable: false),
                    StepsFailed = table.Column<int>(nullable: false),
                    StepsPassed = table.Column<int>(nullable: false),
                    StoredBrowser = table.Column<string>(nullable: true),
                    StoredEndTime = table.Column<DateTime>(nullable: false),
                    StoredStartTime = table.Column<DateTime>(nullable: false),
                    StoredStatus = table.Column<string>(nullable: true),
                    StoredTestDataFileContentType = table.Column<string>(nullable: true),
                    StoredTestDataFileName = table.Column<string>(nullable: true),
                    StoredTestEnvironmentFileContentType = table.Column<string>(nullable: true),
                    StoredTestEnvironmentFileName = table.Column<string>(nullable: true),
                    StoredTestEnvironmentID = table.Column<int>(nullable: false),
                    StoredTestEnvironmentName = table.Column<string>(nullable: true),
                    StoredTestID = table.Column<int>(nullable: false),
                    StoredTestName = table.Column<string>(nullable: true),
                    StoredTestRunnerID = table.Column<int>(nullable: false),
                    StoredTestRunnerName = table.Column<string>(nullable: true),
                    TestRunID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.ResultID);
                    table.ForeignKey(
                        name: "FK_Result_AvailableMethod_AvailableMethodAvailableMethodID",
                        column: x => x.AvailableMethodAvailableMethodID,
                        principalTable: "AvailableMethod",
                        principalColumn: "AvailableMethodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_TestRun_TestRunID",
                        column: x => x.TestRunID,
                        principalTable: "TestRun",
                        principalColumn: "TestRunID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "StoredScreenshotDetails",
                columns: table => new
                {
                    StoredScreenshotDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Order = table.Column<int>(nullable: false),
                    ResultID = table.Column<int>(nullable: false),
                    StepID = table.Column<string>(nullable: true),
                    StoredScreenshotFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredScreenshotDetails", x => x.StoredScreenshotDetailsID);
                    table.ForeignKey(
                        name: "FK_StoredScreenshotDetails_Result_ResultID",
                        column: x => x.ResultID,
                        principalTable: "Result",
                        principalColumn: "ResultID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "StoredStepDetails",
                columns: table => new
                {
                    StoredStepDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Attribute = table.Column<string>(nullable: true),
                    CatastrophicFailure = table.Column<bool>(nullable: false),
                    Input = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ResultID = table.Column<int>(nullable: false),
                    StepEndTime = table.Column<string>(nullable: true),
                    StepID = table.Column<string>(nullable: true),
                    StepStartTime = table.Column<string>(nullable: true),
                    StoredStepStatus = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredStepDetails", x => x.StoredStepDetailsID);
                    table.ForeignKey(
                        name: "FK_StoredStepDetails_Result_ResultID",
                        column: x => x.ResultID,
                        principalTable: "Result",
                        principalColumn: "ResultID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "StoredTestExceptionDetails",
                columns: table => new
                {
                    ExceptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    ExceptionType = table.Column<string>(nullable: true),
                    StoredStepDetailsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredTestExceptionDetails", x => x.ExceptionID);
                    table.ForeignKey(
                        name: "FK_StoredTestExceptionDetails_StoredStepDetails_StoredStepDetailsID",
                        column: x => x.StoredStepDetailsID,
                        principalTable: "StoredStepDetails",
                        principalColumn: "StoredStepDetailsID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
            migrationBuilder.AddForeignKey(
                name: "FK_AvailableMethod_StoredScreenshotDetails_StoredScreenshotStoredScreenshotDetailsID",
                table: "AvailableMethod",
                column: "StoredScreenshotStoredScreenshotDetailsID",
                principalTable: "StoredScreenshotDetails",
                principalColumn: "StoredScreenshotDetailsID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_DependencyGroup_DependencyGroupID",
                table: "Dependency",
                column: "DependencyGroupID",
                principalTable: "DependencyGroup",
                principalColumn: "DependencyGroupID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_TestRun_TestRunID",
                table: "Dependency",
                column: "TestRunID",
                principalTable: "TestRun",
                principalColumn: "TestRunID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_DependencyGroup_TestRun_TestRunTestRunID",
                table: "DependencyGroup",
                column: "TestRunTestRunID",
                principalTable: "TestRun",
                principalColumn: "TestRunID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Result_AvailableMethod_AvailableMethodAvailableMethodID", table: "Result");
            migrationBuilder.DropForeignKey(name: "FK_Test_AvailableMethod_AvailableMethodAvailableMethodID", table: "Test");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_DependencyGroup_DependencyGroupID", table: "TestRun");
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("Dependency");
            migrationBuilder.DropTable("DerivedKey");
            migrationBuilder.DropTable("ProcedureStep");
            migrationBuilder.DropTable("Step");
            migrationBuilder.DropTable("StoredTestExceptionDetails");
            migrationBuilder.DropTable("TestRunnerLog");
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("AspNetUsers");
            migrationBuilder.DropTable("MasterKey");
            migrationBuilder.DropTable("Procedure");
            migrationBuilder.DropTable("ProcessStep");
            migrationBuilder.DropTable("StoredStepDetails");
            migrationBuilder.DropTable("TestRunner");
            migrationBuilder.DropTable("Set");
            migrationBuilder.DropTable("Process");
            migrationBuilder.DropTable("Collection");
            migrationBuilder.DropTable("Component");
            migrationBuilder.DropTable("Category");
            migrationBuilder.DropTable("Area");
            migrationBuilder.DropTable("Platform");
            migrationBuilder.DropTable("AvailableMethod");
            migrationBuilder.DropTable("StoredScreenshotDetails");
            migrationBuilder.DropTable("Result");
            migrationBuilder.DropTable("DependencyGroup");
            migrationBuilder.DropTable("TestRun");
            migrationBuilder.DropTable("TestEnvironment");
            migrationBuilder.DropTable("Test");
            migrationBuilder.DropTable("Run");
            migrationBuilder.DropTable("Suite");
            migrationBuilder.DropTable("Group");
            migrationBuilder.DropTable("Section");
            migrationBuilder.DropTable("Folder");
            migrationBuilder.DropTable("Library");
            migrationBuilder.DropTable("Project");
            migrationBuilder.DropTable("TestRunnerGroup");
        }
    }
}
