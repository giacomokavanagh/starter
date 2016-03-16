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

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SetID { get; set; }
        [ForeignKey("SetID")]
        public virtual Set Set { get; set; }

        public virtual ICollection<ProcedureStep> ProcedureStep { get; set; }
    }
}
