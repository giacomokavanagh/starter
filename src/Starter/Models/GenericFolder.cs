using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class GenericFolder
    {
        [Display(Name = "ID")]
        public int GenericFolderID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? ParentLocationID { get; set; }

        public bool isExpanded { get; set; }

        public virtual IEnumerable<TestEnvironment> TestEnvironments { get; set; }
    }

    public class TreeviewNodeState
    {
        [Display(Name = "ID")]
        public int TreeviewNodeStateID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? GenericFolderID { get; set; }
        public virtual GenericFolder GenericFolder { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserEmail { get; set; }

        public bool isExpanded { get; set; }
    }
}
