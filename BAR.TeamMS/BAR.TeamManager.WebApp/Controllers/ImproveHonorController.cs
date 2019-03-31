using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Common;
using BAR.TeamManager.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text;
using System.IO;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Configuration;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class ImproveHonorController : Controller
    {
        IhonorService HonorService { get; set; }
        IhonorparticipantmemberService HonorparticipantmemberService { get; set; }
        IuserService UserService { get; set; }
        IteamService TeamService { get; set; }
        IteamapplicantService TeamapplicantService { get; set; }
        // GET: ImproveHonor
        public ActionResult Index()
        {
            return View();
        }
        string result = "";//返回结果
        string status = "";//返回状态
        #region   get请求
        /// <summary>
        /// 打开此页面发送get请求，如果里面有内容就返回内容，如果没有就只返回身份标识号其他内容赋值为空，
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTeamInfo()
        {
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            if (loginUserCookie != null)
            {
                Info Info = new Info();
                int teamId = Common.GetID.GetTeamID(loginUserCookie, TeamapplicantService);
                try
                {
                    var teamIn = TeamService.LoadEntities(t => t.ID == teamId && t.IsDel == false).FirstOrDefault();
                    int identityId = Convert.ToInt32(loginUserCookie.Values["loginUserIdentity"]);
                        Info.identityId = identityId;
                        if (!String.IsNullOrEmpty(teamIn.vcTeamIntroduce))
                        {
                            Info.teamIntroduce = teamIn.vcTeamIntroduce;
                        }
                        if (!String.IsNullOrEmpty(teamIn.vcTeamMain))
                        {
                            Info.teamMain = teamIn.vcTeamMain;
                        }
                        if (!String.IsNullOrEmpty(teamIn.vcTeamLogoAddress))
                        {
                            Info.teamLogo = teamIn.vcTeamLogoAddress;
                        }
                        if (!String.IsNullOrEmpty(teamIn.vcTeamSliderAddress))
                        {
                            Info.teamSlider = teamIn.vcTeamSliderAddress;
                        }
                    return Content(JsonConvert.SerializeObject(Info, Newtonsoft.Json.Formatting.Indented));

                }
                catch (Exception ex)
                {
                    return Content("Network instability");
                }

            }
            else
            {
                return Content("Not logged in");
            }
        }
        /// <summary>
        /// 获取此团队的所有荣耀 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHonorInfo()
        {
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            if (loginUserCookie != null)
            {
                try
                {
                    int teamId = Common.GetID.GetTeamID(loginUserCookie, TeamapplicantService);
                    var honorInfos = HonorService.LoadEntities(h => h.iTeamID == teamId && h.IsDel == false && h.bReviewOfWorks == true).ToList();
                    List<HonorInfo> honorInfoList = new List<HonorInfo>();
                    foreach (var honor in honorInfos)
                    {
                        HonorInfo honorInfo = new HonorInfo();
                        honorInfo.honorId = honor.ID;
                        honorInfo.honorName = honor.vcHonorName;
                        honorInfo.guidTeacher = honor.vcGuideTeacher;
                        honorInfo.submitTime = (DateTime)honor.dSubmitTime;
                        honorInfoList.Add(honorInfo);
                    }
                    MaskLayer maskLayer = new MaskLayer();
                    maskLayer.HonorList = honorInfoList;
                    var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
                    string jsonTxt = JsonConvert.SerializeObject(maskLayer, Newtonsoft.Json.Formatting.Indented);
                    return Content(jsonTxt);
                }
                catch(Exception ex)
                {
                    return Content("Not logged in");
                }
            }
            else
            {
                return Content("Not logged in");
            }
        }
        /// <summary>
        /// 展示荣耀的LOGO 和 轮播图
        /// </summary>
        /// <returns></returns>
        public ActionResult GetThisHonor()
        {
            int honorId = Convert.ToInt32(Request["honorId"]);
            try
            {
                var honorInfo = HonorService.LoadEntities(h => true && h.IsDel == false && h.bReviewOfWorks == true).FirstOrDefault();
                HonorImg honorImg = new HonorImg();
                if (!String.IsNullOrEmpty(honorInfo.vcPreviewAddress))
                {
                    honorImg.Logo = honorInfo.vcPreviewAddress;
                }
                if (!String.IsNullOrEmpty(honorInfo.vcHonorSliderAddress))
                {
                    honorImg.Slider = honorInfo.vcHonorSliderAddress;
                }

                string jsonTxt = JsonConvert.SerializeObject(honorImg, Newtonsoft.Json.Formatting.Indented);
                return Content(jsonTxt);
            }
            catch(Exception ex)
            {
                return Content("no");
            }
        }

        #endregion

        #region  post请求
        /// <summary>
        /// 添加荣耀表的轮播地址
        /// </summary>
        /// <returns></returns>
        public ActionResult AddHonorSliderAddress()
        {
            //获取前台发送的Base64编码，然后分割转换成图片，拼成Json格式保存到数据库
            string jsonTxt = "";
            int honorId = Convert.ToInt32(Request["honorId"]);
            if (honorId == 0)
            {
                status = "no";
                result = "添加失败";
            }
            else if (honorId == -1)
            {
                status = "no";
                result = "添加失败";
            }
            else
            {
                string honorSliderAddress = Request["honorSliderAddress"];
                if (honorSliderAddress != null)
                {
                    string getPath = ConfigurationManager.AppSettings["honorSlider"];
                    string sliderTxt = Common.EditSlider.EncapCode(honorSliderAddress, getPath);
                    try
                    {
                        var honor = HonorService.LoadEntities(h => h.ID == honorId && h.IsDel == false).FirstOrDefault();
                        honor.vcHonorSliderAddress = sliderTxt;
                        if (HonorService.EditEntity(honor))
                        {
                            status = "ok";
                            result = "添加成功";
                        }
                        else
                        {
                            status = "no";
                            result = "添加失败";
                        }
                    }
                    catch (Exception)
                    {
                        status = "no";
                        result = "网络异常";
                    }
                }
                else
                {
                    try
                    {
                        var honor = HonorService.LoadEntities(h => h.ID == honorId && h.IsDel == false).FirstOrDefault();
                        honor.vcHonorSliderAddress = "null";
                        if (HonorService.EditEntity(honor))
                        {
                            status = "ok";
                            result = "添加成功";
                        }
                        else
                        {
                            status = "no";
                            result = "添加失败";
                        }
                    }
                    catch (Exception)
                    {
                        status = "no";
                        result = "网络异常";
                    }
                }

            }
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            return Content(jsonTxt);
        }




        /// <summary>
        /// 添加荣耀表的Logo地址
        /// </summary>
        /// <returns></returns>
        public ActionResult AddHonorLogoAddress()
        {
            string jsonTxt = "";
            int honorId = Convert.ToInt32(Request["honorId"]);
            if (honorId == 0)
            {
                status = "no";
                result = "添加失败";
            }
            else if (honorId == -1)
            {
                status = "no";
                result = "添加失败";
            }
            else
            {
                string honorLogoAddress = Request["honorLogoAddress"] != null ? Request["honorLogoAddress"] : "Image/SliderPhoto/slider.jpg";//获取团队头像的base64编码
                string getPath = ConfigurationManager.AppSettings["honorLogo"];
                string savePath = EditPhotos.Base64StringToImage(honorLogoAddress, getPath);//Base64转图片
                string sliderLogo = ".." + getPath + "/" + Path.GetFileName(savePath);
                try
                {
                    var honor = HonorService.LoadEntities(h => h.ID == honorId && h.IsDel == false).FirstOrDefault();
                    honor.vcPreviewAddress = sliderLogo;
                    if (HonorService.EditEntity(honor))
                    {
                        status = "ok";
                        result = "添加成功";
                    }
                    else
                    {
                        status = "no";
                        result = "添加失败";
                    }
                }
                catch (Exception)
                {
                    status = "no";
                    result = "网络异常";
                }
            }
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            return Content(jsonTxt);
        }

        /// <summary>
        /// 上传荣耀文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadHonor()
        {
            int HonorId = Convert.ToInt32(Request["honorId"]);
            string downLoad = Request["DownLoad"];
            string jsonTxt = "";
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            string getPath = ConfigurationManager.AppSettings["honorFile"];
            string fileName = Common.UpLoadFile.UpFile(files, getPath);
            string saveName = ".." + getPath + "/" + Path.GetFileName(fileName);
            try
            {
                if (fileName != null)
                {
                    var honorInfo = HonorService.LoadEntities(h => h.ID == HonorId && h.IsDel == false).FirstOrDefault();
                    honorInfo.vcHonorSubmitAddress = saveName;
                    if (downLoad == "是")
                    {
                        honorInfo.bDownLoadUnable = true;
                    }
                    else
                    {
                        honorInfo.bDownLoadUnable = false;
                    }
                    if (HonorService.EditEntity(honorInfo))
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
                    result = "添加失败";
                    jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                }
            }
            catch (Exception ex)
            {
                status = "no";
                result = "添加失败";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            }

            return Content(jsonTxt);
        }



        /// <summary>
        /// 添加团队简介
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTeamIntroduce()
        {
            string jsonTxt = null;
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            int teamId = Common.GetID.GetTeamID(loginUserCookie, TeamapplicantService);
            if (teamId == 0)
            {
                status = "no";
                result = "添加失败";
            }
            else if (teamId == -1)
            {
                status = "no";
                result = "添加失败";
            }
            else
            {
                string teamIntroduce = Request["teamIntroduce"] != null ? Request["teamIntroduce"] : "text";
                try
                {
                    var team = TeamService.LoadEntities(t => t.ID == teamId && t.IsDel == false).FirstOrDefault();
                    team.vcTeamIntroduce = teamIntroduce;
                    if (TeamService.EditEntity(team))
                    {
                        status = "ok";
                        result = "添加成功";
                    }
                    else
                    {
                        status = "no";
                        result = "添加失败";
                    }
                }
                catch (Exception)
                {
                    status = "no";
                    result = "网络异常";
                }
            }
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            return Content(jsonTxt);
        }
        /// <summary>
        /// 添加团队主打
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTeamMain()
        {
            string jsonTxt = null;
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            int teamId = Common.GetID.GetTeamID(loginUserCookie, TeamapplicantService);
            if (teamId == 0)
            {
                status = "no";
                result = "添加失败";
            }
            else if (teamId == -1)
            {
                status = "no";
                result = "添加失败";
            }
            else
            {
                string teamMain = Request["teamMain"] != null ? Request["teamMain"] : "text";
                try
                {
                    var team = TeamService.LoadEntities(t => t.ID == teamId && t.IsDel == false).FirstOrDefault();
                    team.vcTeamMain = teamMain;
                    if (TeamService.EditEntity(team))
                    {
                        status = "ok";
                        result = "添加成功";
                    }
                    else
                    {
                        status = "no";
                        result = "添加失败";
                    }
                }
                catch (Exception)
                {
                    status = "no";
                    result = "网络异常";
                }
            }
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            return Content(jsonTxt);
        }
        /// <summary>
        /// 添加团队轮播地址   已改
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTeamSliderAddress()
        {
            string jsonTxt = null;
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            int teamId = Common.GetID.GetTeamID(loginUserCookie, TeamapplicantService);
            if (teamId == 0)
            {
                status = "no";
                result = "添加失败";
            }
            else if (teamId == -1)
            {
                status = "no";
                result = "添加失败";
            }
            else
            {
                string teamSliderAddress = Request["teamSliderAddress"];
                if (teamSliderAddress != null)
                {
                    string getPath = ConfigurationManager.AppSettings["teamSlider"];
                    string sliderTxt = Common.EditSlider.EncapCode(teamSliderAddress, getPath);
                    try
                    {
                        var team = TeamService.LoadEntities(t => t.ID == teamId && t.IsDel == false).FirstOrDefault();
                        team.vcTeamSliderAddress = sliderTxt;  //有错误，需要修改
                        if (TeamService.EditEntity(team))
                        {
                            status = "ok";
                            result = "添加成功";
                        }
                        else
                        {
                            status = "no";
                            result = "添加失败";
                        }

                    }
                    catch (Exception)
                    {
                        status = "no";
                        result = "网络异常";
                    }
                }
                else
                {
                    try
                    {
                        var team = TeamService.LoadEntities(t => t.ID == teamId && t.IsDel == false).FirstOrDefault();
                        team.vcTeamSliderAddress = "null";
                        if (TeamService.EditEntity(team))
                        {
                            status = "ok";
                            result = "添加成功";
                        }
                        else
                        {
                            status = "no";
                            result = "添加失败";
                        }
                    }
                    catch (Exception)
                    {
                        status = "no";
                        result = "网络异常";
                    }
                }

            }
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            return Content(jsonTxt);
        }
        /// <summary>
        /// 添加团队Logo地址   已改
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTeamLogoAddress()
        {
            string jsonTxt = null;
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];
            int teamId = Common.GetID.GetTeamID(loginUserCookie, TeamapplicantService);
            if (teamId == 0)
            {
                status = "no";
                result = "添加失败";
            }
            else if (teamId == -1)
            {
                status = "no";
                result = "添加失败";
            }
            else
            {
                string teamLogoAddress = Request["teamLogoAddress"] != null ? Request["teamLogoAddress"] : "Image/SliderPhoto/slider.jpg";//获取团队头像的base64编码
                string getPath = ConfigurationManager.AppSettings["teamLogo"];
                string savePath = EditPhotos.Base64StringToImage(teamLogoAddress, getPath);//Base64转图片
                string sliderLogo = ".." + getPath + "/" + Path.GetFileName(savePath);
                try
                {
                    var team = TeamService.LoadEntities(t => t.ID == teamId && t.IsDel == false).FirstOrDefault();
                    team.vcTeamLogoAddress = sliderLogo;
                    if (TeamService.EditEntity(team))   //保存的时候出现对一个或多个实体
                    {
                        status = "ok";
                        result = "添加成功";
                    }
                    else
                    {
                        status = "no";
                        result = "添加失败";
                    }
                }
                catch (Exception ex)
                {
                    status = "no";
                    result = "网络异常";
                }
            }
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            return Content(jsonTxt);
        }


        class Info
        {
            public int identityId { get; set; }
            public string honorLogo { get; set; }
            public string honorSlider { get; set; }
            public string teamLogo { get; set; }
            public string teamSlider { get; set; }
            public string teamIntroduce { get; set; }
            public string teamMain { get; set; }
        }
        class HonorInfo
        {
            public int honorId { get; set; }
            public string honorName { get; set; }
            public DateTime submitTime { get; set; }
            public string guidTeacher { get; set; }
        }
        class MaskLayer
        {
            public List<HonorInfo> HonorList { get; set; }
        }
        class HonorImg
        {
            public string Logo { get; set; }
            public string Slider { get; set; }
        }
        #endregion


    }
}