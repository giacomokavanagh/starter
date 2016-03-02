using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Starter.Models;

namespace Starter.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160302142912_TestRunnersToKeys")]
    partial class TestRunnersToKeys
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

            modelBuilder.Entity("Starter.Models.Test", b =>
                {
                    b.Property<int>("TestID")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("Starter.Models.TestEnvironment", b =>
                {
                    b.Property<int>("TestEnvironmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5000);

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

            modelBuilder.Entity("Starter.Models.Project", b =>
                {
                    b.HasOne("Starter.Models.TestRunnerGroup")
                        .WithMany()
                        .HasForeignKey("TestRunnerGroupID");
                });

            modelBuilder.Entity("Starter.Models.Result", b =>
                {
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

            modelBuilder.Entity("Starter.Models.Test", b =>
                {
                    b.HasOne("Starter.Models.Run")
                        .WithMany()
                        .HasForeignKey("RunRunID");

                    b.HasOne("Starter.Models.Suite")
                        .WithMany()
                        .HasForeignKey("SuiteID");
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
        }
    }
}
