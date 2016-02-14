using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Folder
    {
        public int FolderID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? TestRunnerGroupID { get; set; }
        [ForeignKey("TestRunnerGroupID")]
        public virtual TestRunnerGroup TestRunnerGroup { get; set; }
    }
}
