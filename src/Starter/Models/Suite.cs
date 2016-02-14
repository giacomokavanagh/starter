using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Suite
    {
        public int SuiteID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectionID { get; set; }

        [ForeignKey("SectionID")]
        public virtual Section Section { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
