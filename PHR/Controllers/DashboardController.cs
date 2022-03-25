using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PHR.Services.CommonFunctions;
using PHR.Services.Dashboard;
using PHR.Services.Logger;
using PHR.ViewModels.Common;
using PHR.ViewModels.Dashboard;

namespace PHR.Controllers
{
    public class DashboardController : Controller
    {
        #region Fields
        private readonly ILoggerService logger;
        private readonly IDashboardService dashboardService;
        private readonly IHostingEnvironment environment;
        private readonly IConfiguration configuration;
        #endregion

        #region Controller
        public DashboardController(ILoggerService _logger, IDashboardService _dashboardServic, IHostingEnvironment _environment, IConfiguration _configuration)
        {
            logger = _logger;
            dashboardService = _dashboardServic;
            environment = _environment;
            configuration = _configuration;
        }
        #endregion

        #region Dashboard Methods
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region City Master
        [HttpGet]
        public IActionResult CityMaster()
        {
            PagedList<CityMasterViewModel> cityList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = 1;
                pagingParams.PageSize = 10;
                cityList = dashboardService.GetCities(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }

            return View(cityList);
        }

        [HttpPost]
        public JsonResult AddCity(CityMasterViewModel cityMaster)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.AddCity(cityMaster);
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
        public JsonResult EditCity(int cityMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.EditCity(cityMasterId);
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
        public JsonResult DeleteCity(int cityMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.DeleteCity(cityMasterId);
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
        public JsonResult ActivateDeactivateCity(int cityMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.ActivateDeactivateCity(cityMasterId);
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
        public IActionResult GetCities(int pageNumber, int pageSize)
        {
            ResultViewModel result = new ResultViewModel();
            PagedList<CityMasterViewModel> cityList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = pageNumber;
                pagingParams.PageSize = pageSize;
                cityList = dashboardService.GetCities(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            return PartialView("_CityList", cityList);
        }

        #endregion

        #region Education Master
        public IActionResult EducationMaster()
        {
            PagedList<EducationMasterViewModel> educationList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = 1;
                pagingParams.PageSize = 10;
                educationList = dashboardService.GetEducations(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return View(educationList);
        }

        [HttpPost]
        public JsonResult AddEducation(EducationMasterViewModel educationMaster)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.AddEducation(educationMaster);
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
        public JsonResult EditEducation(int educationMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.EditEducation(educationMasterId);
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
        public JsonResult DeleteEducation(int educationMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.DeleteEducation(educationMasterId);
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
        public JsonResult ActivateDeactivateEducation(int educationMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.ActivateDeactivateEducation(educationMasterId);
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
        public IActionResult GetEducations(int pageNumber, int pageSize)
        {
            ResultViewModel result = new ResultViewModel();
            PagedList<EducationMasterViewModel> educationList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = pageNumber;
                pagingParams.PageSize = pageSize;
                educationList = dashboardService.GetEducations(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            return PartialView("_EducationList", educationList);
        }
        #endregion

        #region Key Skill Master
        public IActionResult KeySkillMaster()
        {
            PagedList<KeySkillMasterViewModel> keySkillList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = 1;
                pagingParams.PageSize = 10;
                keySkillList = dashboardService.GetKeySkills(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return View(keySkillList);
        }

        [HttpPost]
        public JsonResult AddKeySkill(KeySkillMasterViewModel keySkillMaster)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.AddKeySkill(keySkillMaster);
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
        public JsonResult EditKeySkill(int keySkillMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.EditKeySkill(keySkillMasterId);
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
        public JsonResult DeleteKeySkill(int keySkillMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.DeleteKeySkill(keySkillMasterId);
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
        public JsonResult ActivateDeactivateKeySkill(int keySkillMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.ActivateDeactivateKeySkill(keySkillMasterId);
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
        public IActionResult GetKeySkills(int pageNumber, int pageSize)
        {
            ResultViewModel result = new ResultViewModel();
            PagedList<KeySkillMasterViewModel> keySkillList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = pageNumber;
                pagingParams.PageSize = pageSize;
                keySkillList = dashboardService.GetKeySkills(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            return PartialView("_KeySkillList", keySkillList);
        }
        #endregion

        #region Company Master
        public IActionResult CompanyMaster()
        {
            PagedList<CompanyMasterViewModel> companyList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = 1;
                pagingParams.PageSize = 10;
                companyList = dashboardService.GetCompanies(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return View(companyList);
        }

        [HttpPost]
        public JsonResult AddCompany(CompanyMasterViewModel companyMaster)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.AddCompany(companyMaster);
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
        public JsonResult EditCompany(int companyMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.EditCompany(companyMasterId);
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
        public JsonResult DeleteCompany(int companyMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.DeleteCompany(companyMasterId);
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
        public JsonResult ActivateDeactivateCompany(int companyMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.ActivateDeactivateCompany(companyMasterId);
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
        public IActionResult GetCompanies(int pageNumber, int pageSize)
        {
            ResultViewModel result = new ResultViewModel();
            PagedList<CompanyMasterViewModel> companyList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = pageNumber;
                pagingParams.PageSize = pageSize;
                companyList = dashboardService.GetCompanies(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            return PartialView("_CompanyList", companyList);
        }
        #endregion

        #region  Job Master
        public IActionResult JobMaster()
        {
            PagedList<JobMasterViewModel> jobList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = 1;
                pagingParams.PageSize = 10;
                jobList = dashboardService.GetJobs(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return View(jobList);
        }

        [HttpPost]
        public JsonResult AddJob()
        {
            ResultViewModel result = new ResultViewModel();
            
            try
            {
                JobMasterViewModel jobMaster = JsonConvert.DeserializeObject<JobMasterViewModel>(Request.Form["jobData"]);
                IFormFile jdFile = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
                if(jdFile != null)
                {
                    string wwwPath = this.environment.WebRootPath;
                    string partialPath = configuration.GetSection("JDFilePath").Value;
                    string path = Path.Combine(this.environment.WebRootPath, partialPath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileName = Path.GetFileName(jdFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        jdFile.CopyTo(stream);
                    }
                }
                
                result = dashboardService.AddJob(jobMaster);
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
        public JsonResult EditJob(int jobMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.EditJob(jobMasterId);
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
        public JsonResult DeleteJob(int jobMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.DeleteJob(jobMasterId);
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
        public IActionResult GetJobs(int pageNumber, int pageSize)
        {
            ResultViewModel result = new ResultViewModel();
            PagedList<JobMasterViewModel> jobList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = pageNumber;
                pagingParams.PageSize = pageSize;
                jobList = dashboardService.GetJobs(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            return PartialView("_JobList", jobList);
        }

        [HttpGet]
        public JsonResult GetFormData()
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                var formData = new
                {
                    cityList = dashboardService.GetCities(),
                    companyList = dashboardService.GetCompanies(),
                    skillList = dashboardService.GetSkills(),
                    educationList = dashboardService.GetEducations()
                };
                result.Data = formData;
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return Json(result);
        }

        [HttpGet]
        public IActionResult DownloadJDFile(string JDFileName)
        {
            string partialPath = Path.Combine(configuration.GetSection("JDFilePath").Value, JDFileName.Trim());
            string filePath = Path.Combine(this.environment.WebRootPath, partialPath);
            var memory = new MemoryStream();

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    stream.CopyToAsync(memory).GetAwaiter();
                }
                memory.Position = 0;
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            
            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".png", "image/png"},
                {".jpg", "image/jpg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"}                
            };
        }
        #endregion

        #region  Happy Customers
        public IActionResult HappyCustomer()
        {
            PagedList<HappyCustomersViewModel> customerList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = 1;
                pagingParams.PageSize = 10;
                customerList = dashboardService.GetHappyCustomers(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return View(customerList);
        }

        [HttpPost]
        public JsonResult AddHappyCustomer()
        {
            ResultViewModel result = new ResultViewModel();

            try
            {
                HappyCustomersViewModel customerData = JsonConvert.DeserializeObject<HappyCustomersViewModel>(Request.Form["happyCustomerData"]);
                IFormFile companyLogoFile = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
                if (companyLogoFile != null)
                {
                    string wwwPath = this.environment.WebRootPath;
                    string partialPath = configuration.GetSection("LogoFilePath").Value;
                    string path = Path.Combine(this.environment.WebRootPath, partialPath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileName = Path.GetFileName(companyLogoFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        companyLogoFile.CopyTo(stream);
                    }
                }

                result = dashboardService.AddHappyCustomer(customerData);
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
        public JsonResult EditHappyCustomer(int happyCustomerId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.EditHappyCustomer(happyCustomerId);
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
        public JsonResult DeleteHappyCustomer(int happyCustomerId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.DeleteHappyCustomer(happyCustomerId);
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
        public IActionResult GetHappyCustomers(int pageNumber, int pageSize)
        {
            ResultViewModel result = new ResultViewModel();
            PagedList<HappyCustomersViewModel> happyCustomerList = null;
            try
            {
                PagingParams pagingParams = new PagingParams();
                pagingParams.PageNumber = pageNumber;
                pagingParams.PageSize = pageSize;
                happyCustomerList = dashboardService.GetHappyCustomers(pagingParams);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            return PartialView("_HappyCustomersList", happyCustomerList);
        }
                
        [HttpGet]
        public IActionResult DownloadLogoFile(string logoFileName)
        {
            string partialPath = Path.Combine(configuration.GetSection("LogoFilePath").Value, logoFileName.Trim());
            string filePath = Path.Combine(this.environment.WebRootPath, partialPath);
            var memory = new MemoryStream();
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    stream.CopyToAsync(memory).GetAwaiter();
                }
                memory.Position = 0;
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);                
            }
            
            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
        }

        public IActionResult GetHappyCustomerList()
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                result = dashboardService.GetHappyCustomerList();
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            return Json(result);
        }
        #endregion

    }
}
