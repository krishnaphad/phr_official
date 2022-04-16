using AutoMapper;
//using Newtonsoft.Json;
using PHR.Data.Models;
using PHR.Services.CommonFunctions;
using PHR.Services.Logger;
using PHR.ViewModels.Common;
using PHR.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHR.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        #region Fields
        readonly phr_dbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILoggerService logger;
        #endregion

        #region Constructor
        public DashboardService(phr_dbContext _dbContext, IMapper _mapper, ILoggerService _logger)
        {
            dbContext = _dbContext;
            mapper = _mapper;
            logger = _logger;
        }
        #endregion

        #region City Master Methods
        public ResultViewModel AddCity(CityMasterViewModel cityMaster)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                CityMaster city = mapper.Map<CityMaster>(cityMaster);
                city.CityAddedDate = DateTime.Now.Date;
                if (cityMaster.CityId > 0)
                {
                    //Update existing city
                    CityMaster tempCity = dbContext.CityMasters.FirstOrDefault(c => c.CityId == city.CityId);
                    tempCity.CityName = city.CityName;
                    tempCity.CityIsActive = city.CityIsActive;
                    tempCity.CityAddedDate = city.CityAddedDate;

                    dbContext.CityMasters.Update(tempCity);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "City updated successfuly";
                }
                else
                {
                    //Add new city
                    CityMaster tempCity = dbContext.CityMasters.FirstOrDefault(c => c.CityName == city.CityName);
                    if (tempCity == null)
                    {
                        dbContext.CityMasters.Add(city);
                        dbContext.SaveChanges();
                        result.IsSuccessful = true;
                        result.Message = "New city added successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Message = "City with this name already added, please try with different name";
                    }
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

        public ResultViewModel DeleteCity(int cityMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                CityMaster city = dbContext.CityMasters.FirstOrDefault(C => C.CityId == cityMasterId);
                if (city != null)
                {
                    dbContext.CityMasters.Remove(city);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "City record deleted successfuly";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to delete";
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

        public ResultViewModel EditCity(int cityMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                CityMaster city = dbContext.CityMasters.FirstOrDefault(C => C.CityId == cityMasterId);
                if (city != null)
                {
                    CityMasterViewModel cityMaster = mapper.Map<CityMasterViewModel>(city);
                    result.IsSuccessful = true;
                    result.Data = cityMaster;
                    result.Message = "City record loaded for edit";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found for edit";
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

        public ResultViewModel ActivateDeactivateCity(int cityMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                CityMaster city = dbContext.CityMasters.FirstOrDefault(C => C.CityId == cityMasterId);
                if (city != null)
                {
                    city.CityIsActive = city.CityIsActive == true ? false : true;
                    dbContext.CityMasters.Update(city);
                    dbContext.SaveChanges();
                    if (city.CityIsActive == true)
                    {
                        result.IsSuccessful = true;
                        result.Message = "City activated successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = true;
                        result.Message = "City deactivated successfuly";
                    }
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to update";
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

        public PagedList<CityMasterViewModel> GetCities(PagingParams pagingParams)
        {
            PagedList<CityMasterViewModel> pagedList = null;
            
            try
            {
                var query = dbContext.CityMasters.ToList();
                var tempCities = mapper.Map<List<CityMasterViewModel>>(query);

                pagedList = new PagedList<CityMasterViewModel>(
                    tempCities.AsQueryable(), pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return pagedList;
        }
        #endregion

        #region Education Master Methods
        public ResultViewModel AddEducation(EducationMasterViewModel educationMaster)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                EducationMaster education = mapper.Map<EducationMaster>(educationMaster);
                education.EducationAddedDate = DateTime.Now.Date;
                if (education.EducationId > 0)
                {
                    //Update existing education
                    EducationMaster tempEducation = dbContext.EducationMasters.FirstOrDefault(c => c.EducationId == education.EducationId);
                    tempEducation.EducationName = education.EducationName;
                    tempEducation.IsEducationActive = education.IsEducationActive;

                    dbContext.EducationMasters.Update(tempEducation);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Degree updated successfuly";
                }
                else
                {
                    //Add new education
                    EducationMaster tempEducation = dbContext.EducationMasters.FirstOrDefault(c => c.EducationName == education.EducationName);
                    if (tempEducation == null)
                    {
                        dbContext.EducationMasters.Add(education);
                        dbContext.SaveChanges();
                        result.IsSuccessful = true;
                        result.Message = "New degree added successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Message = "Degree with this name already added, please try with different name";
                    }
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

        public ResultViewModel DeleteEducation(int educationMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                EducationMaster education = dbContext.EducationMasters.FirstOrDefault(C => C.EducationId == educationMasterId);
                if (education != null)
                {
                    dbContext.EducationMasters.Remove(education);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Degree record deleted successfuly";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to delete";
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

        public ResultViewModel EditEducation(int educationMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                EducationMaster education = dbContext.EducationMasters.FirstOrDefault(C => C.EducationId == educationMasterId);
                if (education != null)
                {
                    EducationMasterViewModel educationMaster = mapper.Map<EducationMasterViewModel>(education);
                    result.IsSuccessful = true;
                    result.Data = educationMaster;
                    result.Message = "Degree record loaded for edit";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found for edit";
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

        public ResultViewModel ActivateDeactivateEducation(int educationMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                EducationMaster education = dbContext.EducationMasters.FirstOrDefault(C => C.EducationId == educationMasterId);
                if (education != null)
                {
                    education.IsEducationActive = education.IsEducationActive == true ? false : true;
                    dbContext.EducationMasters.Update(education);
                    dbContext.SaveChanges();
                    if (education.IsEducationActive == true)
                    {
                        result.IsSuccessful = true;
                        result.Message = "Degree activated successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = true;
                        result.Message = "Degree deactivated successfuly";
                    }
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to update";
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

        public PagedList<EducationMasterViewModel> GetEducations(PagingParams pagingParams)
        {
            PagedList<EducationMasterViewModel> pagedList = null;

            try
            {
                var query = dbContext.EducationMasters.ToList();
                var tempEducations = mapper.Map<List<EducationMasterViewModel>>(query);

                pagedList = new PagedList<EducationMasterViewModel>(
                    tempEducations.AsQueryable(), pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return pagedList;
        }
        #endregion

        #region Key Skill Master Methods
        public ResultViewModel AddKeySkill(KeySkillMasterViewModel keySkillMaster)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                KeySkillMaster keySkill = mapper.Map<KeySkillMaster>(keySkillMaster);
                keySkill.KeySkillAddedDate = DateTime.Now.Date;
                if (keySkill.KeySkillId > 0)
                {
                    //Update existing key skill
                    KeySkillMaster tempKeySkill = dbContext.KeySkillMasters.FirstOrDefault(c => c.KeySkillId == keySkill.KeySkillId);
                    tempKeySkill.KeySkillName = keySkill.KeySkillName;
                    tempKeySkill.IsKeySkillActive = keySkill.IsKeySkillActive;

                    dbContext.KeySkillMasters.Update(tempKeySkill);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Key skill updated successfuly";
                }
                else
                {
                    //Add new key skill
                    KeySkillMaster tempKeySkill = dbContext.KeySkillMasters.FirstOrDefault(c => c.KeySkillName.ToLower() == keySkill.KeySkillName.ToLower());
                    if (tempKeySkill == null)
                    {
                        dbContext.KeySkillMasters.Add(keySkill);
                        dbContext.SaveChanges();
                        result.IsSuccessful = true;
                        result.Message = "New key skill added successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Message = "Key skill with this name already added, please try with different name";
                    }
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

        public ResultViewModel DeleteKeySkill(int keySkillMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                KeySkillMaster keySkill = dbContext.KeySkillMasters.FirstOrDefault(C => C.KeySkillId == keySkillMasterId);
                if (keySkill != null)
                {
                    dbContext.KeySkillMasters.Remove(keySkill);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Key skill record deleted successfuly";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to delete";
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

        public ResultViewModel EditKeySkill(int keySkillMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                KeySkillMaster keySkill = dbContext.KeySkillMasters.FirstOrDefault(C => C.KeySkillId == keySkillMasterId);
                if (keySkill != null)
                {
                    KeySkillMasterViewModel keySkillMaster = mapper.Map<KeySkillMasterViewModel>(keySkill);
                    result.IsSuccessful = true;
                    result.Data = keySkillMaster;
                    result.Message = "Key skill record loaded for edit";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found for edit";
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

        public ResultViewModel ActivateDeactivateKeySkill(int keySkillMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                KeySkillMaster keySkill = dbContext.KeySkillMasters.FirstOrDefault(C => C.KeySkillId == keySkillMasterId);
                if (keySkill != null)
                {
                    keySkill.IsKeySkillActive = keySkill.IsKeySkillActive == true ? false : true;
                    dbContext.KeySkillMasters.Update(keySkill);
                    dbContext.SaveChanges();
                    if (keySkill.IsKeySkillActive == true)
                    {
                        result.IsSuccessful = true;
                        result.Message = "Key skill activated successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = true;
                        result.Message = "Key skill deactivated successfuly";
                    }
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to update";
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

        public PagedList<KeySkillMasterViewModel> GetKeySkills(PagingParams pagingParams)
        {
            PagedList<KeySkillMasterViewModel> pagedList = null;

            try
            {
                var query = dbContext.KeySkillMasters.ToList();
                var tempKeySkills = mapper.Map<List<KeySkillMasterViewModel>>(query);

                pagedList = new PagedList<KeySkillMasterViewModel>(
                    tempKeySkills.AsQueryable(), pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return pagedList;
        }
        #endregion

        #region  Company Master Methods
        public ResultViewModel AddCompany(CompanyMasterViewModel companyMaster)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                CompanyMaster company = mapper.Map<CompanyMaster>(companyMaster);
                company.CompanyAddedDate = DateTime.Now.Date;
                if (company.CompanyId > 0)
                {
                    //Update existing company
                    CompanyMaster tempCompany = dbContext.CompanyMasters.FirstOrDefault(c => c.CompanyId == company.CompanyId);
                    tempCompany.CompanyName = company.CompanyName;
                    tempCompany.IsCompanyActive = company.IsCompanyActive;

                    dbContext.CompanyMasters.Update(tempCompany);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Company updated successfuly";
                }
                else
                {
                    //Add new company
                    CompanyMaster tempCompany = dbContext.CompanyMasters.FirstOrDefault(c => c.CompanyName.ToLower() == company.CompanyName.ToLower());
                    if (tempCompany == null)
                    {
                        dbContext.CompanyMasters.Add(company);
                        dbContext.SaveChanges();
                        result.IsSuccessful = true;
                        result.Message = "New company added successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Message = "Company with this name already added, please try with different name";
                    }
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

        public ResultViewModel DeleteCompany(int companyMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                CompanyMaster company = dbContext.CompanyMasters.FirstOrDefault(C => C.CompanyId == companyMasterId);
                if (company != null)
                {
                    dbContext.CompanyMasters.Remove(company);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Company record deleted successfuly";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to delete";
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

        public ResultViewModel EditCompany(int companyMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                CompanyMaster company = dbContext.CompanyMasters.FirstOrDefault(C => C.CompanyId == companyMasterId);
                if (company != null)
                {
                    CompanyMasterViewModel companyMaster = mapper.Map<CompanyMasterViewModel>(company);
                    result.IsSuccessful = true;
                    result.Data = companyMaster;
                    result.Message = "Company record loaded for edit";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found for edit";
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

        public ResultViewModel ActivateDeactivateCompany(int companyMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                CompanyMaster company = dbContext.CompanyMasters.FirstOrDefault(C => C.CompanyId == companyMasterId);
                if (company != null)
                {
                    company.IsCompanyActive = company.IsCompanyActive == true ? false : true;
                    dbContext.CompanyMasters.Update(company);
                    dbContext.SaveChanges();
                    if (company.IsCompanyActive == true)
                    {
                        result.IsSuccessful = true;
                        result.Message = "Company activated successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = true;
                        result.Message = "Company deactivated successfuly";
                    }
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to update";
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

        public PagedList<CompanyMasterViewModel> GetCompanies(PagingParams pagingParams)
        {
            PagedList<CompanyMasterViewModel> pagedList = null;

            try
            {
                var query = dbContext.CompanyMasters.ToList();
                var tempCompany = mapper.Map<List<CompanyMasterViewModel>>(query);

                pagedList = new PagedList<CompanyMasterViewModel>(
                    tempCompany.AsQueryable(), pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return pagedList;
        }
        #endregion

        #region  Job Master Methods
        public ResultViewModel AddJob(JobMasterViewModel jobMaster)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                JobsCollection job = mapper.Map<JobsCollection>(jobMaster);
                job.JobAddedDate = DateTime.Now.Date;
                job.JobCompanyId = dbContext.CompanyMasters.FirstOrDefault(c => c.CompanyName == jobMaster.JobCompanyName).CompanyId;
                if (job.JobId > 0)
                {
                    //Update existing job
                    //JobsCollection tempJob = dbContext.JobsCollections.FirstOrDefault(c => c.CompanyId == company.CompanyId);
                    //tempJob.CompanyName = company.CompanyName;
                    //tempJob.IsCompanyActive = company.IsCompanyActive;

                    dbContext.JobsCollections.Update(job);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Job updated successfuly";
                }
                else
                {
                    //Add new company
                    JobsCollection tempJob = dbContext.JobsCollections.FirstOrDefault(c => c.JobTitle.ToLower() == job.JobTitle.ToLower());
                    if (tempJob == null)
                    {
                        dbContext.JobsCollections.Add(job);
                        dbContext.SaveChanges();
                        result.IsSuccessful = true;
                        result.Message = "New job added successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Message = "Job with this title already added, please try with different one";
                    }
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

        public ResultViewModel DeleteJob(int jobMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                JobsCollection job = dbContext.JobsCollections.FirstOrDefault(C => C.JobId == jobMasterId);
                if (job != null)
                {
                    dbContext.JobsCollections.Remove(job);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Job deleted successfuly";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to delete";
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

        public ResultViewModel EditJob(int jobMasterId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                JobsCollection job = dbContext.JobsCollections.FirstOrDefault(C => C.JobId == jobMasterId);
                if (job != null)
                {
                    JobMasterViewModel jobMaster = mapper.Map<JobMasterViewModel>(job);
                    jobMaster.JobCompanyName = dbContext.CompanyMasters.FirstOrDefault(c => c.CompanyId == jobMaster.JobCompanyId).CompanyName;
                    result.IsSuccessful = true;
                    result.Data = jobMaster;
                    result.Message = "Jobs details loaded for edit";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found for edit";
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

        public PagedList<JobMasterViewModel> GetJobs(PagingParams pagingParams)
        {
            PagedList<JobMasterViewModel> pagedList = null;

            try
            {
                var queryNew = (from a in dbContext.JobsCollections
                                join b in dbContext.CompanyMasters on a.JobCompanyId equals b.CompanyId
                                select new JobMasterViewModel
                                {
                                    JobId = a.JobId,
                                    JobTitle = a.JobTitle,
                                    JobCompanyId = b.CompanyId,
                                    JobCompanyName = b.CompanyName
                                }).ToList();

                //var query = dbContext.JobsCollections.ToList();
                //var tempJob = mapper.Map<List<JobMasterViewModel>>(query);
                
                //var tempJob = mapper.Map<List<JobMasterViewModel>>(queryNew);

                pagedList = new PagedList<JobMasterViewModel>(
                    queryNew.AsQueryable(), pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return pagedList;
        }
        
        public List<AutoComplete> GetCities()
        {
            List<AutoComplete> cityList = new List<AutoComplete>();
            try
            {
                cityList = dbContext.CityMasters.Where(x => x.CityIsActive == true).Select(x => new AutoComplete
                {
                    Label = x.CityName,
                    Value = x.CityName
                }).ToList();
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return cityList;
        }

        public List<AutoComplete> GetCompanies()
        {
            List<AutoComplete> companyList = new List<AutoComplete>();
            try
            {
                companyList = dbContext.CompanyMasters.Where(x => x.IsCompanyActive == true).Select(x => new AutoComplete
                {
                    Label = x.CompanyName,
                    Value = x.CompanyName
                }).ToList();
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return companyList;
        }

        public List<AutoComplete> GetSkills()
        {
            List<AutoComplete> skillList = new List<AutoComplete>();
            try
            {
                skillList = dbContext.KeySkillMasters.Where(x => x.IsKeySkillActive == true).Select(x => new AutoComplete
                {
                    Label = x.KeySkillName,
                    Value = x.KeySkillName
                }).ToList();
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return skillList;
        }

        public List<AutoComplete> GetEducations()
        {
            List<AutoComplete> educationList = new List<AutoComplete>();
            try
            {
                educationList = dbContext.EducationMasters.Where(x => x.IsEducationActive == true).Select(x => new AutoComplete
                {
                    Label = x.EducationName,
                    Value = x.EducationName
                }).ToList();
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return educationList;
        }
        #endregion

        #region  Happy Customers Methods
        public ResultViewModel AddHappyCustomer(HappyCustomersViewModel happyCustomerr)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                HappyCustomer customer = mapper.Map<HappyCustomer>(happyCustomerr);
                
                if (customer.HappyCustomerId > 0)
                {
                    //Update existing happy customer                    
                    dbContext.HappyCustomers.Update(customer);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Customer updated successfuly";
                }
                else
                {
                    //Add new happy customer
                    HappyCustomer tempCustomer = dbContext.HappyCustomers.FirstOrDefault(c => c.HappyCustomerCompanyName.ToLower() == customer.HappyCustomerCompanyName.ToLower());
                    if (tempCustomer == null)
                    {
                        dbContext.HappyCustomers.Add(customer);
                        dbContext.SaveChanges();
                        result.IsSuccessful = true;
                        result.Message = "New customer added successfuly";
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Message = "Customer with this name already added, please try with different name";
                    }
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

        public ResultViewModel DeleteHappyCustomer(int happyCustomerId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                HappyCustomer customer = dbContext.HappyCustomers.FirstOrDefault(C => C.HappyCustomerId == happyCustomerId);
                if (customer != null)
                {
                    dbContext.HappyCustomers.Remove(customer);
                    dbContext.SaveChanges();
                    result.IsSuccessful = true;
                    result.Message = "Happy customer record deleted successfuly";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found to delete";
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

        public ResultViewModel EditHappyCustomer(int happyCustomerId)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                HappyCustomer customer = dbContext.HappyCustomers.FirstOrDefault(C => C.HappyCustomerId == happyCustomerId);
                if (customer != null)
                {
                    HappyCustomersViewModel happyCustomer = mapper.Map<HappyCustomersViewModel>(customer);
                    result.IsSuccessful = true;
                    result.Data = happyCustomer;
                    result.Message = "Happy customer record loaded for edit";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "No record found for edit";
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

        public PagedList<HappyCustomersViewModel> GetHappyCustomers(PagingParams pagingParams)
        {
            PagedList<HappyCustomersViewModel> pagedList = null;

            try
            {
                var query = dbContext.HappyCustomers.ToList();
                var tempCustomer = mapper.Map<List<HappyCustomersViewModel>>(query);

                pagedList = new PagedList<HappyCustomersViewModel>(
                    tempCustomer.AsQueryable(), pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception ex)
            {
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
            }
            return pagedList;
        }

        public ResultViewModel GetHappyCustomerList()
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                List<HappyCustomer> happyCustomers = dbContext.HappyCustomers.ToList();
                result.IsSuccessful = true;
                result.Data = mapper.Map<List<HappyCustomersViewModel>>(happyCustomers);
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
