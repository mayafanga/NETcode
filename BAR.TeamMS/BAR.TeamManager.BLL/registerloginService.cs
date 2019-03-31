using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.Model;
using BAR.TeamManager.IBLL;

namespace BAR.TeamManager.BLL
{
    public partial class registerloginService : BaseService<registerlogin>, IregisterloginService
    {
        /// <summary>
        /// 获取含有此用户ID的登录注册信息
        /// </summary>
        /// <param name="userId">用户ID列表</param>
        /// <returns></returns>
        public IQueryable<registerlogin> LoadReggisterLoginList(List<int> userId)
        {

            var ReggisterLoginList = this.CurrentDBSession.registerloginDal.LoadEntities(rl => userId.Contains((int)rl.iUserID));
            return ReggisterLoginList;
        }
        /// <summary>
        /// 得到用户的身份标识
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<registerlogin> LoginUserIdentity(int id)
        {
            var loginUserList = this.CurrentDBSession.registerloginDal.LoginUserIdentity(id);
            return loginUserList;
        }

    }
}
