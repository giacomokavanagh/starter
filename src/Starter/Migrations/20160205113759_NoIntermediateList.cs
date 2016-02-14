using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace Starter.Migrations
{
    public partial class NoIntermediateList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_Dependency_DependencyGroup_DependencyGroupID", table: "Dependency");
            migrationBuilder.DropForeignKey(name: "FK_Dependency_TestRun_TestRunID", table: "Dependency");
            migrationBuilder.DropForeignKey(name: "FK_Folder_Project_ProjectID", table: "Folder");
            migrationBuilder.DropForeignKey(name: "FK_Group_Folder_FolderID", table: "Group");
            migrationBuilder.DropForeignKey(name: "FK_Result_ScreenshotDetailsList_ScreenshotDetailsListScreenshotDetailsListID", table: "Result");
            migrationBuilder.DropForeignKey(name: "FK_Result_StepDetailsList_StepDetailsListStepDetailsListID", table: "Result");
            migrationBuilder.DropForeignKey(name: "FK_Result_TestRun_TestRunID", table: "Result");
            migrationBuilder.DropForeignKey(name: "FK_Run_Group_GroupID", table: "Run");
            migrationBuilder.DropForeignKey(name: "FK_Section_Library_LibraryID", table: "Section");
            migrationBuilder.DropForeignKey(name: "FK_Suite_Section_SectionID", table: "Suite");
            migrationBuilder.DropForeignKey(name: "FK_Test_Suite_SuiteID", table: "Test");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Run_RunID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Test_TestID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRunner_TestRunnerGroup_TestRunnerGroupID", table: "TestRunner");
            migrationBuilder.DropColumn(name: "ScreenshotDetailsListScreenshotDetailsListID", table: "Result");
            migrationBuilder.DropColumn(name: "StepDetailsListStepDetailsListID", table: "Result");
            migrationBuilder.DropTable("ScreenshotDetailsList");
            migrationBuilder.DropTable("StepDetailsList");
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
                    Input = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ResultID = table.Column<int>(nullable: false),
                    StepID = table.Column<string>(nullable: true),
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
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "FK_Folder_Project_ProjectID",
                table: "Folder",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Group_Folder_FolderID",
                table: "Group",
                column: "FolderID",
                principalTable: "Folder",
                principalColumn: "FolderID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Result_TestRun_TestRunID",
                table: "Result",
                column: "TestRunID",
                principalTable: "TestRun",
                principalColumn: "TestRunID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Run_Group_GroupID",
                table: "Run",
                column: "GroupID",
                principalTable: "Group",
                principalColumn: "GroupID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Section_Library_LibraryID",
                table: "Section",
                column: "LibraryID",
                principalTable: "Library",
                principalColumn: "LibraryID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Suite_Section_SectionID",
                table: "Suite",
                column: "SectionID",
                principalTable: "Section",
                principalColumn: "SectionID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Test_Suite_SuiteID",
                table: "Test",
                column: "SuiteID",
                principalTable: "Suite",
                principalColumn: "SuiteID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_Run_RunID",
                table: "TestRun",
                column: "RunID",
                principalTable: "Run",
                principalColumn: "RunID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_Test_TestID",
                table: "TestRun",
                column: "TestID",
                principalTable: "Test",
                principalColumn: "TestID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_TestRunner_TestRunnerGroup_TestRunnerGroupID",
                table: "TestRunner",
                column: "TestRunnerGroupID",
                principalTable: "TestRunnerGroup",
                principalColumn: "TestRunnerGroupID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_Dependency_DependencyGroup_DependencyGroupID", table: "Dependency");
            migrationBuilder.DropForeignKey(name: "FK_Dependency_TestRun_TestRunID", table: "Dependency");
            migrationBuilder.DropForeignKey(name: "FK_Folder_Project_ProjectID", table: "Folder");
            migrationBuilder.DropForeignKey(name: "FK_Group_Folder_FolderID", table: "Group");
            migrationBuilder.DropForeignKey(name: "FK_Result_TestRun_TestRunID", table: "Result");
            migrationBuilder.DropForeignKey(name: "FK_Run_Group_GroupID", table: "Run");
            migrationBuilder.DropForeignKey(name: "FK_Section_Library_LibraryID", table: "Section");
            migrationBuilder.DropForeignKey(name: "FK_Suite_Section_SectionID", table: "Suite");
            migrationBuilder.DropForeignKey(name: "FK_Test_Suite_SuiteID", table: "Test");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Run_RunID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Test_TestID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRunner_TestRunnerGroup_TestRunnerGroupID", table: "TestRunner");
            migrationBuilder.DropTable("StoredScreenshotDetails");
            migrationBuilder.DropTable("StoredStepDetails");
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
            migrationBuilder.AddColumn<int>(
                name: "ScreenshotDetailsListScreenshotDetailsListID",
                table: "Result",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "StepDetailsListStepDetailsListID",
                table: "Result",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_DependencyGroup_DependencyGroupID",
                table: "Dependency",
                column: "DependencyGroupID",
                principalTable: "DependencyGroup",
                principalColumn: "DependencyGroupID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_TestRun_TestRunID",
                table: "Dependency",
                column: "TestRunID",
                principalTable: "TestRun",
                principalColumn: "TestRunID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Folder_Project_ProjectID",
                table: "Folder",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Group_Folder_FolderID",
                table: "Group",
                column: "FolderID",
                principalTable: "Folder",
                principalColumn: "FolderID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Result_ScreenshotDetailsList_ScreenshotDetailsListScreenshotDetailsListID",
                table: "Result",
                column: "ScreenshotDetailsListScreenshotDetailsListID",
                principalTable: "ScreenshotDetailsList",
                principalColumn: "ScreenshotDetailsListID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Result_StepDetailsList_StepDetailsListStepDetailsListID",
                table: "Result",
                column: "StepDetailsListStepDetailsListID",
                principalTable: "StepDetailsList",
                principalColumn: "StepDetailsListID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Result_TestRun_TestRunID",
                table: "Result",
                column: "TestRunID",
                principalTable: "TestRun",
                principalColumn: "TestRunID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Run_Group_GroupID",
                table: "Run",
                column: "GroupID",
                principalTable: "Group",
                principalColumn: "GroupID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Section_Library_LibraryID",
                table: "Section",
                column: "LibraryID",
                principalTable: "Library",
                principalColumn: "LibraryID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Suite_Section_SectionID",
                table: "Suite",
                column: "SectionID",
                principalTable: "Section",
                principalColumn: "SectionID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Test_Suite_SuiteID",
                table: "Test",
                column: "SuiteID",
                principalTable: "Suite",
                principalColumn: "SuiteID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_Run_RunID",
                table: "TestRun",
                column: "RunID",
                principalTable: "Run",
                principalColumn: "RunID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_Test_TestID",
                table: "TestRun",
                column: "TestID",
                principalTable: "Test",
                principalColumn: "TestID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_TestRunner_TestRunnerGroup_TestRunnerGroupID",
                table: "TestRunner",
                column: "TestRunnerGroupID",
                principalTable: "TestRunnerGroup",
                principalColumn: "TestRunnerGroupID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
