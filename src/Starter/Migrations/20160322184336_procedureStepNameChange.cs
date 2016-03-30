using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Starter.Migrations
{
    public partial class procedureStepNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_Area_Platform_PlatformID", table: "Area");
            migrationBuilder.DropForeignKey(name: "FK_Collection_Category_CategoryID", table: "Collection");
            migrationBuilder.DropForeignKey(name: "FK_Component_Area_AreaID", table: "Component");
            migrationBuilder.DropForeignKey(name: "FK_Dependency_DependencyGroup_DependencyGroupID", table: "Dependency");
            migrationBuilder.DropForeignKey(name: "FK_Dependency_TestRun_TestRunID", table: "Dependency");
            migrationBuilder.DropForeignKey(name: "FK_DerivedKey_MasterKey_MasterKeyID", table: "DerivedKey");
            migrationBuilder.DropForeignKey(name: "FK_Folder_Project_ProjectID", table: "Folder");
            migrationBuilder.DropForeignKey(name: "FK_Group_Folder_FolderID", table: "Group");
            migrationBuilder.DropForeignKey(name: "FK_Procedure_Set_SetID", table: "Procedure");
            migrationBuilder.DropForeignKey(name: "FK_ProcedureStep_Procedure_ProcedureID", table: "ProcedureStep");
            migrationBuilder.DropForeignKey(name: "FK_ProcedureStepInProcessInProcedure_ProcessInProcedure_ProcessInProcedureID", table: "ProcedureStepInProcessInProcedure");
            migrationBuilder.DropForeignKey(name: "FK_Process_Component_ComponentID", table: "Process");
            migrationBuilder.DropForeignKey(name: "FK_ProcessInProcedure_Procedure_ProcedureID", table: "ProcessInProcedure");
            migrationBuilder.DropForeignKey(name: "FK_ProcessInProcedure_Process_ProcessID", table: "ProcessInProcedure");
            migrationBuilder.DropForeignKey(name: "FK_ProcessStep_Process_ProcessID", table: "ProcessStep");
            migrationBuilder.DropForeignKey(name: "FK_Result_TestRun_TestRunID", table: "Result");
            migrationBuilder.DropForeignKey(name: "FK_Run_Group_GroupID", table: "Run");
            migrationBuilder.DropForeignKey(name: "FK_Section_Library_LibraryID", table: "Section");
            migrationBuilder.DropForeignKey(name: "FK_Set_Collection_CollectionID", table: "Set");
            migrationBuilder.DropForeignKey(name: "FK_Step_Test_TestID", table: "Step");
            migrationBuilder.DropForeignKey(name: "FK_StoredScreenshotDetails_Result_ResultID", table: "StoredScreenshotDetails");
            migrationBuilder.DropForeignKey(name: "FK_StoredStepDetails_Result_ResultID", table: "StoredStepDetails");
            migrationBuilder.DropForeignKey(name: "FK_StoredTestExceptionDetails_StoredStepDetails_StoredStepDetailsID", table: "StoredTestExceptionDetails");
            migrationBuilder.DropForeignKey(name: "FK_Suite_Section_SectionID", table: "Suite");
            migrationBuilder.DropForeignKey(name: "FK_Test_Suite_SuiteID", table: "Test");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Run_RunID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Test_TestID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRunner_TestRunnerGroup_TestRunnerGroupID", table: "TestRunner");
            migrationBuilder.DropForeignKey(name: "FK_TestRunnerLog_TestRunner_TestRunnerID", table: "TestRunnerLog");
            migrationBuilder.DropColumn(name: "StoredProcedureStepID", table: "ProcedureStepInProcessInProcedure");
            migrationBuilder.AddColumn<int>(
                name: "ProcedureStepID",
                table: "ProcedureStepInProcessInProcedure",
                nullable: false,
                defaultValue: 0);
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
                name: "FK_Area_Platform_PlatformID",
                table: "Area",
                column: "PlatformID",
                principalTable: "Platform",
                principalColumn: "PlatformID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Collection_Category_CategoryID",
                table: "Collection",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Component_Area_AreaID",
                table: "Component",
                column: "AreaID",
                principalTable: "Area",
                principalColumn: "AreaID",
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
                name: "FK_DerivedKey_MasterKey_MasterKeyID",
                table: "DerivedKey",
                column: "MasterKeyID",
                principalTable: "MasterKey",
                principalColumn: "MasterKeyID",
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
                name: "FK_Procedure_Set_SetID",
                table: "Procedure",
                column: "SetID",
                principalTable: "Set",
                principalColumn: "SetID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureStep_Procedure_ProcedureID",
                table: "ProcedureStep",
                column: "ProcedureID",
                principalTable: "Procedure",
                principalColumn: "ProcedureID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureStepInProcessInProcedure_ProcessInProcedure_ProcessInProcedureID",
                table: "ProcedureStepInProcessInProcedure",
                column: "ProcessInProcedureID",
                principalTable: "ProcessInProcedure",
                principalColumn: "ProcessInProcedureID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Process_Component_ComponentID",
                table: "Process",
                column: "ComponentID",
                principalTable: "Component",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInProcedure_Procedure_ProcedureID",
                table: "ProcessInProcedure",
                column: "ProcedureID",
                principalTable: "Procedure",
                principalColumn: "ProcedureID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInProcedure_Process_ProcessID",
                table: "ProcessInProcedure",
                column: "ProcessID",
                principalTable: "Process",
                principalColumn: "ProcessID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcessStep_Process_ProcessID",
                table: "ProcessStep",
                column: "ProcessID",
                principalTable: "Process",
                principalColumn: "ProcessID",
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
                name: "FK_Set_Collection_CollectionID",
                table: "Set",
                column: "CollectionID",
                principalTable: "Collection",
                principalColumn: "CollectionID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Step_Test_TestID",
                table: "Step",
                column: "TestID",
                principalTable: "Test",
                principalColumn: "TestID",
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
            migrationBuilder.AddForeignKey(
                name: "FK_TestRunnerLog_TestRunner_TestRunnerID",
                table: "TestRunnerLog",
                column: "TestRunnerID",
                principalTable: "TestRunner",
                principalColumn: "TestRunnerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_Area_Platform_PlatformID", table: "Area");
            migrationBuilder.DropForeignKey(name: "FK_Collection_Category_CategoryID", table: "Collection");
            migrationBuilder.DropForeignKey(name: "FK_Component_Area_AreaID", table: "Component");
            migrationBuilder.DropForeignKey(name: "FK_Dependency_DependencyGroup_DependencyGroupID", table: "Dependency");
            migrationBuilder.DropForeignKey(name: "FK_Dependency_TestRun_TestRunID", table: "Dependency");
            migrationBuilder.DropForeignKey(name: "FK_DerivedKey_MasterKey_MasterKeyID", table: "DerivedKey");
            migrationBuilder.DropForeignKey(name: "FK_Folder_Project_ProjectID", table: "Folder");
            migrationBuilder.DropForeignKey(name: "FK_Group_Folder_FolderID", table: "Group");
            migrationBuilder.DropForeignKey(name: "FK_Procedure_Set_SetID", table: "Procedure");
            migrationBuilder.DropForeignKey(name: "FK_ProcedureStep_Procedure_ProcedureID", table: "ProcedureStep");
            migrationBuilder.DropForeignKey(name: "FK_ProcedureStepInProcessInProcedure_ProcessInProcedure_ProcessInProcedureID", table: "ProcedureStepInProcessInProcedure");
            migrationBuilder.DropForeignKey(name: "FK_Process_Component_ComponentID", table: "Process");
            migrationBuilder.DropForeignKey(name: "FK_ProcessInProcedure_Procedure_ProcedureID", table: "ProcessInProcedure");
            migrationBuilder.DropForeignKey(name: "FK_ProcessInProcedure_Process_ProcessID", table: "ProcessInProcedure");
            migrationBuilder.DropForeignKey(name: "FK_ProcessStep_Process_ProcessID", table: "ProcessStep");
            migrationBuilder.DropForeignKey(name: "FK_Result_TestRun_TestRunID", table: "Result");
            migrationBuilder.DropForeignKey(name: "FK_Run_Group_GroupID", table: "Run");
            migrationBuilder.DropForeignKey(name: "FK_Section_Library_LibraryID", table: "Section");
            migrationBuilder.DropForeignKey(name: "FK_Set_Collection_CollectionID", table: "Set");
            migrationBuilder.DropForeignKey(name: "FK_Step_Test_TestID", table: "Step");
            migrationBuilder.DropForeignKey(name: "FK_StoredScreenshotDetails_Result_ResultID", table: "StoredScreenshotDetails");
            migrationBuilder.DropForeignKey(name: "FK_StoredStepDetails_Result_ResultID", table: "StoredStepDetails");
            migrationBuilder.DropForeignKey(name: "FK_StoredTestExceptionDetails_StoredStepDetails_StoredStepDetailsID", table: "StoredTestExceptionDetails");
            migrationBuilder.DropForeignKey(name: "FK_Suite_Section_SectionID", table: "Suite");
            migrationBuilder.DropForeignKey(name: "FK_Test_Suite_SuiteID", table: "Test");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Run_RunID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRun_Test_TestID", table: "TestRun");
            migrationBuilder.DropForeignKey(name: "FK_TestRunner_TestRunnerGroup_TestRunnerGroupID", table: "TestRunner");
            migrationBuilder.DropForeignKey(name: "FK_TestRunnerLog_TestRunner_TestRunnerID", table: "TestRunnerLog");
            migrationBuilder.DropColumn(name: "ProcedureStepID", table: "ProcedureStepInProcessInProcedure");
            migrationBuilder.AddColumn<int>(
                name: "StoredProcedureStepID",
                table: "ProcedureStepInProcessInProcedure",
                nullable: false,
                defaultValue: 0);
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
                name: "FK_Area_Platform_PlatformID",
                table: "Area",
                column: "PlatformID",
                principalTable: "Platform",
                principalColumn: "PlatformID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Collection_Category_CategoryID",
                table: "Collection",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Component_Area_AreaID",
                table: "Component",
                column: "AreaID",
                principalTable: "Area",
                principalColumn: "AreaID",
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
                name: "FK_DerivedKey_MasterKey_MasterKeyID",
                table: "DerivedKey",
                column: "MasterKeyID",
                principalTable: "MasterKey",
                principalColumn: "MasterKeyID",
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
                name: "FK_Procedure_Set_SetID",
                table: "Procedure",
                column: "SetID",
                principalTable: "Set",
                principalColumn: "SetID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureStep_Procedure_ProcedureID",
                table: "ProcedureStep",
                column: "ProcedureID",
                principalTable: "Procedure",
                principalColumn: "ProcedureID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureStepInProcessInProcedure_ProcessInProcedure_ProcessInProcedureID",
                table: "ProcedureStepInProcessInProcedure",
                column: "ProcessInProcedureID",
                principalTable: "ProcessInProcedure",
                principalColumn: "ProcessInProcedureID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Process_Component_ComponentID",
                table: "Process",
                column: "ComponentID",
                principalTable: "Component",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInProcedure_Procedure_ProcedureID",
                table: "ProcessInProcedure",
                column: "ProcedureID",
                principalTable: "Procedure",
                principalColumn: "ProcedureID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInProcedure_Process_ProcessID",
                table: "ProcessInProcedure",
                column: "ProcessID",
                principalTable: "Process",
                principalColumn: "ProcessID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ProcessStep_Process_ProcessID",
                table: "ProcessStep",
                column: "ProcessID",
                principalTable: "Process",
                principalColumn: "ProcessID",
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
                name: "FK_Set_Collection_CollectionID",
                table: "Set",
                column: "CollectionID",
                principalTable: "Collection",
                principalColumn: "CollectionID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Step_Test_TestID",
                table: "Step",
                column: "TestID",
                principalTable: "Test",
                principalColumn: "TestID",
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
            migrationBuilder.AddForeignKey(
                name: "FK_TestRunnerLog_TestRunner_TestRunnerID",
                table: "TestRunnerLog",
                column: "TestRunnerID",
                principalTable: "TestRunner",
                principalColumn: "TestRunnerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
