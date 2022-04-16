using System;

namespace PHR.ViewModels.Dashboard
{
    public class KeySkillMasterViewModel
    {
        public int KeySkillId { get; set; }
        public string KeySkillName { get; set; }
        public bool IsKeySkillActive { get; set; }
        public DateTime KeySkillAddedDate { get; set; }
    }
}
