using System.Linq;
using BAR.TeamManager.IDAL;
using BAR.TeamManager.Model;
using System.Data.Entity;

namespace BAR.TeamManager.DAL
{
    public partial class userDal : BaseDal<user>, IuserDal
    {
        /// <summary>
        /// 检查用户的账号是否被注册
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        public IQueryable<user> CheckAccount(string account)
        {
            barteammanagedbEntities1 db = new barteammanagedbEntities1();
            var reslut = from a in db.user
                         where a.vcUserAccount == account
                         select a;
            return reslut;
        }
        /// <summary>
        /// 检查用户的注册状态
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public IQueryable<user> CheckUserRegisterStatus(string account, string pwd)
        {
            barteammanagedbEntities1 db = new barteammanagedbEntities1();
            var result = from a in db.user
                         where a.vcUserAccount == account && a.vcPassWord == pwd
                         select a;
            return result;
        }
        /// <summary>
        /// 通过账号得到用户的姓名和昵称
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        public IQueryable<user> FindNickName(string account)
        {
            barteammanagedbEntities1 db = new barteammanagedbEntities1();
            var result = from u in db.user
                         where u.vcUserAccount == account
                         select u;
            return result;
        }

        public IQueryable<user> Test()
        {
            barteammanagedbEntities1 db = new barteammanagedbEntities1();
            var result = from a in db.user
                         select a;
            return result;
        }
    }
}
