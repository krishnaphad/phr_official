using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using PHR.Data.Models;
using PHR.Services.Logger;
using PHR.ViewModels.Common;
using PHR.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PHR.Services.EmailService
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly string fromEmail = string.Empty;
        private readonly string password = string.Empty;
        private readonly int smtpPort;
        private readonly string smtpServer = string.Empty;
        private readonly IConfiguration configuration;
        private readonly ILoggerService logger;
        private readonly phr_dbContext dbContext;
        private const int SaltKey = 12;

        #endregion

        #region Constructore
        public EmailService(IConfiguration _configuration, ILoggerService _logger, phr_dbContext _dbContext)
        {
            configuration = _configuration;
            fromEmail = configuration.GetSection("Mailkit_Details").GetSection("MailFrom").Value;
            password = configuration.GetSection("Mailkit_Details").GetSection("SMTPPassword").Value;
            smtpPort = Convert.ToInt32(configuration.GetSection("Mailkit_Details").GetSection("SMTPPort").Value);
            smtpServer = configuration.GetSection("Mailkit_Details").GetSection("SMTPServer").Value;
            logger = _logger;
            dbContext = _dbContext;
        }
        #endregion

        #region Methods
        public ResultViewModel SendForgotPasswordEmail(ForgotPasswordViewModel forgotPassword, string templatePath)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                ForgotPasswordDataModel dataModel = new ForgotPasswordDataModel();

                var user = dbContext.LoginDetails.FirstOrDefault(u => u.UserEmail.ToLower().Equals(forgotPassword.UserEmail.ToLower()));
                if (user != null)
                {
                    string tempPass = GenerateSystemPassword();
                    dataModel.Passsword = BCrypt.Net.BCrypt.HashPassword(tempPass, SaltKey);

                    ForgotPassword forgotData = new ForgotPassword()
                    {
                        RequestedUserEmail = forgotPassword.UserEmail,
                        SystemGeneratedPassword = dataModel.Passsword,
                        IsLinkActive = true,
                        LinkCreatedDate = DateTime.Now.Date
                    };

                    dbContext.ForgotPasswords.Add(forgotData);
                    dbContext.SaveChanges();

                    dataModel.WebLink = forgotPassword.WebLink + "/Login/ValidateForgotPasswordRequest?requestId=" + forgotData.ForgotPasswordId;
                    dataModel.ResetLink = dataModel.WebLink;
                    dataModel.Passsword = tempPass;

                    List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("@webAppLink@", dataModel.WebLink),
                        new KeyValuePair<string, string>("@EmailID@", forgotPassword.UserEmail),
                        new KeyValuePair<string, string>("@SystemGeneratedPassword@", dataModel.Passsword),
                        new KeyValuePair<string, string>("@ResetPasswordLink@", dataModel.WebLink)
                    };

                    EmailParameters emailParams = new EmailParameters();
                    emailParams.Body = GetEmailTemplate(keyValues, templatePath);
                    emailParams.To = forgotPassword.UserEmail;
                    emailParams.From = fromEmail;
                    emailParams.Subject = "Forgot Password Recovery";
                    SendEmail(emailParams);
                    result.IsSuccessful = true;
                    result.Message = "Password recovery email sent on your registerd email";
                }
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }

            return result;
        }

        private string GetEmailTemplate(List<KeyValuePair<string, string>> replaceValues, string templatePath)
        {
            var emailFormat = new StreamReader(templatePath);
            var builder = new StringBuilder(emailFormat.ReadToEnd());

            foreach (var keyValue in replaceValues)
            {
                builder.Replace(keyValue.Key, keyValue.Value);
            }
            return builder.ToString();
        }

        private string GenerateSystemPassword()
        {
            string SystemPassword = string.Empty;
            try
            {
                Guid Guid = Guid.NewGuid();
                string[] GuidArray = Guid.ToString().Split('-');
                SystemPassword = string.Concat(GuidArray[0], GuidArray[1]);

            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return SystemPassword;
        }

        public bool SendEmail(EmailParameters emailParams)
        {
            bool isEmailSent = false;
            try
            {
                // create email message
                MimeMessage email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(fromEmail));
                email.To.Add(MailboxAddress.Parse(emailParams.To));
                email.Subject = emailParams.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = emailParams.Body };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(fromEmail, password);
                smtp.Send(email);
                smtp.Disconnect(true);
                isEmailSent = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isEmailSent;
        }

        public ResultViewModel SendEnquiryEmail(EnquiryEmailDetails enquiryEmail, string templatePath)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("@@Name@@", enquiryEmail.Name),
                        new KeyValuePair<string, string>("@@Email@@", enquiryEmail.Email),
                        new KeyValuePair<string, string>("@@Enquiry@@", enquiryEmail.EnquiryDetails)  
                    };

                EmailParameters emailParams = new EmailParameters();
                emailParams.Body = GetEmailTemplate(keyValues, templatePath);
                emailParams.To = fromEmail;
                emailParams.From = fromEmail;
                emailParams.Subject = "Enquiry Email";
                SendEmail(emailParams);
                result.IsSuccessful = true;
                result.Message = "Enquiry email sent successfuly";
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            return result;
        }
        #endregion

    }
}
