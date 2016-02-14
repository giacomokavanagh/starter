using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class TestRunner
    {
        public int TestRunnerID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [StringLength(5000, MinimumLength = 1), Required]
        [Display(Name = "Configuration Details")]
        public string ConfigurationDetails { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestRunnerGroupID { get; set; }
        [ForeignKey("TestRunnerGroupID")]
        public virtual TestRunnerGroup TestRunnerGroup { get; set; }

        [Display(Name = "Clear Remote Log")]
        public bool ClearRemoteLogOnNextAccess { get; set; }

        public virtual ICollection<TestRunnerLog> TestRunnerLogs { get; set; }
    }

    public class TestRunnerLog
    {
        [Display(Name = "ID")]
        public int TestRunnerLogID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestRunnerID { get; set; }
        [ForeignKey("TestRunnerID")]
        public virtual TestRunner TestRunner { get; set; }
        public string FilePath { get; set; }
        public string Filename { get; set; }
        public string DateTime { get; set; }
    }
}
