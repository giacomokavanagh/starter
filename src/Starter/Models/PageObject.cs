using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class PageObject
    {
        [Display(Name="ID")]
        public int PageObjectID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Method { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ObjectLibraryID { get; set; }
        public virtual ObjectLibrary ObjectLibrary { get; set; }

        public virtual ICollection<TagLink> TagLinks { get; set; }
    }
}
