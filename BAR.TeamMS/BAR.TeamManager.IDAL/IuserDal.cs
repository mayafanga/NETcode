using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BAR.TeamManager.IDAL
{
    public partial interface IuserDal : IBaseDal<user>
    {
        //IQueryable<user> UserLogin(user userInfo);//用户登陆
        //检查用户是否注册成功
        IQueryable<user> CheckUserRegisterStatus(string account, string pwd);
        //检查用户是否注册
        IQueryable<user> CheckAccount(string account);
        //通过账号查询用户的姓名和昵称
        IQueryable<user> FindNickName(string account);
    }

}
