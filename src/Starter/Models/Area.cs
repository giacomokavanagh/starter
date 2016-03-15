﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Area
    {
        public int AreaID { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [MaxLength(5000), Required]
        public string Description { get; set; }

        public virtual ICollection<Component> Component { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlatformID { get; set; }
        [ForeignKey("PlatformID")]
        public virtual Platform Platform { get; set; }
    }
}