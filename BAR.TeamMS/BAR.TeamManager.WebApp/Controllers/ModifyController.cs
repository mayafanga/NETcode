using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
namespace BAR.TeamManager.WebApp.Controllers
{
    public class ModifyController : Controller
    {
        IuserService userService = null;//用户
        IpersonalinformationService personalinformationService = null;//个人信息
        string userNickName = "null";//用户昵称
        string userName = "null";
        string mess = "null";
        string result = "null";
        string status = "fail";//状态默认为fail
        int usersId = 0;//用户ID
        string account = null;
        string pwd = null;
        string nickName = null;
        string ProfilePhotoAddress = null;
        int weChatId = 0;
        bool isDel = false;
        user usersInfo { get; set; }
        // GET: Modify
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询用户的昵称
        /// </summary>
        /// <returns></returns>
        [HttpGet]//建议把查询按钮去掉用GET请求
        public ActionResult FindUserNickNamePage()
        {
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];//登录用户的Cookie
            if (loginUserCookie != null)
            {
                string userAccount = Request.QueryString["userAccount"];//账号
                //string userAccount = "164804073";
                if (userAccount != null)
                {
                    try
                    {
                        var userInfo = userService.FindNickName(userAccount).FirstOrDefault();
                        if (userInfo != null)
                        {
                            userNickName = userInfo.vcNickName;//昵称
                            try
                            {
                                var personInfo = personalinformationService.LoadEntities(personalinformation => true).ToList();
                                foreach (var item in personInfo)
                                {
                                    if (userInfo.ID == item.iUserID)
                                    {
                                        userName = item.vcName;
                                        status = "ok";
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                status = "fail";
                                mess = "网络连接不稳定，请稍后再试";
                            }
                            
                        }
                        else
                        {
                            mess = "查询不到该用户";
                            status = "fail";
                        }
                    }
                    catch (Exception)
                    {

                        mess = "网络连接不稳定，请稍后再试";
                        status = "fail";
                    }
                }
                else
                {
                    mess = "请输入账号";
                    status = "fail";
                }

            }
            else
            {
                mess = "请先登录";
                status = "fail";
            }
            if (status=="fail")
            {
                result = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}"; 
            }
            if (status == "ok")
            {
                result = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "nickName" + "\"" + ":" + "\"" + userNickName + "\""+","+"\""+"userName"+"\""+":"+"\""+userName+"\""+"}"; 
            }
            return Content(result);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult ModifyPasswordPage()
        {
            string account = Request["account"];
            string pwd = Request["pwd"];
            //string account = "164804073";
            //string pwd = "01234567890";
            //userNickName = "小灰灰";
            if (account != null && pwd != null)
            {
                try
                {
                    var userInfo = userService.FindNickName(account).FirstOrDefault();
                    user users = new user();
                    users.ID = userInfo.ID;
                    users.vcUserAccount = userInfo.vcUserAccount;
                    users.vcNickName = userInfo.vcNickName;
                    users.vcProfilePhotoAddress = userInfo.vcProfilePhotoAddress;
                    users.iWeChatID = userInfo.iWeChatID;
                    users.IsDel = userInfo.IsDel;
                    users.vcPassWord = pwd;
                    try
                    {
                        if (userService.EditEntity(users))
                        {
                            mess = "修改成功";
                            status = "ok";
                        }
                        else
                        {
                            mess = "修改失败";
                            status = "fail";
                        }
                    }
                    catch (Exception ex)
                    {

                        mess = "网络连接不稳定，请稍后再试" + ex.ToString();
                        status = "fail";
                    }
                }
                catch (Exception)
                {
                    mess = "网络连接不稳定，请稍后再试";
                    status = "fail";
                }
            }
            else
            {
                mess = "账号不存在";
                status = "fail";
            }
            result = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}";
            return Content(result);
        }
    }
}