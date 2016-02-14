using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Project
    {
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        public virtual ICollection<Folder> Folders { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Test Runner Group")]
        public int? TestRunnerGroupID { get; set; }
        [ForeignKey("TestRunnerGroupID")]
        public virtual TestRunnerGroup TestRunnerGroup { get; set; }
    }
}
