using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class HappyCustomer
    {
        public int HappyCustomerId { get; set; }
        public string HappyCustomerCompanyName { get; set; }
        public string HappyCustomerComment { get; set; }
        public string HappyCustomerCompanyLogoName { get; set; }
    }
}
