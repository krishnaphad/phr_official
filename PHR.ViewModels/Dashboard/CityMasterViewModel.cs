using System;

namespace PHR.ViewModels.Dashboard
{
    public class CityMasterViewModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public bool CityIsActive { get; set; }
        public DateTime CityAddedDate { get; set; }        
    }
}
