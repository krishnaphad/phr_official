using AutoMapper;
using PHR.Data.Models;
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
        }
    }
}
