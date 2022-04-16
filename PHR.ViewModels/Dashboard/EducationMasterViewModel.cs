using System;

namespace PHR.ViewModels.Dashboard
{
    public class EducationMasterViewModel
    {
        public int EducationId { get; set; }
        public string EducationName { get; set; }
        public bool IsEducationActive { get; set; }
        public DateTime EducationAddedDate { get; set; }
    }
}
