using System;
using System.Collections.Generic;
using System.Text;

namespace PHR.ViewModels.Common
{
    public class ResultViewModel
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
