using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PHR.Services.EmailService;
using PHR.Services.Logger;
using PHR.Services.Login;
using PHR.Session;
using PHR.ViewModels.Common;
using PHR.ViewModels.Login;

namespace PHR.Controllers
{
    public class LoginController : Controller
    {
        #region Fields
        private readonly ILoginService loginService;
        private readonly ILoggerService logger;
        private readonly IWebHostEnvironment environment;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        #endregion

        #region Constructor
        public LoginController(ILoginService _loginService, ILoggerService _logger, IWebHostEnvironment _environment, IConfiguration _configuration, IEmailService _emailService)
        {
            loginService = _loginService;
            logger = _logger;
            environment = _environment;
            configuration = _configuration;
            emailService = _emailService;
        }
        #endregion

        #region Methods
        [HttpPost]
        public JsonResult ValidateLoginDetails(LoginCredentials credentials)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = loginService.ValidateCredentials(credentials);                
                HttpContext.Session.SetObjectAsJson("UserDetails", (LoginDetailsViewModel)result.Data);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            
            return Json(result);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(RegisterUserViewModel registerUser)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = loginService.RegisterUser(registerUser);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            return Json(result);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            ForgotPasswordViewModel data = new ForgotPasswordViewModel();

            try
            {
                ForgotPasswordDataModel dataModel = new ForgotPasswordDataModel();
                string partialPath = configuration.GetSection("EmailTemplates").GetSection("ForgotPasswordEmailPath").Value;
                string templatePath = Path.Combine(this.environment.WebRootPath, partialPath);
                resultViewModel = emailService.SendForgotPasswordEmail(forgotPassword, templatePath);

                ViewBag.Success = resultViewModel.IsSuccessful ? "Success" : "Error";
                ViewBag.Message = resultViewModel.Message;
                if (resultViewModel.IsSuccessful)
                {
                    TempData["EmailId"] = forgotPassword.UserEmail;
                }
                data.UserEmail = "";
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                ViewBag.Success = "Error";
                ViewBag.Message = "System error occured, please try later or contact Administrator";
                data.UserEmail = "";
            }
            return View(data);
        }
        
        [HttpGet]
        public IActionResult ValidateForgotPasswordRequest(int requestId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = loginService.ValidateForgotPasswordRequest(requestId);

            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            TempData["requestId"] = requestId;
            return result.IsSuccessful? RedirectToAction("SetNewPassword", result) : RedirectToAction("ForgotPassword");
        }

        [HttpGet]
        public IActionResult SetNewPassword(ResultViewModel result)
        {
            ViewBag.Success = result.IsSuccessful ? "Success" : "Error";
            ViewBag.Message = result.Message;
            SetNewPassword data = new SetNewPassword()
            {
                RequestId = Convert.ToInt32(TempData["requestId"]),
                EmailId = (string)TempData["EmailId"]
            };
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateNewPassowrd(SetNewPassword password)
        {
            ResultViewModel resultViewModel = new ResultViewModel();

            try
            {
                resultViewModel = loginService.SetNewPassword(password);
                
                if (!resultViewModel.IsSuccessful)
                {
                    TempData["requestId"] = password.RequestId;
                    TempData["EmailId"] = password.EmailId;
                }                
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                ViewBag.Success = "Error";
                ViewBag.Message = "System error occured, please try later or contact Administrator";
            }
            return resultViewModel.IsSuccessful ? RedirectToAction("ApplyNow", "Home", resultViewModel) : RedirectToAction("SetNewPassword", resultViewModel);
        }

        #endregion

    }
}
