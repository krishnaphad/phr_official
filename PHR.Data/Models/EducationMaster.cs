using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class EducationMaster
    {
        public int EducationId { get; set; }
        public string EducationName { get; set; }
        public bool IsEducationActive { get; set; }
        public DateTime EducationAddedDate { get; set; }
    }
}
