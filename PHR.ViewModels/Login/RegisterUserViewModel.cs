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

    public class ForgotPasswordViewModel
    {
        public string UserEmail { get; set; }
        public string WebLink { get; set; }

    }
    public class ForgotPasswordDataModel
    {
        public string WebLink { get; set; }
        public string ResetLink { get; set; }
        public string Passsword { get; set; }
    }

    public class EmailParameters
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class SetNewPassword
    {
        public int RequestId { get; set; }
        public string NewPasswod { get; set; }
        public string EmailId { get; set; }
    }
}
