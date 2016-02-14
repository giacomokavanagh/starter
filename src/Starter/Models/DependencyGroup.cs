using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class DependencyGroup
    {
        public int DependencyGroupID { get; set; }

        [StringLength(500, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }


        public virtual ICollection<Dependency> Dependencies { get; set; }
    }
}
