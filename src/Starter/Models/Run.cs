using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Run
    {
        public int RunID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupID { get; set; }

        [ForeignKey("GroupID")]
        public virtual Group Group { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<TestRun> TestRuns { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? TestRunnerGroupID { get; set; }
        [ForeignKey("TestRunnerGroupID")]
        public virtual TestRunnerGroup TestRunnerGroup { get; set; }
    }
}
