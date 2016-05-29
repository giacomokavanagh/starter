using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class ObjectLibrary
    {
        public int ObjectLibraryID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<PageObject> PageObjects { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenericFolderID { get; set; }
        public virtual GenericFolder GenericFolder { get; set; }

        public virtual ICollection<TagLink> TagLinks { get; set; }
    }
}
