using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class ProcedureStep
    {
        [Display(Name = "ID")]
        public int ProcedureStepID { get; set; }

        public int ProcedureID { get; set; }
        [ForeignKey("ProcedureID")]
        public virtual Procedure Procedure { get; set; }

        [Display(Name = "Step ID"), Required]
        public string StepID { get; set; }

        [Required]
        public string Method { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }
        public string Input { get; set; }
        public int Order { get; set; }

        public bool Static { get; set; }

        public int? ProcessStepID { get; set; }
        public virtual ProcessStep ProcessStep { get; set; }

        public bool? MatchesProcessStep { get; set; }

        public virtual ICollection<TestCaseStep> TestCaseSteps { get; set; }
    }
}
