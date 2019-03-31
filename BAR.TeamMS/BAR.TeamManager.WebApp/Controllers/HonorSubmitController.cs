using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using Newtonsoft.Json;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class HonorSubmitController : Controller
    {
        IhonorService HonorService { get; set; }
        IhonorparticipantmemberService HonorparticipantmemberService { get; set; }
        IteamService TeamService { get; set; }
        IuserService UserService { get; set; }
        IpersonalinformationService PersonalinformationService { get; set; }
        // GET: HonorSubmit
        public ActionResult Index()
        {
            return View();
        }
        string HonorId = "";//添加成功后返回的荣耀ID
        string result = "";//返回结果
        string status = "";//返回状态
       /// <summary>
      /// 往荣耀表和荣耀参与成员表中添加信息  
      /// </summary>
      /// <returns></returns>
        [HttpPost]
        public ActionResult AddHonor()
        {
            string jsonTxt = "";
            string dataSize = Request["honorCommitJson"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HonorInfo perInfoSize = jss.Deserialize<HonorInfo>(dataSize);
            try
            {
                if (perInfoSize != null)
                {
                    //HonorInfo honorInfo = new HonorInfo();
                    //List<HonorPerson> honorInfoList = new List<HonorPerson>();
                    honor honorinfo = new honor();
                    honorinfo.vcHonorName = perInfoSize.HonorName;
                    honorinfo.vcGuideTeacher = perInfoSize.GuidTeacher;
                    honorinfo.vcNetConnectAddress = perInfoSize.NetConnect;
                    honorinfo.vcHonorIntroduce = perInfoSize.HonorIntroduce;
                    honorinfo.vcTechnicalType = perInfoSize.TechnicalType;
                    
                    honorinfo.dSubmitTime = DateTime.Now;
                    honorinfo.IsDel = false;
                    honorinfo.bReviewOfWorks = true;//作品是否审核   不能这样做

                    var teaminfo = TeamService.LoadEntities(t => t.vcTeamName == perInfoSize.HonorTeam).FirstOrDefault();
                    honorinfo.iTeamID = teaminfo.ID;
                    var addHonor = HonorService.AddEntity(honorinfo);
                    if (addHonor != null)
                    {
                        string personNumList = perInfoSize.userAccount;
                        string[] personNumAll = personNumList.Split(',');
                        //添加成员  往荣耀参与成员表中添加数据
                        foreach (string Num in personNumAll)
                        {
                            honorparticipantmember honorper = new honorparticipantmember();
                            honorper.iHonorID = addHonor.ID;
                            honorper.iUserID = Convert.ToInt32(Num);
                            honorper.vcNonTeamMember = perInfoSize.NonMember;
                            honorper.IsDel = false;
                            if (HonorparticipantmemberService.AddEntity(honorper) != null)
                            {
                                continue;
                            }
                        }
                        status = "ok";
                        result = "添加成功";
                        jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"," + "\"" + "honorId" + "\"" + ":" + "\"" + addHonor.ID.ToString() + "\"" +  "}";
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
            catch(Exception ex)
            {
                status = "no";
                result = "添加失败";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            }
            return Content(jsonTxt);
        }

       
        /// <summary>
        /// 搜索学号获取个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchAccount()
        {
            string jsonTxt = "";
            string account = Request["userAccount"];
            //string account = "123456789";
            try
            {
                if (account != null)
                {
                    SearchInfo searchInfo = new SearchInfo();
                    var userInfo = UserService.LoadEntities(u => u.vcUserAccount == account && u.IsDel == false).FirstOrDefault();
                    if (userInfo != null && userInfo.vcUserAccount != null)
                    {
                        try
                        {
                            var perInfo = PersonalinformationService.LoadEntities(p => p.iUserID == userInfo.ID && p.IsDel == false).FirstOrDefault();
                            searchInfo.UserId = userInfo.ID;
                            searchInfo.Account = userInfo.vcUserAccount;
                            if (perInfo.vcName != null && perInfo.cPhone != null)
                            {
                                searchInfo.Name = perInfo.vcName;
                                searchInfo.Phone = perInfo.cPhone;
                                jsonTxt = JsonConvert.SerializeObject(searchInfo, Newtonsoft.Json.Formatting.Indented);
                            }
                            else
                            {
                                status = "no";
                                result = "此账号信息未完善";
                                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                            }
                        }
                        catch(Exception ex)
                        {
                            status = "no";
                            result = "此账号信息未完善";
                            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                        }
                    }
                    else
                    {
                        status = "no";
                        result = "此账号没有注册";
                        jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                    }
                }
                else  
                {
                    status = "no";
                    result = "获取数据失败";
                    jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
                }
            }
            catch(Exception ex)
            {
                status = "no";
                result = "网络异常";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            }
            return Content(jsonTxt);
        }

        class SearchInfo
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Account { get; set; }
            public string Phone { get; set; }
        }


        class HonorInfo
        {
            public string HonorName { get; set; }
            public string GuidTeacher { get; set; }
            public string NetConnect { get; set; }
            public string HonorTeam { get; set; }
            public string HonorIntroduce { get; set; }
            public string TechnicalType { get; set; }
            public string NonMember { get; set; }
            public string userAccount { get; set; }
        }
    }
}