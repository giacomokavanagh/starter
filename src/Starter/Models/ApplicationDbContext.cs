using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Starter.Models;

namespace Starter.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //builder.Entity<Project>()
              //  .HasOne(p => p.TestRunnerGroup).WithMany(t => t.Projects).IsRequired(false);
            //builder.Entity<TestRunnerGroup>()
              //  .HasMany(p => p.Projects).WithOne(s => s.TestRunnerGroup).IsRequired(false);
        }
        public DbSet<Project> Project { get; set; }
        public DbSet<Folder> Folder { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Run> Run { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Suite> Suite { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<TestRun> TestRun { get; set; }
        public DbSet<TestEnvironment> TestEnvironment { get; set; }
        public DbSet<TestRunner> TestRunner { get; set; }
        public DbSet<TestRunnerGroup> TestRunnerGroup { get; set; }
        public DbSet<DependencyGroup> DependencyGroup { get; set; }
        public DbSet<Dependency> Dependency { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<StoredScreenshotDetails> StoredScreenshotDetails { get; set; }
        public DbSet<StoredStepDetails> StoredStepDetails { get; set; }
        public DbSet<StoredTestExceptionDetails> StoredTestExceptionDetails { get; set; }
        public DbSet<TestRunnerLog> TestRunnerLog { get; set; }
        public DbSet<MasterKey> MasterKey { get; set; }
        public DbSet<DerivedKey> DerivedKey { get; set; }
        public DbSet<Step> Step { get; set; }
        public DbSet<AvailableMethod> AvailableMethod { get; set; }
    }
}
