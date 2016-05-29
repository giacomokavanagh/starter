using Starter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.ViewModels.PageObject
{
    public class ObjectLibraryDetailsViewModel
    {
        public ObjectLibrary ObjectLibrary { get; set; }
        public ICollection<Models.PageObject> PageObjects { get; set; }
        public Models.PageObject NewPageObject{ get; set; }
    }
}
