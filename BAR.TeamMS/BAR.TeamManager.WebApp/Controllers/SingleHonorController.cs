using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Net;
using System.Configuration;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class SingleHonorController : Controller
    {
        IteamService TeamService { get; set; }
        IhonorService HonorService { get; set; }
        IhonorparticipantmemberService HonorparticipantmemberService { get; set; }
        IpersonalinformationService PersonalinformationService { get; set; }
        IuserService UserService { get; set; }
        IregisterloginService RegisterloginService { get; set; }
        // GET: SingleHonor
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 加载单个荣耀页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSingleHonor()
        {
            string jsonTxt = null;
            Model.ViewModel.HonorStripModel.SingleHonor singleHonor = new Model.ViewModel.HonorStripModel.SingleHonor();         
            List<int> userId = new List<int>();
            int HonorId= Request["HonorId"] != null ? int.Parse(Request["HonorId"]) : 1;
            try
            {
                var honor = HonorService.LoadEntities(h => h.ID == HonorId && h.bReviewOfWorks == true && h.IsDel==false).FirstOrDefault();//获取团队表中的信息
                if (honor != null)
                {
                    var team = TeamService.LoadEntities(t => t.ID == honor.iTeamID && t.bCheckedcounselor==true && t.bCheckedTeacher==true &&t.IsDel==false).FirstOrDefault();
                    singleHonor.teamName = team.vcTeamName;
                    singleHonor.honorName = honor.vcHonorName;
                    singleHonor.honorSlider = honor.vcHonorSliderAddress;
                    singleHonor.guidTeacher = honor.vcGuideTeacher;
                    singleHonor.honorIntroduce = honor.vcHonorIntroduce;
                    singleHonor.honorSubmit = (DateTime)honor.dSubmitTime;
                    if ((bool)honor.bReviewOfWorks)
                    {
                        singleHonor.check = "已审核";
                    }
                    else
                    {
                        singleHonor.check = "未审核";
                    }
                    singleHonor.netLocation = honor.vcNetConnectAddress;
                    singleHonor.downLoadLoaction = honor.vcDownLoadAddress;
                    var honorMember = HonorparticipantmemberService.LoadEntities(hm => hm.iHonorID == honor.ID && hm.IsDel==false).ToList();
                    foreach (var member in honorMember)
                    {
                        singleHonor.unperHonorName = member.vcNonTeamMember;
                        userId.Add((int)member.iUserID);
                    }
                    
                    List<Model.ViewModel.HonorStripModel.SInglePerson> perList = new List<Model.ViewModel.HonorStripModel.SInglePerson>();
                    var perInfoList = PersonalinformationService.LoadPersonalInformationList(userId).ToList();

                    foreach (var perInfo in perInfoList)
                    {
                        Model.ViewModel.HonorStripModel.SInglePerson singlePer = new Model.ViewModel.HonorStripModel.SInglePerson();
                        singlePer.singlePersonId = perInfo.ID;
                        singlePer.perName = perInfo.vcName;
                        singlePer.gender = perInfo.vcGender;
                        singlePer.phone = perInfo.cPhone;
                        singlePer.perIntroduce = perInfo.vcPersonalIntroduce;
                        singlePer.perHobby = perInfo.vcHobby;
                        var user = UserService.LoadEntities(u => u.ID == perInfo.iUserID && u.IsDel==false).FirstOrDefault();
                        singlePer.perLogo = user.vcProfilePhotoAddress;
                        var register = RegisterloginService.LoadEntities(r => r.iUserID == perInfo.iUserID && r.IsDel==false).FirstOrDefault();
                        if(register != null)
                        {
                            if((int)register.iIdentity == 0)
                            {
                                singlePer.position = "普通用户";
                            }
                            else if((int)register.iIdentity==(int)Model.EnumType.IdentityEnumType.Member)
                            {
                                singlePer.position = "队员";
                            }
                            else if((int)register.iIdentity == (int)Model.EnumType.IdentityEnumType.Captain)
                            {
                                singlePer.position = "队长";
                            }
                            else if ((int)register.iIdentity == (int)Model.EnumType.IdentityEnumType.Teacher)
                            {
                                singlePer.position = "老师";
                            }
                            else
                            {
                                singlePer.position = "管理员";
                            }
                        }
                        else
                        {
                            singlePer.position = "游客";
                        }
                        perList.Add(singlePer);
                    }
                    Model.ViewModel.HonorStripModel.SingleModel model = new Model.ViewModel.HonorStripModel.SingleModel();
                    model.singleHonor = singleHonor;
                    model.singlePerson = perList;
                    var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
                    jsonTxt = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
                }
                else
                {
                    jsonTxt = "空空如也";
                }
            }
            catch(Exception ex)
            {
                jsonTxt = "网络不稳定，请稍后重试"+ex.ToString();
            }
            return Content(jsonTxt);
        }

        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public ActionResult DownLoadFile()
        {
            string status = null;
            string result = null;
            string jsonTxt = null;
            //给我传过来荣耀ID 在数据库中找到荣耀文件地址   给uri
            //给我保存文件的地址把从服务器上找到的文件保存到此地址中  savePath
            try
            {
                //int HonorId = 31;
                int HonorId = Request["HonorId"] != null ? int.Parse(Request["HonorId"]) : 1;
                var honorInfo = HonorService.LoadEntities(h => h.ID == HonorId).FirstOrDefault();
                string uri = honorInfo.vcHonorSubmitAddress;  //要配置到web.config文件中
                string savePath = ConfigurationManager.AppSettings["saveHonor"];
                status = Common.DownLoadFile.DownLoadFiles(uri, savePath);
                if (status=="no")
                {
                    result = "下载失败";
                }
                else
                {
                    result = "下载成功";
                }
            }
            catch(Exception ex)
            {
                status = "no";
                result = "下载失败";
            }
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "result" + "\"" + ":" + "\"" + result + "\"" + "}";
            return Content(jsonTxt);
        }
    }
}