using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Process
    {
        [Display(Name = "Process ID")]
        public int ProcessID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ComponentID { get; set; }
        [ForeignKey("ComponentID")]
        public virtual Component Component { get; set; }

        public virtual ICollection<ProcessStep> ProcessSteps { get; set; }
    }
}
