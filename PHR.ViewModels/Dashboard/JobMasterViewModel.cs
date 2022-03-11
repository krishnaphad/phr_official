using System;
using System.Collections.Generic;
using System.Text;

namespace PHR.ViewModels.Dashboard
{
    public class JobMasterViewModel
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobKeySkills { get; set; }
        public string JobCity { get; set; }
        public string JobExperienceRequired { get; set; }
        public string JobSalary { get; set; }
        public string JobEducationRequired { get; set; }
        public int JobCompanyId { get; set; }
        public string JobCompanyName { get; set; }
        public string JobLocationAddress { get; set; }
        public string JobDescriptionFileName { get; set; }
        public string JobDescriptionFilePath { get; set; }
        public DateTime JobAddedDate { get; set; }
    }

    public class AutoComplete
    {
        public string Label  { get; set; }
        public string Value { get; set; }        
    }
}
