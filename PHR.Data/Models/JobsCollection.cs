using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class JobsCollection
    {
        public JobsCollection()
        {
            JobApplications = new HashSet<JobApplication>();
        }

        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobKeySkills { get; set; }
        public string JobCity { get; set; }
        public string JobExperienceRequired { get; set; }
        public string JobSalary { get; set; }
        public string JobEducationRequired { get; set; }
        public int JobCompanyId { get; set; }
        public string JobLocationAddress { get; set; }
        public DateTime JobAddedDate { get; set; }

        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}
