using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Tag
    {
        public int TagID { get; set; }

        [Required]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? TagGroupID { get; set; }
        public virtual TagGroup TagGroup { get; set; }

        public virtual TagLink TagLink { get; set; }
    }

    public class TagLink
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagLinkID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TagID { get; set; }
        public virtual Tag Tag { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? PageObjectID { get; set; }
        public virtual PageObject PageObject { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? ObjectLibraryID { get; set; }
        public virtual ObjectLibrary ObjectLibrary { get; set; }
    }

    public class TagGroup
    {
        public int TagGroupID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
