using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.Model;
using BAR.TeamManager.IDAL;

namespace BAR.TeamManager.IBLL
{
    /// <summary>
    /// 获取用户ID列表中所有的个人信息
    /// </summary>
    public partial interface IpersonalinformationService:IBaseService<personalinformation>
    {
        IQueryable<personalinformation> LoadPersonalInformationList(List<int> userId);
    }
}
