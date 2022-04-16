using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class ForgotPassword
    {
        public int ForgotPasswordId { get; set; }
        public string RequestedUserEmail { get; set; }
        public string SystemGeneratedPassword { get; set; }
        public bool IsLinkActive { get; set; }
        public DateTime LinkCreatedDate { get; set; }
    }
}
