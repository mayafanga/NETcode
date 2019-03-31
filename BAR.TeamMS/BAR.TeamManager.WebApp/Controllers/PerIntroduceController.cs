using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data.Entity.Validation;
using System.Configuration;
using System.IO;

namespace BAR.TeamManager.WebApp.Controllers
{
    /// <summary>
    /// 武利敏
    /// </summary>
    public class PerIntroduceController : Controller
    {
        IpersonalinformationService PersonalinformationService { get; set; }
        IuserService UserService { get; set; }

        string result = "";//返回结果
        string status = "";//返回状态

        // GET: PerIntroduce
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPerIntroduce()
        {
            string jsonTxt = "";
            try
            {
                string datasize = Request["perInfoJson"];
                JavaScriptSerializer jss = new JavaScriptSerializer();
                PerInfo perInfoSize = jss.Deserialize<PerInfo>(datasize);
                HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
                int userId = Convert.ToInt32(loginUserCookie.Values["loginUserId"]);//用户的ID
                var userInfo = UserService.LoadEntities(u => u.ID == userId).FirstOrDefault();
                userInfo.vcNickName = perInfoSize.nickName;
                if (UserService.EditEntity(userInfo))
                {
                    var per = PersonalinformationService.LoadEntities(p => p.iUserID == userId).FirstOrDefault();
                    //把个人信息添加到数据库中
                    per.vcName = perInfoSize.name;
                    per.cPhone = perInfoSize.phone;
                    per.vcGender = perInfoSize.gender;
                    per.dBirthday = perInfoSize.birthday;
                    per.vcWeChat = perInfoSize.wechat;
                    per.vcEmail = perInfoSize.Email;
                    per.vcQQ = perInfoSize.QQ;
                    per.vcHobby = perInfoSize.hobby;
                    per.vcPersonalIntroduce = perInfoSize.introduce;
                    per.vcAddress = perInfoSize.address;
                    per.vcMajor = perInfoSize.major;
                    per.vcGrade = perInfoSize.grade;
                    per.IsDel = false;
                    if (PersonalinformationService.EditEntity(per))
                    {
                        status = "ok";
                        result = "添加成功";
                        jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                    }
                    else
                    {
                        status = "no";
                        result = "添加失败";
                        jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                    }
                }
                else
                {
                    status = "no";
                    result = "请先登录";
                    jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                }
            }
           catch(Exception ex)
            {
                status = "no";
                result = "添加失败";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            }
            return Content(jsonTxt);
        }
        /// <summary>
        /// 添加用户头像 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUserLogo()
        {
            string jsonTxt = "";
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            int userId = Convert.ToInt32(loginUserCookie.Values["loginUserId"]);//用户的ID
            string userLogo = Request["userLogo"];
            string getPath = ConfigurationManager.AppSettings["perLogo"];
            string perLogo = Common.EditPhotos.Base64StringToImage(userLogo, getPath);
            string savePath = ".." + getPath + "/" + Path.GetFileName(perLogo);
            try
            {
                var userInfo = UserService.LoadEntities(u => u.ID == userId).FirstOrDefault();
                userInfo.vcProfilePhotoAddress = savePath;
                if (UserService.EditEntity(userInfo))
                {
                    status = "ok";
                    result = "添加成功";
                    jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                }
                else
                {
                    status = "no";
                    result = "添加失败";
                    jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                }
            }
            //catch (DbEntityValidationException dbEx)
            //{
            //    status = "no";
            //    result = dbEx.EntityValidationErrors.SelectMany(item => item.ValidationErrors).Aggregate(result, (current, item2) => current + string.Format("{0}:{1}\r\n", item2.PropertyName, item2.ErrorMessage));
            //    jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            //}
            catch(Exception ex)
            {
                status = "no";
                result = ex.ToString();
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            }
            return Content(jsonTxt);
        }
        /// <summary>
        /// 获取个人信息  get请求
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPerInfo()
        {
            string jsonTxt = "";
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            if (loginUserCookie != null)
            {
                 int userId = Convert.ToInt32(loginUserCookie.Values["loginUserId"]);
                 var per = PersonalinformationService.LoadEntities(p => p.iUserID == userId).FirstOrDefault();
                 var user = UserService.LoadEntities(u => u.ID == userId).FirstOrDefault();
                 if (per != null && user != null)
                 {
                     Per perInfo = new Per();
                    if (!String.IsNullOrEmpty(user.vcNickName))
                    {
                      perInfo.nickName = user.vcNickName;
                    }
                    if (!String.IsNullOrEmpty(per.vcName))
                    {
                        perInfo.name = per.vcName;
                    }
                    if (!String.IsNullOrEmpty(user.vcProfilePhotoAddress))
                    {
                        perInfo.perLogo = user.vcProfilePhotoAddress;
                    }
                    if (!String.IsNullOrEmpty(per.vcGender))
                    {
                        perInfo.gender = per.vcGender;
                    }
                    if (!String.IsNullOrEmpty(per.vcGrade))
                    {
                        perInfo.grade = per.vcGrade;
                    }
                    if (!String.IsNullOrEmpty(per.vcMajor))
                    {
                        perInfo.major = per.vcMajor;
                    }
                    if (per.dBirthday != null)
                    {
                        perInfo.birthday = (DateTime)per.dBirthday;
                    }
                    if (!String.IsNullOrEmpty(per.cPhone))
                    {
                        perInfo.phone = per.cPhone;
                    }
                    if (!String.IsNullOrEmpty(per.vcWeChat))
                    {
                        perInfo.wechat = per.vcWeChat;
                    }
                    if (!String.IsNullOrEmpty(per.vcQQ))
                    {
                        perInfo.QQ = per.vcQQ;
                    }
                    if (!String.IsNullOrEmpty(per.vcEmail))
                    {
                        perInfo.Email = per.vcEmail;
                    }
                    if (!String.IsNullOrEmpty(per.vcHobby))
                    {
                        perInfo.hobby = per.vcHobby;
                    }
                    if (!String.IsNullOrEmpty(per.vcAddress))
                    {
                        perInfo.address = per.vcAddress;
                    }
                    if (!String.IsNullOrEmpty(per.vcPersonalIntroduce))
                    {
                        perInfo.introduce = per.vcPersonalIntroduce;
                    }
                     var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
                     jsonTxt = JsonConvert.SerializeObject(perInfo, Newtonsoft.Json.Formatting.Indented);
                 }
                else  //未找到数据
                {
                    status = "no";
                    result = "未找到数据";
                    jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                }
            }
            else //未登录
            {
                status = "no";
                result = "未登录";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            }
            return Content(jsonTxt);
        }
        class PerInfo
        {
            public string nickName { get; set; }
            public string name { get; set; }
            public string gender { get; set; }
            public string grade { get; set; }
            public string major { get; set; }
            public DateTime birthday { get; set; }
            public string phone { get; set; }
            public string wechat { get; set; }
            public string Email { get; set; }
            public string QQ { get; set; }
            public string hobby { get; set; }
            public string address { get; set; }
            public string introduce { get; set; }
        }
        class Per
        {
            public string nickName { get; set; }
            public string name { get; set; }
            public string gender { get; set; }
            public string grade { get; set; }
            public string major { get; set; }
            public DateTime birthday { get; set; }
            public string phone { get; set; }
            public string wechat { get; set; }
            public string Email { get; set; }
            public string QQ { get; set; }
            public string hobby { get; set; }
            public string address { get; set; }
            public string introduce { get; set; }
            public string perLogo { get; set; }
        }
    }
}