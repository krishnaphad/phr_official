using PHR.ViewModels.Common;
using PHR.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace PHR.Services.EmailService
{
    public interface IEmailService
    {
        ResultViewModel SendForgotPasswordEmail(ForgotPasswordViewModel forgotPassword, string templatePath);
        bool SendEmail(EmailParameters emailParams);
        ResultViewModel SendEnquiryEmail(EnquiryEmailDetails enquiryEmail, string templatePath);
    }
}
