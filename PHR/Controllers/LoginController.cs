using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        #endregion

        #region Constructor
        public LoginController(ILoginService _loginService, ILoggerService _logger)
        {
            loginService = _loginService;
            logger = _logger;
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

        public IActionResult ForgotPassword()
        {
            return View();
        }

        #endregion

    }
}
