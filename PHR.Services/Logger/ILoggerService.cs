using System;
using System.Collections.Generic;
using System.Text;

namespace PHR.Services.Logger
{
    public interface ILoggerService
    {
        void Logger(string message, string stackTrace);
    }
}
