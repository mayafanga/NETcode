using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.BLL;
using BAR.TeamManager.Common;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static BAR.TeamManager.WebApp.Controllers.PersonShowController;

namespace BAR.TeamManager.WebApp.Controllers
{
    /// <summary>
    /// 武利敏
    /// </summary>
    public class PersonShowController : Controller
    {
        IhonorparticipantmemberService HonorparticipantmemberService { get; set; }
        IteamService TeamService { get; set; }
        IhonorService HonorService { get; set; }
        IpersonalinformationService PersonalinformationService { get; set; }
        IuserService UserService { get; set; }
        IteamapplicantService TeamapplicantService { get; set; }

        // GET: PersonShow
        public ActionResult Index()
        {
            return View();
        }
        string status = "";
        /// <summary>
        /// 个人信息展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowPerson()
        {
            string jsonTxt = "";
            /// <summary>
            /// 根据Cookie拿到UserID
            /// </summary>
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            string strId = Request["userId"];
            if (loginUserCookie != null && strId != null)
            {
                Model.ViewModel.PerStripModel.PerInfo perInfo = new Model.ViewModel.PerStripModel.PerInfo();
                int userId = Convert.ToInt32(strId);
                /// <summary>
                /// 根据UserId拿到所有与UserId对应的HonorId
                /// </summary>
                try
                {
                    var per = PersonalinformationService.LoadEntities(p => p.iUserID == userId).FirstOrDefault();
                    var user = UserService.LoadEntities(u => u.ID == userId).FirstOrDefault();
                    if (per != null && user != null)
                    {
                        perInfo.perEmail = per.vcEmail;
                        perInfo.perGrade = per.vcGrade;
                        perInfo.perMajor = per.vcMajor;
                        perInfo.perName = per.vcName;
                        perInfo.perNickName = user.vcNickName;
                        perInfo.perPhone = per.cPhone;
                        perInfo.perPhotoAddress = user.vcProfilePhotoAddress;
                        perInfo.perQQ = per.vcQQ;
                        perInfo.perWechat = per.vcWeChat;
                        perInfo.perIntroduce = per.vcPersonalIntroduce;
                        perInfo.perHobby = per.vcHobby;
                        Model.ViewModel.PerStripModel.PerHonorModel model = new Model.ViewModel.PerStripModel.PerHonorModel();
                        model.perInfo = perInfo;
                        jsonTxt = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
                    }
                    var teamapplicant = TeamapplicantService.LoadEntities(ta => ta.iUserID == userId).FirstOrDefault();
                    var team = TeamService.LoadEntities(t => t.ID == teamapplicant.iTeamID).FirstOrDefault();
                    perInfo.perTeamLogo = team.vcTeamLogoAddress;
                    List<Model.ViewModel.PerStripModel.PerHonor> perHonorList = new List<Model.ViewModel.PerStripModel.PerHonor>();
                    var honorPer = HonorparticipantmemberService.LoadEntities(hp => hp.iUserID == userId).ToList();
                    if (honorPer != null)
                    {
                        foreach (var honorper in honorPer)
                        {
                            Model.ViewModel.PerStripModel.PerHonor perHonor = new Model.ViewModel.PerStripModel.PerHonor();
                            var honorInfo = HonorService.LoadEntities(h => h.ID == honorper.iHonorID).FirstOrDefault();
                            perHonor.honorFell = honorper.vcHonorFeel;
                            perHonor.honorAdvice = honorper.vcHonorAdvice;
                            if (honorInfo != null)
                            {
                                perHonor.honorId = honorInfo.ID;
                                perHonor.honorLogo = honorInfo.vcPreviewAddress;
                                perHonor.honorName = honorInfo.vcHonorName;
                                perHonor.honorSubmit = (DateTime)honorInfo.dSubmitTime;
                                perHonor.honorTeacher = honorInfo.vcGuideTeacher;
                                var teamInfo = TeamService.LoadEntities(t => t.ID == honorInfo.iTeamID).FirstOrDefault();
                                if (teamInfo != null)
                                {
                                    perHonor.teamName = teamInfo.vcTeamName;
                                }
                                else
                                {
                                    perHonor.teamName = "数据库无此荣耀团队数据";
                                }
                            }
                            else
                            {
                                perHonor.honorId = 0;
                                perHonor.honorLogo = "../img/aboutUs/large/cover.jpg";
                                perHonor.honorName = "数据库无此荣耀名称数据";
                                perHonor.honorSubmit = DateTime.Now.Date;
                            }
                            perHonorList.Add(perHonor);
                        }
                        Model.ViewModel.PerStripModel.PerHonorModel model = new Model.ViewModel.PerStripModel.PerHonorModel();
                        model.perInfo = perInfo;
                        model.perHonorList = perHonorList;
                        var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
                        jsonTxt = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
                    }
                    else  //查询数据失败
                    {
                        status = "no";
                        jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\""  + "}";
                    }
                }
                catch (Exception ex)  //网络连接故障
                {
                    if (jsonTxt != null)
                    {
                        return Content(jsonTxt);
                    }
                    else
                    {
                        status = "no";
                        jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
                    }
                }
            }
            else //未登录
            {
                status = "no";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
            }
            return Content(jsonTxt);
        }

        /// <summary>
        /// 往荣耀成员表中添加荣耀建议和荣耀感悟
        /// </summary>
        /// <returns></returns>
        public ActionResult AddHonorInfo()
        {
            string jsonTxt = "";
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            if (loginUserCookie != null)
            {
                string userAccount = loginUserCookie.Values["loginUserAccount"];
                int honorId = Convert.ToInt32(Request["honorId"]);
                var userInfo = UserService.LoadEntities(u => u.vcUserAccount == userAccount).FirstOrDefault();
                string perHonorFeel = Request["HonorFell"];
                string perHonorAdvice = Request["HonorAdvice"];
                try
                {
                   var honorPerInfo = HonorparticipantmemberService.LoadEntities(hp => hp.iHonorID == honorId && hp.iUserID == userInfo.ID).FirstOrDefault();
                   honorPerInfo.vcHonorFeel = perHonorFeel;
                   honorPerInfo.vcHonorAdvice = perHonorAdvice;
                   if (HonorparticipantmemberService.EditEntity(honorPerInfo))
                   {
                      status = "ok";
                      jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
                   }
                   else
                   {
                       status = "no";
                       jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
                    }
                 }
                 catch(Exception ex)
                 {
                      status = "no";
                      jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
                 }
            }
            else  //未登录
            {
                status = "no";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
            }
            return Content(jsonTxt);
        }
        
        /// <summary>
        /// 得到荣耀感悟和荣耀建议
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHonorInfo()
        {
            string jsonTxt = "";
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            if (loginUserCookie != null)
            {
                string userAccount = loginUserCookie.Values["loginUserAccount"];
                int honorId = Convert.ToInt32(Request["HonorId"]);
                string useraccount = Request["userAccount"];
                string userPwd = Request["userPwd"];
                string userName = Request["userName"];
                if (honorId != null && userAccount==useraccount)//honorID有值并且账号一致
                {
                    try
                    {
                        var userInfo = UserService.LoadEntities(u => u.vcUserAccount == useraccount).FirstOrDefault();
                        string userpwd = userInfo.vcPassWord;
                        if (userPwd == userpwd)
                        {
                            var honor = HonorparticipantmemberService.LoadEntities(hp => hp.iHonorID == honorId && hp.iUserID == userInfo.ID).FirstOrDefault();
                            if (honor != null)
                            {
                                status = "ok";
                                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
                            }
                            else
                            {
                                status = "no";
                                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
                            }
                        }
                        else  //密码不一致
                        {
                            status = "no";
                            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
                        }
                    }
                    catch(Exception ex)
                    {
                        status = "no";
                        jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
                    }
                    
                }
                else
                {
                    status = "no";
                    jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
                }
            }
            else  //未登录
            {
                status = "no";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "}";
            }
            return Content(jsonTxt);
        }

        class HonorInfo
        {
            public string HonorFell { get; set; }
            public string HonorAdvice { get; set; }
        }
    }
}