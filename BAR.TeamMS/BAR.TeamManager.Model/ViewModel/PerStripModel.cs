using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.Model.ViewModel
{
    public class PerStripModel
    {
        public class PerInfo
        {
            public string perName { get; set; }
            public string perPhone { get; set; }
            public string perWechat { get; set; }
            public string perEmail { get; set; }
            public string perQQ { get; set; }
            public string perGrade { get; set; }
            public string perNickName { get; set; }
            public string perPhotoAddress { get; set; }
            public string perMajor { get; set; }
            public string perIntroduce { get; set; }
            public string perHobby { get; set; }
            public string perTeamLogo { get; set; }
        }
        public class PerHonor
        {
            public int honorId { get; set; }
            public string honorLogo { get; set; }
            public string honorName { get; set; }
            public string teamName { get; set; }
            public DateTime honorSubmit { get; set; }
            public string honorFell { get; set; }
            public string honorAdvice { get; set; }
            public string honorTeacher { get; set; }
        }
        public class PerHonorModel
        {
            public PerInfo perInfo { get; set; }
            public List<PerHonor> perHonorList { get; set; } 
        }
    }
}
