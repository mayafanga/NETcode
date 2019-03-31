using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.Model;
using BAR.TeamManager.IDAL;

namespace BAR.TeamManager.IBLL
{
    public partial interface IuserService : IBaseService<user>
    {
        //获取用户ID列表中用户的信息
        IQueryable<user> LoadUserList(List<int> userId);
        //用户登陆
        IQueryable<user> UserLogin(user userInfo);
        //检查用户是否注册成功
        IQueryable<user> CheckUserRegisterStatus(string account, string pwd);
        //检查账号是不是已经注册
        IQueryable<user> CheckAccount(string account);
        /// <summary>
        /// 获取用户头像地址
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        string GetUserPhotoAdd(int id);
        //通过账号查询用户的姓名和昵称
        IQueryable<user> FindNickName(string account);
    }
}
