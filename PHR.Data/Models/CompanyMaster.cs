using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class CompanyMaster
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsCompanyActive { get; set; }
        public DateTime CompanyAddedDate { get; set; }
    }
}
