using System;
using System.Collections.Generic;

#nullable disable

namespace PHR.Data.Models
{
    public partial class KeySkillMaster
    {
        public int KeySkillId { get; set; }
        public string KeySkillName { get; set; }
        public bool IsKeySkillActive { get; set; }
        public DateTime KeySkillAddedDate { get; set; }
    }
}
