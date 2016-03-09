using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class AvailableMethod
    {
        [Display(Name="ID")]
        public int AvailableMethodID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        public virtual ICollection<Step> Steps { get; set; }

        public virtual ICollection<Test> Tests { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public virtual StoredScreenshotDetails StoredScreenshot { get; set; }
    }
}
