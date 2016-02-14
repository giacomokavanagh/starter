using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Group
    {
        public int GroupID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FolderID { get; set; }

        [ForeignKey("FolderID")]
        public virtual Folder Folder { get; set; }

        public virtual ICollection<Run> Runs { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? TestRunnerGroupID { get; set; }
        [ForeignKey("TestRunnerGroupID")]
        public virtual TestRunnerGroup TestRunnerGroup { get; set; }
    }
}
