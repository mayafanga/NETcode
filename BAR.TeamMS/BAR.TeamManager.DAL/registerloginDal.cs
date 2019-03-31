using BAR.TeamManager.IDAL;
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.DAL
{
    public partial class registerloginDal : BaseDal<registerlogin>, IregisterloginDal
    {
        /// <summary>
        /// 得到用户的身份标识
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public IQueryable<registerlogin> LoginUserIdentity(int id)
        {
            barteammanagedbEntities1 db = new barteammanagedbEntities1();
            var reslut = from a in db.registerlogin
                         where a.iUserID == id
                         select a;
            return reslut;
        }
    }
}
