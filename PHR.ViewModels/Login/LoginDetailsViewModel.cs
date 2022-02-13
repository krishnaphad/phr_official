using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PHR.ViewModels.Login
{
    public class LoginDetailsViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public bool? IsUserActive { get; set; }
        public DateTime AddedDate { get; set; }
    }

    public class LoginCredentials
    {
        public string UserEmail { get; set; }

        public string UserPassword { get; set; }
    }
}
