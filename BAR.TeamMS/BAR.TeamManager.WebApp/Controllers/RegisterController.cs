using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.Common;
namespace BAR.TeamManager.WebApp.Controllers
{
    public class RegisterController : Controller
    {
        IregisterloginService registerloginService { get; set; }
        IuserService userService { get; set; }
        IpersonalinformationService personalinformationService { get; set; }
        string result = null;
        //string registerLoginStatus = null;//声明注册登陆表字段
        string userInfoStatus = null;//声明用户表字段
        string status = null;
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterPage()
        {
            if (Request.HttpMethod.ToUpper() == "POST")
            {

                string userAccount = Request["account"];//学号注册
                string userPwd = Request["password"];//密码
                string reUserPwd = Request["rePassword"];
                char[] userAccounts = userAccount.ToCharArray();
                string userPhone = Request["phone"];
                //string userAccount = "185800000";
                //string userPwd = "0123456789";
                //string reUserPwd = "0123456789";
                //string userPhone = "18337281814";
                ////string userPhone = "010-88888888";//固定电话
                //char[] userAccounts = userAccount.ToCharArray();
                //int userWeChatId = Convert.ToInt32(Request["weChatId"]);//微信ID账号，目前不做
                try
                {
                    var uAccount = userService.CheckAccount(userAccount).FirstOrDefault();
                    if (uAccount == null)//账号未注册
                    {
                        if (userAccounts[1] == '6' || userAccounts[1] == '7' || userAccounts[1] == '8' || userAccounts[1] == '9')
                        {
                            if (userAccounts[0] == '1' && userAccounts.Length == 9 && CheckValidateCharacter(userAccount))
                            {
                                if (userPwd == reUserPwd)
                                {
                                    char[] userPwds = userPwd.ToCharArray();
                                    if (userPwds.Length >= 10 && userPwds.Length <= 16 && CheckValidateCharacter(userPwd))
                                    {
                                        if (CheckValidatePhone(userPhone))
                                        {
                                            if (userAccount != null && userPwd != null && userPhone != null)
                                            {

                                                user userRegister = new user();//用户注册和登陆表，存储用户注册时的基本信息
                                                registerlogin registerAndLogin = new registerlogin();//注册登录表，在后台自动填写用的其他信息
                                                personalinformation perlinformation = new personalinformation();//个人信息表,用户填充IUSERID
                                                userRegister.vcUserAccount = userAccount;
                                                userPwd = MD5Secret.GetMD5Str(userPwd);//给密码加密
                                                userRegister.vcPassWord = userPwd;
                                                //userRegister.iWeChatID= userWeChatId;暂时不用
                                                userRegister.IsDel = false;//数据库如果没有写false,添加
                                                try
                                                {
                                                    userService.AddEntity(userRegister);//将用户注册的基本信息添加到user表里
                                                    try
                                                    {
                                                        var userStatus = userService.CheckUserRegisterStatus(userAccount, userPwd);
                                                        foreach (var status in userStatus)
                                                        {
                                                            userInfoStatus = status.vcUserAccount;

                                                        }
                                                        //用户基本信息完毕
                                                        //用户注册时后台自动填充用户的其他信息的表
                                                        registerAndLogin.GUID = Guid.NewGuid();
                                                        registerAndLogin.iUserID = userRegister.ID;
                                                        registerAndLogin.dApplyTime = DateTime.Now.ToLocalTime();
                                                        registerAndLogin.vcApplyLocation = "text";//申请地点
                                                        registerAndLogin.iIdentity = 0;//身份标识
                                                        registerAndLogin.IsDel = false;
                                                        //填充注册登录表完毕
                                                        //填充个人信息表
                                                        perlinformation.iUserID = userRegister.ID;
                                                        try
                                                        {
                                                            personalinformationService.AddEntity(perlinformation);
                                                        }
                                                        catch (Exception)
                                                        {
                                                            status = "fail";
                                                            result = "网络异常,注册失败";
                                                        }
                                                        //个人信息表完毕
                                                        //后台自动填充用户其他的信息完毕
                                                        try
                                                        {
                                                            if (userInfoStatus == userAccount && registerloginService.AddEntity(registerAndLogin) != null)
                                                            {
                                                                status = "ok";
                                                                result = "注册成功";
                                                            }
                                                            else
                                                            {
                                                                status = "fail";
                                                                result = "注册失败";
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            status = "fail";
                                                            result = "网络异常,注册失败";
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                        status = "fail";
                                                        result = "网络异常,注册失败";
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    status = "fail";
                                                    result = "网络异常,注册失败"; ;
                                                }
                                            }
                                            else
                                            {
                                                status = "fail";
                                                result = "注册失败";
                                            }
                                        }
                                        else
                                        {
                                            status = "fail";
                                            result = "请输入正确的电话号码";
                                        }
                                    }
                                    else
                                    {
                                        status = "fail";
                                        result = "密码格式不正确，请检查密码长度和密码格式是否符合规范";
                                    }
                                }
                                else
                                {
                                    status = "fail";
                                    result = "两次输入的密码不一致";
                                }
                            }
                            else
                            {
                                status = "fail";
                                result = "账号格式输入不正确，请以学号注册";
                            }
                        }
                        else
                        {
                            status = "fail";
                            result = "账号格式输入不正确，请以学号注册(暂时支持16、17、18、19级学生)";
                        }
                    }
                    else
                    {
                        status = "fail";
                        result = "您的账号已注册，无法重复注册";
                    }
                }
                catch (Exception)
                {
                    status = "fail";
                    result = "网络连接不稳定";
                }
            }
            string jsonData = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + result + "\"" + "}";
            return Content(jsonData);
        }
        /// <summary>
        /// 判断用户注册的密码是不是具有汉字
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool CheckValidateCharacter(string pwd)
        {
            bool result = false;
            for (int i = 0; i < pwd.Length; i++)
            {
                //输入的密码里具有汉字返回false
                if (Regex.IsMatch(pwd.ToString(), @"[\u4E00-\u9FA5]+$"))
                {
                    result = false;
                }
                //不具有汉字返回true
                else
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 判断用户输入的手机号是否合法
        /// </summary>
        /// <param name="phone">电话号码</param>
        /// <returns></returns>
        public bool CheckValidatePhone(string phone)
        {
            bool result = false;
            if (Regex.IsMatch(phone, "^(0\\d{2,3}-?\\d{7,8}(-\\d{3,5}){0,1})|(((13[0-9])|(15([0-3]|[5-9]))|(18[0-9])|(17[0-9])|(14[0-9]))\\d{8})$"))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}