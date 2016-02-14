using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Starter.Migrations
{
    public partial class ScreenshotFolderToResult : Migration
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
            migrationBuilder.DropForeignKey(name: "FK_Result_TestRun_TestRunID", table: "Result");
            migrationBuilder.DropForeignKey(name: "FK_Run_Group_GroupID", table: "Run");
            migrationBuilder.DropForeignKey(name: "FK_Section_Library_LibraryID", table: "Section");
            migrationBuilder.DropForeignKey(name: "FK_StoredScreenshotDetails_Result_ResultID", table: "StoredScreenshotDetails");
            migrationBuilder.DropForeignKey(name: "FK_StoredStepDetails_Result_ResultID", table: "StoredStepDetails");
            migrationBuilder.DropForeignKey(name: "FK_StoredTestExceptionDetails_StoredStepDetails_StoredStepDetailsID", table: "StoredTestExceptionDetails");
            migrationBuilder.DropForeignKey(name: "FK_Suite_Section_SectionID", table: "Suite");
            migrationBuilder.DropForeignKey(name: "FK_Test_Suite_SuiteID", table: "Test");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Run_RunID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Test_TestID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRunner_TestRunnerGroup_TestRunnerGroupID", table: "TestRunner");
            migrationBuilder.AddColumn<string>(
                name: "ScreenshotsDirectory",
                table: "Result",
                nullable: true);
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
                name: "FK_StoredScreenshotDetails_Result_ResultID",
                table: "StoredScreenshotDetails",
                column: "ResultID",
                principalTable: "Result",
                principalColumn: "ResultID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_StoredStepDetails_Result_ResultID",
                table: "StoredStepDetails",
                column: "ResultID",
                principalTable: "Result",
                principalColumn: "ResultID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_StoredTestExceptionDetails_StoredStepDetails_StoredStepDetailsID",
                table: "StoredTestExceptionDetails",
                column: "StoredStepDetailsID",
                principalTable: "StoredStepDetails",
                principalColumn: "StoredStepDetailsID",
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
            migrationBuilder.DropForeignKey(name: "FK_StoredScreenshotDetails_Result_ResultID", table: "StoredScreenshotDetails");
            migrationBuilder.DropForeignKey(name: "FK_StoredStepDetails_Result_ResultID", table: "StoredStepDetails");
            migrationBuilder.DropForeignKey(name: "FK_StoredTestExceptionDetails_StoredStepDetails_StoredStepDetailsID", table: "StoredTestExceptionDetails");
            migrationBuilder.DropForeignKey(name: "FK_Suite_Section_SectionID", table: "Suite");
            migrationBuilder.DropForeignKey(name: "FK_Test_Suite_SuiteID", table: "Test");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Run_RunID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Test_TestID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRunner_TestRunnerGroup_TestRunnerGroupID", table: "TestRunner");
            migrationBuilder.DropColumn(name: "ScreenshotsDirectory", table: "Result");
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
                name: "FK_StoredScreenshotDetails_Result_ResultID",
                table: "StoredScreenshotDetails",
                column: "ResultID",
                principalTable: "Result",
                principalColumn: "ResultID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_StoredStepDetails_Result_ResultID",
                table: "StoredStepDetails",
                column: "ResultID",
                principalTable: "Result",
                principalColumn: "ResultID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_StoredTestExceptionDetails_StoredStepDetails_StoredStepDetailsID",
                table: "StoredTestExceptionDetails",
                column: "StoredStepDetailsID",
                principalTable: "StoredStepDetails",
                principalColumn: "StoredStepDetailsID",
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
