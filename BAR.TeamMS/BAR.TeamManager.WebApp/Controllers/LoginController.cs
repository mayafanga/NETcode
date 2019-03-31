using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading;
using System.Timers;
using BAR.TeamManager.Common;
namespace BAR.TeamManager.WebApp.Controllers
{
    public class LoginController : Controller
    {
        IuserService userService { get; set; }
        IregisterloginService registerloginService { get; set; }
        public static List<string> cookieList = new List<string>();//存储客户端的cookie集合
        string loginReslut = null;//登录结果
        string mess = null;//登录后的信息
        HttpCookie loginUserCookiesInfo = null;//用户登录Cookie
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginPage()
        {
            StringBuilder sbMess = new StringBuilder();

            if (Request.HttpMethod.ToUpper() == "POST")
            {
                //HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
                //string CookieAccount = loginUserCookie["loginUserAccount"];//cookie里无数据不能用
                string usersAccount = Request["account"];//获取用户账号
                string userPwd = Request["pwd"];
                ThreadUserCookie();//执行定时器
                loginReslut = LoginUser(usersAccount, userPwd, out string loginMess);
                mess = loginMess;
            }
            sbMess.Append("{" + "\"" + "status" + "\"" + ":" + "\"" + loginReslut + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}");
            return Content(sbMess.ToString());
        }
        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="sbMess"></param>
        /// <returns></returns>
        public string LoginUser(string userAccount, string userPwd, out string loginMess)
        {
            loginMess = null;
            string loginStatus = null;
            user userInfo = new user();
            userInfo.vcUserAccount = userAccount;
            userPwd = MD5Secret.GetMD5Str(userPwd);//输入的密码加密，与数据库里对比
            userInfo.vcPassWord = userPwd;
            try
            {
                var loginUsers = userService.UserLogin(userInfo).FirstOrDefault();
                if (loginUsers != null)
                {
                    var user = userService.UserLogin(userInfo);
                    if (user != null)
                    {
                        foreach (var item in user)
                        {
                            if (item != null)
                            {
                                try
                                {
                                    var register = registerloginService.LoginUserIdentity(item.ID);//通过用户ID拿到用户的身份标识
                                    if (register != null)
                                    {
                                        try
                                        {
                                            foreach (var registerInfo in register)
                                            {
                                                if (registerInfo != null)
                                                {
                                                    //HttpCookie loginUserCookiesInfo = new HttpCookie("loginUserInfo");//创建cookie对象
                                                    loginUserCookiesInfo = new HttpCookie("loginUserInfo");//创建cookie对象
                                                    loginUserCookiesInfo.Values.Add("loginUserAccount", item.vcUserAccount);
                                                    loginUserCookiesInfo.Values.Add("loginUserId", item.ID.ToString());
                                                    loginUserCookiesInfo.Values.Add("loginUserIdentity", registerInfo.iIdentity.ToString());//将身份标识写入Cookie
                                                    //loginUserCookiesInfo.Expires = DateTime.Now.AddDays(1);
                                                    Response.AppendCookie(loginUserCookiesInfo);//写入到客户端
                                                    cookieList.Add(item.vcUserAccount);//将登录上的用户账号存入集合
                                                    loginStatus = "ok";//用户登录成功，
                                                    loginMess = "用户登录成功";
                                                }
                                                else
                                                {
                                                    loginStatus = "fail";
                                                    loginMess = "登录失败,查询不到用户（可能未注册)";
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            loginStatus = "fail";
                                            loginMess = "登录失败，查询不到登录信息（可能未注册)";
                                        }

                                    }
                                    else
                                    {
                                        loginStatus = "fail";
                                        loginMess = "登录失败,查询不到用户（可能未注册)";
                                    }
                                }
                                catch (Exception)
                                {

                                    loginStatus = "fail";
                                    loginMess = "登录失败，网络连接不稳定";
                                }
                            }
                        }
                    }
                    else
                    {
                        loginStatus = "fail";
                        loginMess = "登录失败,查询不到用户（可能未注册)";
                    }
                }
                else
                {
                    loginStatus = "fail";
                    loginMess = "登录失败,查询不到用户（可能未注册或账号密码不正确)";
                }
            }
            catch (Exception)
            {

                loginStatus = "fail";//网络连接不稳定
                loginMess = "登录失败，网络连接不稳定";
            }
            return loginStatus;
        }
        public void ThreadUserCookie()
        {
            Thread thUserCookie = new Thread(CookieTime);
            thUserCookie.IsBackground = true;
            thUserCookie.Start();

        }
        public void CookieTime()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);//设定计时器，1秒进行一次循环
            timer.Elapsed += new System.Timers.ElapsedEventHandler(ThCookie);
            timer.AutoReset = true;//执行多次
            timer.Enabled = true;//执行事件为true
        }
        public void ThCookie(object sender, ElapsedEventArgs e)
        {
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            if (loginUserCookie == null)
            {
                if (cookieList.Count > 0 && cookieList.Contains(Request["account"]))
                {
                    cookieList.Remove(Request["account"]);
                }
            }
        }
    }
}