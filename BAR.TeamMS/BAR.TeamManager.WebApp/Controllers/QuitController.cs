using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class QuitController : Controller
    {
        string msg = null;
        string status = null;
        // GET: Quit退出当前用户
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QuitLoginUser()
        {
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            List<string> quitCookieList = LoginController.cookieList;//得到集合里的登录数据
            if (loginUserCookie != null)
            {
                Response.Cookies["loginUserInfo"].Expires = DateTime.Now.AddDays(-1);//让Cookie过期
                //Response.Cookies["loginUserInfo"].Expires里的key要与Request.Cookies["loginUserInfo"]保持一致
                string quitAccount = loginUserCookie["loginUserAccount"];
                quitCookieList.Remove(quitAccount);//清除集合里的数据
                msg = "用户已退出";
                status = "ok";
            }
            else
            {
                msg = "您还没登录";
                status = "fail";
            }
            string jsonData = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + msg + "\"" + "}";
            return Content(jsonData);
        }
    }
}