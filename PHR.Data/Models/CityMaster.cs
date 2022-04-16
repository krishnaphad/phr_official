using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class CityMaster
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public bool CityIsActive { get; set; }
        public DateTime CityAddedDate { get; set; }
    }
}
