using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.Model;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.DAL;
namespace BAR.TeamManager.BLL
{
    public partial class userService : BaseService<user>, IuserService
    {
        /// <summary>
        /// 获取用户ID列表中用户的信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IQueryable<user> LoadUserList(List<int> userId)
        {
            var UserList = this.CurrentDBSession.userDal.LoadEntities(u => userId.Contains(u.ID));
            return UserList;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userInfo">user类型的对象</param>
        /// <returns></returns>
        public IQueryable<user> UserLogin(user userInfo)
        {
            var userLogin = this.CurrentDBSession.userDal.LoadEntities(u => userInfo.vcUserAccount == u.vcUserAccount && userInfo.vcPassWord == u.vcPassWord);
            return userLogin;
        }
        /// <summary>
        /// 检查用户注册的账号是否已经存在
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        public IQueryable<user> CheckAccount(string account)
        {
            DAL.userDal uAccount = new userDal();
            var userAccount = uAccount.CheckAccount(account);
            return userAccount;
        }
        /// <summary>
        /// 检查用户是否注册成功
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public IQueryable<user> CheckUserRegisterStatus(string account, string pwd)
        {
            userDal registerStatus = new userDal();
            var status = registerStatus.CheckUserRegisterStatus(account, pwd);
            return status;
        }
        /// <summary>
        /// 获取用户头像地址
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public string GetUserPhotoAdd(int id)
        {
            var userInfo = this.LoadEntities(u => u.ID == id).FirstOrDefault();
            string photoAdd = userInfo.vcProfilePhotoAddress;
            return photoAdd;
        }
        /// <summary>
        /// 通过账号得到用户的姓名和昵称
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        public IQueryable<user> FindNickName(string account)
        {
            userDal modifyDal = new userDal();
            var modifyInfo = modifyDal.FindNickName(account);
            return modifyInfo;
        }
    }
}
