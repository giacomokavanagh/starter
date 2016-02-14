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
                name: "ScreenshotDetailsList",
                columns: table => new
                {
                    ScreenshotDetailsListID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResultID = table.Column<int>(nullable: false),
                    StoredScreenshotDetailsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenshotDetailsList", x => x.ScreenshotDetailsListID);
                });
            migrationBuilder.CreateTable(
                name: "StepDetailsList",
                columns: table => new
                {
                    StepDetailsListID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResultID = table.Column<int>(nullable: false),
                    StoredStepDetailsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepDetailsList", x => x.StepDetailsListID);
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
                    ConfigurationDetails = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
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
                name: "Test",
                columns: table => new
                {
                    TestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    ResultFile = table.Column<string>(nullable: true),
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
                    ResultFileName = table.Column<string>(nullable: true),
                    ResultFilePath = table.Column<string>(nullable: true),
                    ResultPath = table.Column<string>(nullable: true),
                    ScreenshotDetailsListScreenshotDetailsListID = table.Column<int>(nullable: true),
                    StepDetailsListStepDetailsListID = table.Column<int>(nullable: true),
                    StoredEndTime = table.Column<DateTime>(nullable: false),
                    StoredStartTime = table.Column<DateTime>(nullable: false),
                    StoredStatus = table.Column<string>(nullable: true),
                    StoredTestDataFileName = table.Column<string>(nullable: true),
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
                        name: "FK_Result_ScreenshotDetailsList_ScreenshotDetailsListScreenshotDetailsListID",
                        column: x => x.ScreenshotDetailsListScreenshotDetailsListID,
                        principalTable: "ScreenshotDetailsList",
                        principalColumn: "ScreenshotDetailsListID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_StepDetailsList_StepDetailsListStepDetailsListID",
                        column: x => x.StepDetailsListStepDetailsListID,
                        principalTable: "StepDetailsList",
                        principalColumn: "StepDetailsListID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_TestRun_TestRunID",
                        column: x => x.TestRunID,
                        principalTable: "TestRun",
                        principalColumn: "TestRunID",
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
            migrationBuilder.DropForeignKey(name: "FK_TestRun_DependencyGroup_DependencyGroupID", table: "TestRun");
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("Dependency");
            migrationBuilder.DropTable("Result");
            migrationBuilder.DropTable("TestRunner");
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("AspNetUsers");
            migrationBuilder.DropTable("ScreenshotDetailsList");
            migrationBuilder.DropTable("StepDetailsList");
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
