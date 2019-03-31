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

namespace BAR.TeamManager.WebApp.Controllers
{
    public class BackManagePersonController : Controller
    {
        IpersonalinformationService PersonalinformationService { get; set; }
        IuserService UserService { get; set; }
        IteamService TeamService { get; set; }

        private List<BackPersonInfo> perInfoList { get; set; }

        // GET: BackManagePerson
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取全部个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPersonList()
        {
            string jsonTxt = "";
            string id = Request["personId"];
            int pageIndex = Request["currentPage"] != null ? int.Parse(Request["currentPage"]) : 1;//当前页数
            int pageSize = Request["PageSize"] != null ? int.Parse(Request["PageSize"]) : 1;//一页有几条数据
            int totalCount;//总页数
            bool? Istrue = false;
            if (id != null)
            {
                try
                {
                    var PersonInfoList = PersonalinformationService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, p => p.IsDel == Istrue, p => p.ID, true).ToList();
                    List<BackPersonInfo> perInfoList = new List<BackPersonInfo>();
                    foreach (var persons in PersonInfoList)
                    {
                        BackPersonInfo perInfo = new BackPersonInfo();
                        perInfo.UserId = persons.ID;
                        perInfo.Name = persons.vcName;
                        var user = UserService.LoadEntities(u => u.ID == persons.iUserID).FirstOrDefault();
                        perInfo.UserName = user.vcNickName;
                        perInfo.ProfilePhotoAddress = user.vcProfilePhotoAddress;
                        perInfo.Gender = persons.vcGender;
                        perInfo.Birthday = (DateTime)persons.dBirthday;
                        perInfo.Major = persons.vcMajor;
                        perInfo.PersonalIntroduce = persons.vcPersonalIntroduce;
                        perInfoList.Add(perInfo);
                    }
                    BackPersonModel model = new BackPersonModel();
                    model.PersonInfoList = perInfoList;
                    model.totalPage = pageSize;
                    model.PageNavigate = Common.PageBarHelper.CreatePageNavigator(pageSize, pageIndex, totalCount);//调用分页的方法
                    var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
                    jsonTxt = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);

                }
                catch (Exception ex)
                {
                    throw;
                    jsonTxt = "no";
                }
            }
            else
            {
                jsonTxt = "no";
            }
            return Content(jsonTxt);
        }

        /// <summary>
        /// 获取一个人的全部个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagePersonPageHead()
        {
            string jsonTxt = "";
            string id = Request["personId"];
            if (id != null)
            {
                try
                {
                    int perId = Convert.ToInt32(id);
                    var persons = PersonalinformationService.LoadEntities(p => p.ID == perId).FirstOrDefault();
                    BackPersonInfo perInfo = new BackPersonInfo();
                    //perInfo.TeamName = "BAR团队";
                    var team = TeamService.LoadEntities(t => t.ID == persons.iUserID).FirstOrDefault();
                    perInfo.TeamName = team.vcTeamName;
                    perInfo.Name = persons.vcName;
                    var user1 = UserService.LoadEntities(u => u.ID == persons.iUserID).FirstOrDefault();
                    perInfo.ProfilePhotoAddress = user1.vcProfilePhotoAddress;
                    perInfo.UserAccount = user1.vcUserAccount;
                    perInfo.UserName = user1.vcNickName;
                    perInfo.Gender = persons.vcGender;
                    perInfo.Grade = persons.vcGrade;
                    perInfo.Birthday = (DateTime)persons.dBirthday;
                    perInfo.Major = persons.vcMajor;
                    perInfo.WeChat = persons.vcWeChat;
                    perInfo.QQ = persons.vcQQ;
                    perInfo.Email = persons.vcEmail;
                    perInfo.Phone = persons.cPhone;
                    perInfo.PersonalIntroduce = persons.vcPersonalIntroduce;
                    perInfo.Hobby = persons.vcHobby;
                    perInfo.Address = persons.vcAddress;
                    BackPersonModel model = new BackPersonModel();
                    model.PersonInfoList = perInfoList;
                    var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
                    jsonTxt = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
                }
                catch (Exception ex)
                {
                    jsonTxt = "no";
                }

            }
            else
            {
                jsonTxt = "no";
            }
            return Content(jsonTxt);
        }

        class BackPersonModel
        {
            public List<BackPersonInfo> PersonInfoList { get; set; }
            public int totalPage { get; set; }
            public string PageNavigate { get; set; }

        }

        class BackPersonInfo
        {
            public int? UserId { get; set; }
            public string TeamName { get; set; }
            public string Name { get; set; }
            public string UserAccount { get; set; }
            public string UserName { get; set; }
            public string Gender { get; set; }
            public string Grade { get; set; }
            public DateTime Birthday { get; set; }
            public string Major { get; set; }
            public string WeChat { get; set; }
            public string QQ { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string PersonalIntroduce { get; set; }
            public string ProfilePhotoAddress { get; set; }
            public string Hobby { get; set; }
            public string Address { get; set; }
        }
    }
}