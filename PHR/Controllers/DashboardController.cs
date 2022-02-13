using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PHR.Controllers
{
    public class DashboardController : Controller
    {
        #region Fields

        #endregion

        #region Controller
        public DashboardController()
        {

        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CityMaster()
        {
            return View();
        }

        public IActionResult EducationMaster()
        {
            return View();
        }

        public IActionResult KeySkillMaster()
        {
            return View();
        }

        public IActionResult CompanMaster()
        {
            return View();
        }

        public IActionResult JobMaster()
        {
            return View();
        }
        #endregion

    }
}
