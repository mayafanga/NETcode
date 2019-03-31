using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.Model.ViewModel
{
    /// <summary>
    /// 马亚芳
    /// </summary>
    public class BackDataModel
    {
        /// <summary>
        /// 后台团队所有信息界面
        /// </summary>
        public class TeamAll
        {
            public int teamId { get; set; }//团队ID
            public string Logo { get; set; }//团队LOGO
            public string teamName { get; set; }//团队名
            public string guidTeacher { get; set; }//团队指导老师
            public DateTime SetupTime { get; set; }//团队成立时间
            public string teamMain { get; set; }//团队主打
            public string check { get; set; }//团队审核状况
            public string teamIntroduce { get; set; }//团队简介
        }
        public class TeamSloe
        {
            public string Logo { get; set; }//团队LOGO
            public string teamName { get; set; }//团队名
            public string guidTeacher { get; set; }//团队指导老师
            public DateTime SetupTime { get; set; }//团队成立时间
            public string teamMain { get; set; }//团队主打
            public string check { get; set; }//团队审核状况
            public string teamIntroduce { get; set; }//团队简介
            public string teamSlider { get; set; }//团队轮播图
        }
        public class Person
        {
            public string headerLogo { get; set; }
            public string perName { get; set; }
        }
        public class TeamModel
        {
            public TeamSloe team { get; set; }
            public List<Person> person { get; set; }
        }
    }
}
