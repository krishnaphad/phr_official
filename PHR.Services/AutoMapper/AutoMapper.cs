using AutoMapper;
using PHR.Data.Models;
using PHR.ViewModels.Dashboard;
using PHR.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace PHR.Services.AutoMapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<LoginDetail, LoginDetailsViewModel>().ReverseMap();
            CreateMap<LoginDetail, RegisterUserViewModel>().ReverseMap();
            CreateMap<CityMaster, CityMasterViewModel>().ReverseMap();
            CreateMap<EducationMaster, EducationMasterViewModel>().ReverseMap();
            CreateMap<KeySkillMaster, KeySkillMasterViewModel>().ReverseMap();
            CreateMap<CompanyMaster, CompanyMasterViewModel>().ReverseMap();
            CreateMap<JobsCollection, JobMasterViewModel>().ReverseMap();
            CreateMap<HappyCustomer, HappyCustomersViewModel>().ReverseMap();
        }
    }
}
