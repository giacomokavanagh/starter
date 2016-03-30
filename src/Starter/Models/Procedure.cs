using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Procedure
    {
        [Display(Name = "Procedure ID")]
        public int ProcedureID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        [Display(Name = "Locked")]
        public bool IsLocked { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SetID { get; set; }
        [ForeignKey("SetID")]
        public virtual Set Set { get; set; }

        public bool DisplayStaticSteps { get; set; }

        public virtual ICollection<ProcedureStep> ProcedureSteps { get; set; }

        public virtual ICollection<ProcessInProcedure> ProcessInProcedures { get; set; }

        public virtual ICollection<TestCase> TestCases { get; set; }
    }

    public class ProcessInProcedure
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProcessInProcedureID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProcessID { get; set; }
        public virtual Process Process { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProcedureID { get; set; }
        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<ProcedureStepInProcessInProcedure> ProcedureStepsInProcessInProcedures { get; set; }
    }

    public class ProcedureStepInProcessInProcedure
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProcedureStepInProcessInProcedureID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProcedureStepID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProcessInProcedureID { get; set; }

        public virtual ProcessInProcedure ProcessInProcedure { get; set; }
    }
}
