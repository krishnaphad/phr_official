using System;
using System.Collections.Generic;
using System.Text;

namespace PHR.ViewModels.Login
{
    public class RegisterUserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; } = "User";
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public bool? IsUserActive { get; set; }
        public DateTime AddedDate { get; set; }

    }
}
