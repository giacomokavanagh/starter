using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class TestRun : IValidatableObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestRunID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RunID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? DependencyGroupID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? TestEnvironmentID { get; set; }

        [StringLength(50, MinimumLength = 1), Required]
        public string Browser { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Browser != "Chrome" &&
                Browser != "Firefox" &&
                Browser != "IE")
            {
                yield return new ValidationResult
              ("That browser isn't supported", new[] { "Browser" });
            }

            if (Status != "Passed" &&
                Status != "Failed" &&
                Status != "Blocked" &&
                Status != "Running" &&
                Status != "Waiting" &&
                Status != "Ready" &&
                Status != "Unassigned")
            {
                yield return new ValidationResult
              ("That status isn't supported", new[] { "Status" });
            }
        }

        [Display(Name = "Start Time")]
        public DateTime? StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        [StringLength(50, MinimumLength = 1), Required]
        public string Status { get; set; }

        public virtual Result Result { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Retries { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None), Display(Name = "Retries Left")]
        public int? RetriesLeft { get; set; }

        public int? TestRunner { get; set; }

        public virtual Test Test { get; set; }
        public virtual Run Run { get; set; }
        public virtual TestEnvironment TestEnvironment { get; set; }
        public virtual DependencyGroup DependencyGroup { get; set; }

        public int? TestRunnerGroupID { get; set; }
        public virtual TestRunnerGroup TestRunnerGroup { get; set; }

        public virtual ICollection<DependencyGroup> ValidDependencyGroups { get; set; }
    }
}
