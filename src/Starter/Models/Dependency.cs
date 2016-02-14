using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Dependency
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DependencyID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DependencyGroupID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestRunID { get; set; }

        public virtual DependencyGroup DependencyGroup { get; set; }
        public virtual TestRun TestRun { get; set; }
    }
}
