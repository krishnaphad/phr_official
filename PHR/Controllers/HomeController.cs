using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PHR.Models;
using PHR.ViewModels.Common;
using PHR.ViewModels.Login;
using Microsoft.AspNetCore.Http;

namespace PHR.Controllers
{
    public class HomeController : Controller
    {
        #region Fileds
        private readonly ILogger<HomeController> _logger;
        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Methods
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Careers()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult ApplyNow(ResultViewModel ResultViewModel)
        {

            LoginDetailsViewModel userDetails = PHR.Session.SessionExtensions.GetObjectFromJson<LoginDetailsViewModel>(HttpContext.Session, "UserDetails");
            if (userDetails != null)
            {
                return RedirectToAction("JobSearch");
            }
            ViewBag.Success = ResultViewModel.IsSuccessful ? "Success" : "Error";
            ViewBag.Message = ResultViewModel.Message;
            return View();
        }

        public IActionResult JobSearch()
        {
            LoginDetailsViewModel userDetails = PHR.Session.SessionExtensions.GetObjectFromJson<LoginDetailsViewModel>(HttpContext.Session, "UserDetails");
            if (userDetails == null)
            {
                return RedirectToAction("ApplyNow");
            }
            return View();
        }
        #endregion
    }
}
