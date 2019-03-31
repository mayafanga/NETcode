using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.Model;
using BAR.TeamManager.IBLL;

namespace BAR.TeamManager.BLL
{
    public partial class personalinformationService:BaseService<personalinformation>,IpersonalinformationService
    {
        /// <summary>
        /// 获取用户ID列表中所有的个人信息
        /// </summary>
        /// <param name="userId">用户ID列表</param>
        /// <returns></returns>
        public IQueryable<personalinformation> LoadPersonalInformationList(List<int> userId)
        {
            var PersonalInformationList = this.CurrentDBSession.personalinformationDal.LoadEntities(p => userId.Contains((int)p.iUserID) && p.IsDel==false);
            return PersonalInformationList;
        }

    }
}
