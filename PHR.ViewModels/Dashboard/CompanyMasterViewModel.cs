using System;
using System.Collections.Generic;
using System.Text;

namespace PHR.ViewModels.Dashboard
{
    public class CompanyMasterViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsCompanyActive { get; set; }
        public DateTime CompanyAddedDate { get; set; }
    }
}
