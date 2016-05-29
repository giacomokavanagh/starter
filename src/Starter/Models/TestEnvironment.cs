using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class TestEnvironment
    {
        public int TestEnvironmentID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        public string ContentType { get; set; }

        [Display(Name = "XML File")]
        public string XMLFilePath { get; set; }

        public int? GenericFolderID { get; set; }
        public virtual GenericFolder GenericFolder { get; set; }

        public virtual ICollection<TestRun> TestRuns { get; set; }
    }
}
