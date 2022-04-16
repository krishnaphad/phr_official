using PHR.Services.CommonFunctions;
using PHR.ViewModels.Common;
using PHR.ViewModels.Dashboard;
using System.Collections.Generic;

namespace PHR.Services.Dashboard
{
    public interface IDashboardService
    {
        #region City Master
        ResultViewModel AddCity(CityMasterViewModel cityMaster);
        ResultViewModel EditCity(int cityMasterId);
        ResultViewModel DeleteCity(int cityMasterId);
        ResultViewModel ActivateDeactivateCity(int cityMasterId);
        PagedList<CityMasterViewModel> GetCities(PagingParams pagingParams);
        #endregion

        #region Education Master
        ResultViewModel AddEducation(EducationMasterViewModel educationMaster);
        ResultViewModel EditEducation(int educationMasterId);
        ResultViewModel DeleteEducation(int educationMasterId);
        ResultViewModel ActivateDeactivateEducation(int educationMasterId);
        PagedList<EducationMasterViewModel> GetEducations(PagingParams pagingParams);
        #endregion

        #region Key Skill Master
        ResultViewModel AddKeySkill(KeySkillMasterViewModel keySkillMaster);
        ResultViewModel EditKeySkill(int keySkillMasterId);
        ResultViewModel DeleteKeySkill(int keySkillMasterId);
        ResultViewModel ActivateDeactivateKeySkill(int KeySkillMasterId);
        PagedList<KeySkillMasterViewModel> GetKeySkills(PagingParams pagingParams);
        #endregion

        #region Company Master
        ResultViewModel AddCompany(CompanyMasterViewModel companyMaster);
        ResultViewModel EditCompany(int companyMasterId);
        ResultViewModel DeleteCompany(int companyMasterId);
        ResultViewModel ActivateDeactivateCompany(int companyMasterId);
        PagedList<CompanyMasterViewModel> GetCompanies(PagingParams pagingParams);
        #endregion

        #region Job Master
        ResultViewModel AddJob(JobMasterViewModel jobMaster);
        ResultViewModel EditJob(int jobMasterId);
        ResultViewModel DeleteJob(int jobMasterId);
        PagedList<JobMasterViewModel> GetJobs(PagingParams pagingParams);
        List<AutoComplete> GetEducations();
        List<AutoComplete> GetCities();
        List<AutoComplete> GetSkills();
        List<AutoComplete> GetCompanies();
        #endregion

        #region Happy Customers
        ResultViewModel AddHappyCustomer(HappyCustomersViewModel happyCustomer);
        ResultViewModel EditHappyCustomer(int happyCustomerId);
        ResultViewModel DeleteHappyCustomer(int happyCustomerId);
        PagedList<HappyCustomersViewModel> GetHappyCustomers(PagingParams pagingParams);
        ResultViewModel GetHappyCustomerList();
        #endregion

    }
}
