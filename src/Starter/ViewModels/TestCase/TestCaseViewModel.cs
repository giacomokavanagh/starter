using Starter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.ViewModels.TestCase
{
    public class TestCasesForProcedureViewModel
    {
        public Procedure Procedure { get; set; }
        public Models.TestCase NewTestCase { get; set; }
    }

    public class ManageTestCasesForProcedureViewModel
    {
        public Procedure Procedure { get; set; }
        public Models.TestCase NewTestCase { get; set; }
        public GenerateTestCasesFromProcedure GenerateTestCasesFromProcedure { get; set; }
    }

    public class GenerateTestCasesFromProcedure
    {
        public int ProcedureID { get; set; }
        [Display(Name = "Suite")]
        public int SuiteID { get; set; }
        public bool Overwrite { get; set; }
    }
}
