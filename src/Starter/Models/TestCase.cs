using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class TestCase
    {
        [Key, Display(Name = "Test Case ID")]
        public int TestCaseID { get; set; }

        [StringLength(500, MinimumLength = 1), Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public int Order { get; set; }

        public bool DisplayTestCase { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Procedure ID")]
        public int ProcedureID { get; set; }
        public virtual Procedure Procedure { get; set; }

        public ICollection<TestCaseStep> TestCaseSteps { get; set; }
    }

    public class TestCaseStep
    {
        [Key, Display(Name = "Test Case Step ID")]
        public int TestCaseStepID { get; set; }

        public string Data { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Test Case ID")]
        public int TestCaseID { get; set; }
        [ForeignKey("TestCaseID")]
        public virtual TestCase TestCase { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProcedureStepID { get; set; }
        public virtual ProcedureStep ProcedureStep { get; set; }
    }
}
