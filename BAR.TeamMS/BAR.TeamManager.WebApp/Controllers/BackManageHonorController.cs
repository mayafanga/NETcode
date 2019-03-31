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
    public class BackManageHonorController : Controller
    {
        IhonorService HonorService { get; set; }
        IhonorparticipantmemberService HonorparticipantmemberService { get; set; }
        IpersonalinformationService PersonalinformationService { get; set; }
        IregisterloginService RegisterloginService { get; set; }
        IuserService UserService { get; set; }
        // GET: BackManageHonor
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获得荣耀页面的全部信息(get请求)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHonorInfoList()
        {
            int pageIndex = Request["currentPage"] != null ? int.Parse(Request["currentPage"]) : 1;  //当前页数
            int pageSize = Request["pageSize"] != null ? int.Parse(Request["pageSize"]) : 1;             //一页有几条数据
            int totalCount;    //总页数
            bool? Istrue = false;
            var HonorInfoList = HonorService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, h => h.IsDel == Istrue, h => h.ID, true);
            List<Model.ViewModel.HonorStripModel.BackHonor> honorInfoList = new List<Model.ViewModel.HonorStripModel.BackHonor>();
            foreach (var honors in HonorInfoList)
            {
                Model.ViewModel.HonorStripModel.BackHonor honorInfo = new Model.ViewModel.HonorStripModel.BackHonor();
                honorInfo.honorId = honors.ID;
                honorInfo.honorLogo = honors.vcPreviewAddress;
                honorInfo.honorName = honors.vcHonorName;
                honorInfo.guidTeacher = honors.vcGuideTeacher;
                honorInfo.technicalType = honors.vcTechnicalType;
                honorInfo.honorIntroduce = honors.vcHonorIntroduce;
                honorInfo.honorSubmit = (DateTime)honors.dSubmitTime;
                if ((bool)honors.bReviewOfWorks)
                {
                    honorInfo.isChecked = "已审核";
                }
                else
                {
                    honorInfo.isChecked = "未审核";
                }
                honorInfoList.Add(honorInfo);
            }
            BackHonorModel model = new BackHonorModel();
            model.honorInfo = honorInfoList;
            int page = 0;
            if (totalCount > 0 && totalCount <= 5)
            {
                page = 1;
            }
            else if (totalCount % 5 == 0) 
            {
                page = (int)(totalCount / pageSize);
            }
            else if (totalCount % 5 != 0)
            {
                page = (int)(totalCount / pageSize) + 1;
            }
            model.totalPage = page;
            model.PageNavigator = Common.PageBarHelper.CreatePageNavigator(pageSize, pageIndex, totalCount);
            var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
            string jsonTxt = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
            return Content(jsonTxt);
        }

        public ActionResult GetHonorInfo()
        {
            string jsonTxt = "";
           int HonorId = Request["HonorId"] != null ? int.Parse(Request["HonorId"]) : 1;
            try
            {
                var honorinfo = HonorService.LoadEntities(h => h.ID == HonorId && h.IsDel == false).FirstOrDefault();
                BackHonorInfo honorInfo = new BackHonorInfo();
                honorInfo.honorName = honorinfo.vcHonorName;
                honorInfo.guidTeacher = honorinfo.vcGuideTeacher;
                honorInfo.technicalType = honorinfo.vcTechnicalType;
                if ((bool)honorinfo.bReviewOfWorks)
                {
                    honorInfo.isChecked = "已审核";
                }
                else
                {
                    honorInfo.isChecked = "未审核";
                }
                honorInfo.honorSubmit = (DateTime)honorinfo.dSubmitTime;
                honorInfo.honorIntroduce = honorinfo.vcHonorIntroduce;
                List<int> UserId = new List<int>();
                List<BackHonorInfoPer> HonorInfoPer = new List<BackHonorInfoPer>();
                var honorPer = HonorparticipantmemberService.LoadEntities(hp => hp.iHonorID == honorinfo.ID && hp.IsDel == false).ToList();
                foreach (var honorper in honorPer)
                {
                    UserId.Add((int)honorper.iUserID);
                    honorInfo.NonTeamMember = honorper.vcNonTeamMember;
                }
                foreach (int userId in UserId)
                {
                    BackHonorInfoPer honorInfoPer = new BackHonorInfoPer();
                    var per = PersonalinformationService.LoadEntities(p => p.iUserID == userId && p.IsDel == false).FirstOrDefault();
                    honorInfoPer.Name = per.vcName;
                    honorInfoPer.Gender = per.vcGender;
                    honorInfoPer.Grade = per.vcGrade;
                    honorInfoPer.Major = per.vcMajor;
                    var reg = RegisterloginService.LoadEntities(r => r.iUserID == userId && r.IsDel == false).FirstOrDefault();
                    if (reg.iIdentity == (int)Model.EnumType.IdentityEnumType.Captain)
                    {
                        honorInfoPer.Identity = "队长";
                    }
                    else if (reg.iIdentity == (int)Model.EnumType.IdentityEnumType.Member)
                    {
                        honorInfoPer.Identity = "队员";
                    }
                    else
                    {
                        honorInfoPer.Identity = "身份出错";
                    }
                    var user = UserService.LoadEntities(u => u.ID == userId && u.IsDel == false).FirstOrDefault();
                    honorInfoPer.Account = user.vcUserAccount;
                    HonorInfoPer.Add(honorInfoPer);
                }
                BackHonorInfoModel HonorInfoModel = new BackHonorInfoModel();
                HonorInfoModel.HonorInfo = honorInfo;
                HonorInfoModel.HonorInfoPer = HonorInfoPer;
                var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
               jsonTxt = JsonConvert.SerializeObject(HonorInfoModel, Newtonsoft.Json.Formatting.Indented);
            }
           catch(Exception ex)
            {
                jsonTxt = "no" + ex.ToString();
            }
            return Content(jsonTxt);
        }
        class BackHonorModel
        {
            public List<Model.ViewModel.HonorStripModel.BackHonor> honorInfo { get; set; }
            public int totalPage { get; set; }
            public string PageNavigator { get; set; }
        }

        class BackHonorInfo
        {
            public string honorName { get; set; }
            public string guidTeacher { get; set; }
            public string technicalType { get; set; }
            public string isChecked { get; set; }
            public System.DateTime honorSubmit { get; set; }
            public string honorIntroduce { get; set; }
            public string honorLogo { get; set; }
            public string NonTeamMember { get; set; }
        }
        class BackHonorInfoPer
        {
            public string Name { get; set; }
            public string Account { get; set; }
            public string Gender { get; set; }
            public string Grade { get; set; }
            public string Major { get; set; }
            public string Identity { get; set; }
        }
        class BackHonorInfoModel
        {
            public BackHonorInfo HonorInfo { get; set; }
            public List<BackHonorInfoPer> HonorInfoPer { get; set; }
        }

    }
}