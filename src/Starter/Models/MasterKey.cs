using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class MasterKey
    {
        [Key]
        [Display(Name = "Master Key ID")]
        public int MasterKeyID { get; set; }
        [Display(Name = "Master Key String")]
        public string MasterKeyString { get; set; }

        public virtual ICollection<DerivedKey> DerivedKeys { get; set; }
    }

    public class DerivedKey
    {
        [Display(Name = "Derived Key ID")]
        public int DerivedKeyID { get; set; }

        [Display(Name = "Derived Key String")]
        public string DerivedKeyString { get; set; }

        public int MasterKeyID { get; set; }
        [ForeignKey("MasterKeyID")]
        public virtual MasterKey MasterKey { get; set; }

        public int? TestRunnerID { get; set; }
        public virtual TestRunner TestRunner { get; set; }
    }
}
