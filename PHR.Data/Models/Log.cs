using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string LogMessage { get; set; }
        public string LogStack { get; set; }
    }
}
