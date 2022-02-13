using PHR.ViewModels.Common;
using PHR.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace PHR.Services.Login
{
    public interface ILoginService
    {
        ResultViewModel ValidateCredentials(LoginCredentials credentials);
        ResultViewModel RegisterUser(RegisterUserViewModel userDetails);
    }
}
