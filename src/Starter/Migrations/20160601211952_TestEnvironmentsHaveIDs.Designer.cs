using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Starter.Models;

namespace Starter.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160601211952_TestEnvironmentsHaveIDs")]
    partial class TestEnvironmentsHaveIDs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("Starter.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Starter.Models.Area", b =>
                {
                    b.Property<int>("AreaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("PlatformID");

                    b.HasKey("AreaID");
                });

            modelBuilder.Entity("Starter.Models.AvailableMethod", b =>
                {
                    b.Property<int>("AvailableMethodID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("StoredScreenshotStoredScreenshotDetailsID");

                    b.HasKey("AvailableMethodID");
                });

            modelBuilder.Entity("Starter.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("CategoryID");
                });

            modelBuilder.Entity("Starter.Models.Collection", b =>
                {
                    b.Property<int>("CollectionID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("CollectionID");
                });

            modelBuilder.Entity("Starter.Models.Component", b =>
                {
                    b.Property<int>("ComponentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("ComponentID");
                });

            modelBuilder.Entity("Starter.Models.Dependency", b =>
                {
                    b.Property<int>("DependencyID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DependencyGroupID");

                    b.Property<int>("TestRunID");

                    b.HasKey("DependencyID");
                });

            modelBuilder.Entity("Starter.Models.DependencyGroup", b =>
                {
                    b.Property<int>("DependencyGroupID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<int?>("TestRunTestRunID");

                    b.HasKey("DependencyGroupID");
                });

            modelBuilder.Entity("Starter.Models.DerivedKey", b =>
                {
                    b.Property<int>("DerivedKeyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DerivedKeyString");

                    b.Property<int>("MasterKeyID");

                    b.Property<int?>("TestRunnerID");

                    b.HasKey("DerivedKeyID");
                });

            modelBuilder.Entity("Starter.Models.Folder", b =>
                {
                    b.Property<int>("FolderID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("ProjectID");

                    b.Property<int?>("TestRunnerGroupID");

                    b.HasKey("FolderID");
                });

            modelBuilder.Entity("Starter.Models.GenericFolder", b =>
                {
                    b.Property<int>("GenericFolderID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("ParentLocationID");

                    b.Property<bool>("isExpanded");

                    b.HasKey("GenericFolderID");
                });

            modelBuilder.Entity("Starter.Models.Group", b =>
                {
                    b.Property<int>("GroupID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<int>("FolderID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("TestRunnerGroupID");

                    b.HasKey("GroupID");
                });

            modelBuilder.Entity("Starter.Models.Library", b =>
                {
                    b.Property<int>("LibraryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("LibraryID");
                });

            modelBuilder.Entity("Starter.Models.MasterKey", b =>
                {
                    b.Property<int>("MasterKeyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MasterKeyString");

                    b.HasKey("MasterKeyID");
                });

            modelBuilder.Entity("Starter.Models.ObjectLibrary", b =>
                {
                    b.Property<int>("ObjectLibraryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("GenericFolderID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ObjectLibraryID");
                });

            modelBuilder.Entity("Starter.Models.PageObject", b =>
                {
                    b.Property<int>("PageObjectID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attribute");

                    b.Property<string>("Description");

                    b.Property<string>("Method")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ObjectLibraryID");

                    b.Property<string>("Value");

                    b.HasKey("PageObjectID");
                });

            modelBuilder.Entity("Starter.Models.Platform", b =>
                {
                    b.Property<int>("PlatformID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("PlatformID");
                });

            modelBuilder.Entity("Starter.Models.Procedure", b =>
                {
                    b.Property<int>("ProcedureID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<bool>("DisplayStaticSteps");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SetID");

                    b.HasKey("ProcedureID");
                });

            modelBuilder.Entity("Starter.Models.ProcedureStep", b =>
                {
                    b.Property<int>("ProcedureStepID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attribute");

                    b.Property<string>("Input");

                    b.Property<bool?>("MatchesProcessStep");

                    b.Property<string>("Method")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.Property<int>("ProcedureID");

                    b.Property<int?>("ProcessStepID");

                    b.Property<bool>("Static");

                    b.Property<string>("StepID")
                        .IsRequired();

                    b.Property<string>("Value");

                    b.HasKey("ProcedureStepID");
                });

            modelBuilder.Entity("Starter.Models.ProcedureStepInProcessInProcedure", b =>
                {
                    b.Property<int>("ProcedureStepInProcessInProcedureID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProcedureStepID");

                    b.Property<int>("ProcessInProcedureID");

                    b.HasKey("ProcedureStepInProcessInProcedureID");
                });

            modelBuilder.Entity("Starter.Models.Process", b =>
                {
                    b.Property<int>("ProcessID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComponentID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("ProcessID");
                });

            modelBuilder.Entity("Starter.Models.ProcessInProcedure", b =>
                {
                    b.Property<int>("ProcessInProcedureID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProcedureID");

                    b.Property<int>("ProcessID");

                    b.HasKey("ProcessInProcedureID");
                });

            modelBuilder.Entity("Starter.Models.ProcessStep", b =>
                {
                    b.Property<int>("ProcessStepID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attribute");

                    b.Property<string>("Input");

                    b.Property<string>("Method")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.Property<int>("ProcessID");

                    b.Property<bool>("Static");

                    b.Property<string>("StepID")
                        .IsRequired();

                    b.Property<string>("Value");

                    b.HasKey("ProcessStepID");
                });

            modelBuilder.Entity("Starter.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("TestRunnerGroupID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("Starter.Models.Result", b =>
                {
                    b.Property<int>("ResultID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AvailableMethodAvailableMethodID");

                    b.Property<DateTime>("Duration");

                    b.Property<string>("ResultDirectory");

                    b.Property<string>("ResultName");

                    b.Property<string>("ScreenshotsDirectory");

                    b.Property<int>("StepsBlocked");

                    b.Property<int>("StepsFailed");

                    b.Property<int>("StepsPassed");

                    b.Property<string>("StoredBrowser");

                    b.Property<DateTime>("StoredEndTime");

                    b.Property<DateTime>("StoredStartTime");

                    b.Property<string>("StoredStatus");

                    b.Property<string>("StoredTestDataFileContentType");

                    b.Property<string>("StoredTestDataFileName");

                    b.Property<string>("StoredTestEnvironmentFileContentType");

                    b.Property<string>("StoredTestEnvironmentFileName");

                    b.Property<int>("StoredTestEnvironmentID");

                    b.Property<string>("StoredTestEnvironmentName");

                    b.Property<int>("StoredTestID");

                    b.Property<string>("StoredTestName");

                    b.Property<int>("StoredTestRunnerID");

                    b.Property<string>("StoredTestRunnerName");

                    b.Property<int>("TestRunID");

                    b.HasKey("ResultID");
                });

            modelBuilder.Entity("Starter.Models.Run", b =>
                {
                    b.Property<int>("RunID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<int>("GroupID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("TestRunnerGroupID");

                    b.HasKey("RunID");
                });

            modelBuilder.Entity("Starter.Models.Section", b =>
                {
                    b.Property<int>("SectionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<int>("LibraryID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("SectionID");
                });

            modelBuilder.Entity("Starter.Models.Set", b =>
                {
                    b.Property<int>("SetID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CollectionID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("SetID");
                });

            modelBuilder.Entity("Starter.Models.Step", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attribute");

                    b.Property<int?>("AvailableMethodAvailableMethodID");

                    b.Property<string>("Input");

                    b.Property<string>("Method")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.Property<string>("StepID")
                        .IsRequired();

                    b.Property<int>("TestID");

                    b.Property<string>("Value");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("Starter.Models.StoredScreenshotDetails", b =>
                {
                    b.Property<int>("StoredScreenshotDetailsID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Order");

                    b.Property<int>("ResultID");

                    b.Property<string>("StepID");

                    b.Property<string>("StoredScreenshotFilePath");

                    b.HasKey("StoredScreenshotDetailsID");
                });

            modelBuilder.Entity("Starter.Models.StoredStepDetails", b =>
                {
                    b.Property<int>("StoredStepDetailsID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attribute");

                    b.Property<bool>("CatastrophicFailure");

                    b.Property<string>("Input");

                    b.Property<string>("Method");

                    b.Property<int>("Order");

                    b.Property<int>("ResultID");

                    b.Property<string>("StepEndTime");

                    b.Property<string>("StepID");

                    b.Property<string>("StepStartTime");

                    b.Property<string>("StoredStepStatus");

                    b.Property<string>("Value");

                    b.HasKey("StoredStepDetailsID");
                });

            modelBuilder.Entity("Starter.Models.StoredTestExceptionDetails", b =>
                {
                    b.Property<int>("ExceptionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExceptionMessage");

                    b.Property<string>("ExceptionType");

                    b.Property<int>("StoredStepDetailsID");

                    b.HasKey("ExceptionID");
                });

            modelBuilder.Entity("Starter.Models.Suite", b =>
                {
                    b.Property<int>("SuiteID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SectionID");

                    b.HasKey("SuiteID");
                });

            modelBuilder.Entity("Starter.Models.Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("TagGroupID");

                    b.HasKey("TagID");
                });

            modelBuilder.Entity("Starter.Models.TagGroup", b =>
                {
                    b.Property<int>("TagGroupID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TagGroupID");
                });

            modelBuilder.Entity("Starter.Models.TagLink", b =>
                {
                    b.Property<int>("TagLinkID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ObjectLibraryID");

                    b.Property<int?>("PageObjectID");

                    b.Property<int>("TagID");

                    b.HasKey("TagLinkID");
                });

            modelBuilder.Entity("Starter.Models.Test", b =>
                {
                    b.Property<int>("TestID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AvailableMethodAvailableMethodID");

                    b.Property<string>("ContentType");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("ExcelFilePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("RunRunID");

                    b.Property<int>("SuiteID");

                    b.Property<string>("TestDataSource")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("TestID");
                });

            modelBuilder.Entity("Starter.Models.TestCase", b =>
                {
                    b.Property<int>("TestCaseID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("DisplayTestCase");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<int>("Order");

                    b.Property<int>("ProcedureID");

                    b.HasKey("TestCaseID");
                });

            modelBuilder.Entity("Starter.Models.TestCaseStep", b =>
                {
                    b.Property<int>("TestCaseStepID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<int>("ProcedureStepID");

                    b.Property<int>("TestCaseID");

                    b.HasKey("TestCaseStepID");
                });

            modelBuilder.Entity("Starter.Models.TestEnvironment", b =>
                {
                    b.Property<int>("TestEnvironmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<int?>("GenericFolderID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("XMLFilePath");

                    b.HasKey("TestEnvironmentID");
                });

            modelBuilder.Entity("Starter.Models.TestRun", b =>
                {
                    b.Property<int>("TestRunID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int?>("DependencyGroupID");

                    b.Property<DateTime?>("EndTime");

                    b.Property<int?>("Retries");

                    b.Property<int?>("RetriesLeft");

                    b.Property<int>("RunID");

                    b.Property<DateTime?>("StartTime");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int?>("TestEnvironmentID");

                    b.Property<int>("TestID");

                    b.Property<int?>("TestRunner");

                    b.Property<int?>("TestRunnerGroupID");

                    b.HasKey("TestRunID");
                });

            modelBuilder.Entity("Starter.Models.TestRunner", b =>
                {
                    b.Property<int>("TestRunnerID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ClearRemoteLogOnNextAccess");

                    b.Property<string>("ConfigurationDetails")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("TakeScreenshots");

                    b.Property<int>("TestRunnerGroupID");

                    b.HasKey("TestRunnerID");
                });

            modelBuilder.Entity("Starter.Models.TestRunnerGroup", b =>
                {
                    b.Property<int>("TestRunnerGroupID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("TestRunnerGroupID");
                });

            modelBuilder.Entity("Starter.Models.TestRunnerLog", b =>
                {
                    b.Property<int>("TestRunnerLogID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DateTime");

                    b.Property<string>("FilePath");

                    b.Property<string>("Filename");

                    b.Property<int>("TestRunnerID");

                    b.HasKey("TestRunnerLogID");
                });

            modelBuilder.Entity("Starter.Models.TreeviewNodeState", b =>
                {
                    b.Property<int>("TreeviewNodeStateID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GenericFolderID");

                    b.Property<string>("UserEmail");

                    b.Property<bool>("isExpanded");

                    b.HasKey("TreeviewNodeStateID");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Starter.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Starter.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Starter.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Starter.Models.Area", b =>
                {
                    b.HasOne("Starter.Models.Platform")
                        .WithMany()
                        .HasForeignKey("PlatformID");
                });

            modelBuilder.Entity("Starter.Models.AvailableMethod", b =>
                {
                    b.HasOne("Starter.Models.StoredScreenshotDetails")
                        .WithMany()
                        .HasForeignKey("StoredScreenshotStoredScreenshotDetailsID");
                });

            modelBuilder.Entity("Starter.Models.Collection", b =>
                {
                    b.HasOne("Starter.Models.Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");
                });

            modelBuilder.Entity("Starter.Models.Component", b =>
                {
                    b.HasOne("Starter.Models.Area")
                        .WithMany()
                        .HasForeignKey("AreaID");
                });

            modelBuilder.Entity("Starter.Models.Dependency", b =>
                {
                    b.HasOne("Starter.Models.DependencyGroup")
                        .WithMany()
                        .HasForeignKey("DependencyGroupID");

                    b.HasOne("Starter.Models.TestRun")
                        .WithMany()
                        .HasForeignKey("TestRunID");
                });

            modelBuilder.Entity("Starter.Models.DependencyGroup", b =>
                {
                    b.HasOne("Starter.Models.TestRun")
                        .WithMany()
                        .HasForeignKey("TestRunTestRunID");
                });

            modelBuilder.Entity("Starter.Models.DerivedKey", b =>
                {
                    b.HasOne("Starter.Models.MasterKey")
                        .WithMany()
                        .HasForeignKey("MasterKeyID");

                    b.HasOne("Starter.Models.TestRunner")
                        .WithOne()
                        .HasForeignKey("Starter.Models.DerivedKey", "TestRunnerID");
                });

            modelBuilder.Entity("Starter.Models.Folder", b =>
                {
                    b.HasOne("Starter.Models.Project")
                        .WithMany()
                        .HasForeignKey("ProjectID");

                    b.HasOne("Starter.Models.TestRunnerGroup")
                        .WithMany()
                        .HasForeignKey("TestRunnerGroupID");
                });

            modelBuilder.Entity("Starter.Models.Group", b =>
                {
                    b.HasOne("Starter.Models.Folder")
                        .WithMany()
                        .HasForeignKey("FolderID");

                    b.HasOne("Starter.Models.TestRunnerGroup")
                        .WithMany()
                        .HasForeignKey("TestRunnerGroupID");
                });

            modelBuilder.Entity("Starter.Models.ObjectLibrary", b =>
                {
                    b.HasOne("Starter.Models.GenericFolder")
                        .WithMany()
                        .HasForeignKey("GenericFolderID");
                });

            modelBuilder.Entity("Starter.Models.PageObject", b =>
                {
                    b.HasOne("Starter.Models.ObjectLibrary")
                        .WithMany()
                        .HasForeignKey("ObjectLibraryID");
                });

            modelBuilder.Entity("Starter.Models.Procedure", b =>
                {
                    b.HasOne("Starter.Models.Set")
                        .WithMany()
                        .HasForeignKey("SetID");
                });

            modelBuilder.Entity("Starter.Models.ProcedureStep", b =>
                {
                    b.HasOne("Starter.Models.Procedure")
                        .WithMany()
                        .HasForeignKey("ProcedureID");

                    b.HasOne("Starter.Models.ProcessStep")
                        .WithMany()
                        .HasForeignKey("ProcessStepID");
                });

            modelBuilder.Entity("Starter.Models.ProcedureStepInProcessInProcedure", b =>
                {
                    b.HasOne("Starter.Models.ProcessInProcedure")
                        .WithMany()
                        .HasForeignKey("ProcessInProcedureID");
                });

            modelBuilder.Entity("Starter.Models.Process", b =>
                {
                    b.HasOne("Starter.Models.Component")
                        .WithMany()
                        .HasForeignKey("ComponentID");
                });

            modelBuilder.Entity("Starter.Models.ProcessInProcedure", b =>
                {
                    b.HasOne("Starter.Models.Procedure")
                        .WithMany()
                        .HasForeignKey("ProcedureID");

                    b.HasOne("Starter.Models.Process")
                        .WithMany()
                        .HasForeignKey("ProcessID");
                });

            modelBuilder.Entity("Starter.Models.ProcessStep", b =>
                {
                    b.HasOne("Starter.Models.Process")
                        .WithMany()
                        .HasForeignKey("ProcessID");
                });

            modelBuilder.Entity("Starter.Models.Project", b =>
                {
                    b.HasOne("Starter.Models.TestRunnerGroup")
                        .WithMany()
                        .HasForeignKey("TestRunnerGroupID");
                });

            modelBuilder.Entity("Starter.Models.Result", b =>
                {
                    b.HasOne("Starter.Models.AvailableMethod")
                        .WithMany()
                        .HasForeignKey("AvailableMethodAvailableMethodID");

                    b.HasOne("Starter.Models.TestRun")
                        .WithOne()
                        .HasForeignKey("Starter.Models.Result", "TestRunID");
                });

            modelBuilder.Entity("Starter.Models.Run", b =>
                {
                    b.HasOne("Starter.Models.Group")
                        .WithMany()
                        .HasForeignKey("GroupID");

                    b.HasOne("Starter.Models.TestRunnerGroup")
                        .WithMany()
                        .HasForeignKey("TestRunnerGroupID");
                });

            modelBuilder.Entity("Starter.Models.Section", b =>
                {
                    b.HasOne("Starter.Models.Library")
                        .WithMany()
                        .HasForeignKey("LibraryID");
                });

            modelBuilder.Entity("Starter.Models.Set", b =>
                {
                    b.HasOne("Starter.Models.Collection")
                        .WithMany()
                        .HasForeignKey("CollectionID");
                });

            modelBuilder.Entity("Starter.Models.Step", b =>
                {
                    b.HasOne("Starter.Models.AvailableMethod")
                        .WithMany()
                        .HasForeignKey("AvailableMethodAvailableMethodID");

                    b.HasOne("Starter.Models.Test")
                        .WithMany()
                        .HasForeignKey("TestID");
                });

            modelBuilder.Entity("Starter.Models.StoredScreenshotDetails", b =>
                {
                    b.HasOne("Starter.Models.Result")
                        .WithMany()
                        .HasForeignKey("ResultID");
                });

            modelBuilder.Entity("Starter.Models.StoredStepDetails", b =>
                {
                    b.HasOne("Starter.Models.Result")
                        .WithMany()
                        .HasForeignKey("ResultID");
                });

            modelBuilder.Entity("Starter.Models.StoredTestExceptionDetails", b =>
                {
                    b.HasOne("Starter.Models.StoredStepDetails")
                        .WithMany()
                        .HasForeignKey("StoredStepDetailsID");
                });

            modelBuilder.Entity("Starter.Models.Suite", b =>
                {
                    b.HasOne("Starter.Models.Section")
                        .WithMany()
                        .HasForeignKey("SectionID");
                });

            modelBuilder.Entity("Starter.Models.Tag", b =>
                {
                    b.HasOne("Starter.Models.TagGroup")
                        .WithMany()
                        .HasForeignKey("TagGroupID");
                });

            modelBuilder.Entity("Starter.Models.TagLink", b =>
                {
                    b.HasOne("Starter.Models.ObjectLibrary")
                        .WithMany()
                        .HasForeignKey("ObjectLibraryID");

                    b.HasOne("Starter.Models.PageObject")
                        .WithMany()
                        .HasForeignKey("PageObjectID");

                    b.HasOne("Starter.Models.Tag")
                        .WithOne()
                        .HasForeignKey("Starter.Models.TagLink", "TagID");
                });

            modelBuilder.Entity("Starter.Models.Test", b =>
                {
                    b.HasOne("Starter.Models.AvailableMethod")
                        .WithMany()
                        .HasForeignKey("AvailableMethodAvailableMethodID");

                    b.HasOne("Starter.Models.Run")
                        .WithMany()
                        .HasForeignKey("RunRunID");

                    b.HasOne("Starter.Models.Suite")
                        .WithMany()
                        .HasForeignKey("SuiteID");
                });

            modelBuilder.Entity("Starter.Models.TestCase", b =>
                {
                    b.HasOne("Starter.Models.Procedure")
                        .WithMany()
                        .HasForeignKey("ProcedureID");
                });

            modelBuilder.Entity("Starter.Models.TestCaseStep", b =>
                {
                    b.HasOne("Starter.Models.ProcedureStep")
                        .WithMany()
                        .HasForeignKey("ProcedureStepID");

                    b.HasOne("Starter.Models.TestCase")
                        .WithMany()
                        .HasForeignKey("TestCaseID");
                });

            modelBuilder.Entity("Starter.Models.TestEnvironment", b =>
                {
                    b.HasOne("Starter.Models.GenericFolder")
                        .WithMany()
                        .HasForeignKey("GenericFolderID");
                });

            modelBuilder.Entity("Starter.Models.TestRun", b =>
                {
                    b.HasOne("Starter.Models.DependencyGroup")
                        .WithMany()
                        .HasForeignKey("DependencyGroupID");

                    b.HasOne("Starter.Models.Run")
                        .WithMany()
                        .HasForeignKey("RunID");

                    b.HasOne("Starter.Models.TestEnvironment")
                        .WithMany()
                        .HasForeignKey("TestEnvironmentID");

                    b.HasOne("Starter.Models.Test")
                        .WithMany()
                        .HasForeignKey("TestID");

                    b.HasOne("Starter.Models.TestRunnerGroup")
                        .WithMany()
                        .HasForeignKey("TestRunnerGroupID");
                });

            modelBuilder.Entity("Starter.Models.TestRunner", b =>
                {
                    b.HasOne("Starter.Models.TestRunnerGroup")
                        .WithMany()
                        .HasForeignKey("TestRunnerGroupID");
                });

            modelBuilder.Entity("Starter.Models.TestRunnerLog", b =>
                {
                    b.HasOne("Starter.Models.TestRunner")
                        .WithMany()
                        .HasForeignKey("TestRunnerID");
                });

            modelBuilder.Entity("Starter.Models.TreeviewNodeState", b =>
                {
                    b.HasOne("Starter.Models.GenericFolder")
                        .WithMany()
                        .HasForeignKey("GenericFolderID");
                });
        }
    }
}
