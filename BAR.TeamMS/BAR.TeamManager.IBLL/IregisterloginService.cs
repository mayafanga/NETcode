using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.Model;
using BAR.TeamManager.IDAL;

namespace BAR.TeamManager.IBLL
{
    public partial interface IregisterloginService:IBaseService<registerlogin>
    {
        /// <summary>
        /// 获取含有此用户ID的登录注册信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IQueryable<registerlogin> LoadReggisterLoginList(List<int> userId);
        /// <summary>
        /// 得到登录用户的身份标识
        /// </summary>
        /// <param name="id">登录用户的id</param>
        /// <returns></returns>
        IQueryable<registerlogin> LoginUserIdentity(int id);
    }
}
