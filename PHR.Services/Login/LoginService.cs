using AutoMapper;
using PHR.Data.Models;
using PHR.Services.Logger;
using PHR.ViewModels.Common;
using PHR.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PHR.Services.Login
{
    public class LoginService : ILoginService
    {
        #region Fields
        readonly phr_dbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILoggerService logger;
        private const int SaltKey = 12;
        #endregion

        #region Constructor
        public LoginService(phr_dbContext _dbContext, IMapper _mapper, ILoggerService _logger)
        {
            dbContext = _dbContext;
            mapper = _mapper;
            logger = _logger;
        }
        #endregion

        #region Methods
        public ResultViewModel ValidateCredentials(LoginCredentials credentials)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                //string passwordHash = BCrypt.Net.BCrypt.HashPassword(credentials.UserPassword, SaltKey);
                //LoginDetail loginDetail = dbContext.LoginDetails.FirstOrDefault(l => l.UserEmail.Equals(credentials.UserEmail) && l.UserPassword.Equals(passwordHash));
                LoginDetail loginDetail = dbContext.LoginDetails.FirstOrDefault(l => l.UserEmail.Equals(credentials.UserEmail));
                //if (loginDetail != null)
                if (loginDetail != null && BCrypt.Net.BCrypt.Verify(credentials.UserPassword, loginDetail.UserPassword))
                //if (loginDetail != null && BCrypt.Net.BCrypt.Verify(credentials.UserPassword, passwordHash)) 
                {
                    if (loginDetail.IsUserActive!=true)
                    {
                        loginDetail.IsUserActive = true;
                        dbContext.LoginDetails.Update(loginDetail);
                        dbContext.SaveChanges();
                    }

                    LoginDetailsViewModel Data = mapper.Map<LoginDetailsViewModel>(loginDetail);
                    result.Data = Data;
                    result.IsSuccessful = true;
                    result.Message = "User authenticated successfully";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "Email or Password is incorrect";
                }
            }
            catch (Exception ex)
            {
                Guid dd = new Guid();
                var datt = dd.ToString();
                logger.Logger(ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex.StackTrace);
                result.IsSuccessful = false;
                result.Message = "System error occured, please try later or contact Administrator";
            }
            
            return result;
        }

        public ResultViewModel RegisterUser(RegisterUserViewModel userDetails)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                if (dbContext.LoginDetails.FirstOrDefault(x => x.UserEmail == userDetails.UserEmail) != null)
                {
                    result.IsSuccessful = false;
                    result.Message = "User with this Email already exist, please try different one";
                }
                else if (dbContext.LoginDetails.FirstOrDefault(x => x.MobileNumber == userDetails.MobileNumber) != null)
                {
                    result.IsSuccessful = false;
                    result.Message = "User with this Mobile Number already exist, please try different one";
                }
                else if (dbContext.LoginDetails.FirstOrDefault(x => x.MobileNumber == userDetails.MobileNumber) != null)
                {
                    result.IsSuccessful = false;
                    result.Message = "User with this User Name already exist, please try different one";
                }
                else
                {
                    LoginDetail loginDetail = mapper.Map<LoginDetail>(userDetails);
                    loginDetail.AddedDate = DateTime.Now;
                    loginDetail.IsUserActive = false;
                    loginDetail.UserPassword = BCrypt.Net.BCrypt.HashPassword(loginDetail.UserPassword, SaltKey);
                    dbContext.LoginDetails.Add(loginDetail);
                    dbContext.SaveChanges();

                    result.IsSuccessful = true;
                    result.Message = "User registered successfully";
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

        #endregion
    }
}
