using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class JobApplication
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string ApplicantName { get; set; }
        public string PositionApplied { get; set; }
        public string ContactNumber { get; set; }
        public string ApplicantEmail { get; set; }
        public double ApplicantExperiance { get; set; }
        public string CurrentCompany { get; set; }
        public double CurrentCtc { get; set; }
        public string Qualification { get; set; }
        public int NoticePeriod { get; set; }
        public bool IsCandidateHired { get; set; }
        public DateTime AppliedDate { get; set; }

        public virtual JobsCollection Job { get; set; }
        public virtual LoginDetail User { get; set; }
    }
}
