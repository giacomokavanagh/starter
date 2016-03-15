using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class ProcessStep
    {
        [Display(Name = "ID")]
        public int ProcessStepID { get; set; }

        public int ProcessID { get; set; }
        [ForeignKey("ProcessID")]
        public virtual Process Process { get; set; }

        [Display(Name = "Step ID"), Required]
        public string StepID { get; set; }

        [Required]
        public string Method { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }
        public string Input { get; set; }
        public int Order { get; set; }

        public bool Static { get; set; }
    }
}
