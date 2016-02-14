using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class Result
    {
        [Display(Name = "Result ID")]
        public int ResultID { get; set; }

        [Display(Name = "Result")]
        public string ResultName { get; set; }
        public string ResultDirectory { get; set; }
        public string ScreenshotsDirectory { get; set; }

        [Display(Name = "Status")]
        public string StoredStatus { get; set; }

        [Display(Name = "Steps Passed")]
        public int StepsPassed { get; set; }
        [Display(Name = "Steps Failed")]
        public int StepsFailed { get; set; }
        [Display(Name = "Steps Blocked")]
        public int StepsBlocked { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StoredStartTime { get; set; }
        [Display(Name = "End Time")]
        public DateTime StoredEndTime { get; set; }
        public DateTime Duration { get; set; }

        [Display(Name = "Browser")]
        public string StoredBrowser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Test Run ID")]
        public int TestRunID { get; set; }
        public virtual TestRun TestRun { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Test ID")]
        public int StoredTestID { get; set; }
        [Display(Name = "Test Name")]
        public string StoredTestName { get; set; }
        [Display(Name = "Test Data File")]
        public string StoredTestDataFileName { get; set; }
        public string StoredTestDataFileContentType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Test Environment ID")]
        public int StoredTestEnvironmentID { get; set; }
        [Display(Name = "Test Environment")]
        public string StoredTestEnvironmentName { get; set; }
        [Display(Name = "Environment File")]
        public string StoredTestEnvironmentFileName { get; set; }
        public string StoredTestEnvironmentFileContentType { get; set; }

        public virtual List<StoredScreenshotDetails> ScreenshotDetailsList { get; set; }

        public virtual List<StoredStepDetails> StepDetailsList { get; set; }

        public int StoredTestRunnerID { get; set; }
        [Display(Name = "Test Runner")]
        public string StoredTestRunnerName { get; set; }
    }

    public class StoredScreenshotDetails
    {
        public int ResultID { get; set; }
        public int StoredScreenshotDetailsID { get; set; }
        public string StepID { get; set; }
        public string StoredScreenshotFilePath { get; set; }
        public int Order { get; set; }
    }

    public class StoredStepDetails
    {
        public int ResultID { get; set; }
        public int StoredStepDetailsID { get; set; }
        public string StoredStepStatus { get; set; }
        public string StepID { get; set; }
        public string Method { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }
        public string Input { get; set; }
        public string StepStartTime { get; set; }
        public string StepEndTime { get; set; }
        public bool CatastrophicFailure { get; set; }
        public virtual List<StoredTestExceptionDetails> listTestExceptionDetails { get; set; }
        public int Order { get; set; }
    }

    public class StoredTestExceptionDetails
    {
        [Key]
        public int ExceptionID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StoredStepDetailsID { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
