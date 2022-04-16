using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class LoginDetail
    {
        public LoginDetail()
        {
            JobApplications = new HashSet<JobApplication>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
        public bool? IsUserActive { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}
