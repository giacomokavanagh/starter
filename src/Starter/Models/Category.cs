using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Category
    {
        [Display(Name = "ID")]
        public int CategoryID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        public virtual ICollection<Collection> Collection { get; set; }
    }
}
