using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Set
    {
        public int SetID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        public virtual ICollection<Procedure> Procedure { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CollectionID { get; set; }
        [ForeignKey("CollectionID")]
        public virtual Collection Collection { get; set; }
    }
}
