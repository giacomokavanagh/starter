using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class TestRunnerGroup
    {
        public int TestRunnerGroupID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [StringLength(1000, MinimumLength = 1), Required]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Projects")]
        public virtual ICollection<Project> Projects { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Folders")]
        public virtual ICollection<Folder> Folders { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Groups")]
        public virtual ICollection<Group> Groups { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Runs")]
        public virtual ICollection<Run> Runs { get; set; }

        public virtual ICollection<TestRunner> TestRunners { get; set; }
    }
}
