using PHR.ViewModels.Common;
using PHR.ViewModels.Login;

namespace PHR.Services.EmailService
{
    public interface IEmailService
    {
        ResultViewModel SendForgotPasswordEmail(ForgotPasswordViewModel forgotPassword, string templatePath);
        bool SendEmail(EmailParameters emailParams);
        ResultViewModel SendEnquiryEmail(EnquiryEmailDetails enquiryEmail, string templatePath);
    }
}
