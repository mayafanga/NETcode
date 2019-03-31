using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.Model.ViewModel
{
    /// <summary>
    /// 马亚芳
    /// 用在查找荣耀所有内容   返回json数据用
    /// </summary>
    public class HonorStripModel
    {
        /// <summary>
        /// 有关荣耀表所需要的内容
        /// </summary>
        public class Honor
        {
            public int honorId { get; set; }
            public string honorLogo { get; set; }
            public string honorName { get; set; }
            public System.DateTime honorSubmit { get; set; }
            public string honorIntroduce { get; set; }
            public string guidTeacher { get; set; }
            public string honorTeam { get; set; }
            public string unperHonorName { get; set; }
            public List<Person> personList { get; set; }
        }
        
        /// <summary>
        /// 有关个人姓名所需要的内容
        /// </summary>
        public class Person
        {
            public string Name { get; set; }
        }
        public class SingleHonor
        {
            public string teamName { get; set; }
            public string honorName { get; set; }
            public string honorSlider { get; set; }
            public string guidTeacher { get; set; }
            public string honorIntroduce { get; set; }
            public string unperHonorName { get; set; }
            public System.DateTime honorSubmit { get; set; }
            public string check { get; set; }
            public string netLocation { get; set; }
            public string downLoadLoaction { get; set; }
            public string fileName { get; set; }
        }
        public class SInglePerson
        {
            public int singlePersonId { get; set; }
            public string perName { get; set; }
            public string gender { get; set; }
            public string position { get; set; }
            public string phone { get; set; }
            public string perIntroduce { get; set; }
            public string perHobby { get; set; }
            public string perLogo { get; set; }
        }
       public class SingleModel
        {
            public SingleHonor singleHonor { get; set; }
            public List<SInglePerson> singlePerson { get; set; }
        }
        public class BackHonor
        {
            public int honorId { get; set; }
            public string honorLogo { get; set; }
            public string honorName { get; set; }
            public string guidTeacher { get; set; }
            public System.DateTime honorSubmit { get; set; }
            public string honorIntroduce { get; set; }
            public string isChecked { get; set; }
            public string technicalType { get; set; }
        }
    }
}
