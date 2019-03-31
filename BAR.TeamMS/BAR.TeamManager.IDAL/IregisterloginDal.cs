using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.IDAL
{
    public partial interface IregisterloginDal : IBaseDal<registerlogin>
    {
        /// <summary>
        /// 得到用户的身份标识
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<registerlogin> LoginUserIdentity(int id);
    }
}
