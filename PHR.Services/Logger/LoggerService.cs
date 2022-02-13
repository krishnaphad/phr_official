using PHR.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PHR.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        #region Fields
        readonly phr_dbContext dbContext;
        #endregion

        #region Constructor
        public LoggerService(phr_dbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        #endregion

        #region Methods
        public void Logger(string message, string stackTrace)
        {
            Log logger = new Log();
            logger.LogMessage = message;
            logger.LogStack = stackTrace;

            dbContext.Logs.Add(logger);
            dbContext.SaveChanges();
        }
        #endregion

    }
}
