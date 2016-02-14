using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Section
    {
        public int SectionID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LibraryID { get; set; }
        [ForeignKey("LibraryID")]
        public virtual Library Library { get; set; }

        public virtual ICollection<Suite> Suites { get; set; }
    }
}
