using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Test : IValidatableObject
    {
        public int TestID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SuiteID { get; set; }

        [StringLength(50, MinimumLength = 1), Required]
        [Display(Name = "Test Data Source")]
        public string TestDataSource { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TestDataSource != "Excel" &&
                TestDataSource != "Starter")
            {
                yield return new ValidationResult
              ("That data source isn't recognised", new[] { "TestDataSource" });
            }
        }

        [Display(Name = "Excel File")]
        public string ExcelFilePath { get; set; }

        public string ContentType { get; set; }

        [ForeignKey("SuiteID")]
        public virtual Suite Suite { get; set; }

        public virtual ICollection<TestRun> TestRuns { get; set; }
    }
}
