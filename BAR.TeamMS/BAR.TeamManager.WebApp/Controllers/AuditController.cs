using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.IBLL;
using System.Text;
using BAR.TeamManager.Model;
namespace BAR.TeamManager.WebApp.Controllers
{
    public class AuditController : Controller
    {
        IteamService teamService { get; set; }
        IhonorService honorService { get; set; }
        StringBuilder sbTeam = new StringBuilder();
        StringBuilder sbHonor = new StringBuilder();
        string teamAuditBackResult = null;
        string honorAuditBackResult = null;
        string status = null;
        string mess = null;
        string errorData = null;
        string result = null;
        // GET: Audit
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 团队审核
        /// </summary>
        /// <returns></returns>
        public ActionResult TeamAudit()
        {
            try
            {
                var teamList = teamService.LoadEntities(t => t.bCheckedTeacher == false).ToList();
                if (teamList != null)
                {

                    sbTeam.Append("{" + "\"" + "teamAudit" + "\"" + ":" + "[");
                    foreach (var teams in teamList)
                    {
                        sbTeam.Append("{" + "\"" + "id" + "\"" + ":" + "\"" + teams.ID + "\"" + "," + "\"" + "teamName" + "\"" + ":" + "\"" + teams.vcTeamName + "\"" + "," + "\"" + "guidTeacher" + "\"" + ":" + "\"" + teams.vcGuideTeacher + "\"" + "," + "\"" + "teamMain" + "\"" + ":" + "\"" + teams.vcTeamMain + "\"" + "," + "\"" + "teamSetupTime" + "\"" + ":" + "\"" + teams.dTeamSetupTime + "\"" + "," + "\"" + "teamIntroduce" + "\"" + ":" + "\"" + teams.vcTeamIntroduce + "\"" + "}" + ",");
                    }
                    sbTeam.Append("]}");
                    teamAuditBackResult = sbTeam.ToString().Remove(sbTeam.ToString().LastIndexOf(','), 1);//移除最后一个逗号
                }
                else
                {
                    status = "fail";
                    mess = "没有需要审核的内容";
                    errorData = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}";
                }
            }
            catch (Exception)
            {
                status = "fail";
                mess = "网络不稳定，请稍后再试";
                errorData = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}";
            }
            if (status == "fail")
            {
                return Content(errorData);
            }
            else
            {
                return Content(teamAuditBackResult);
            }
        }
        /// <summary>
        /// 荣耀审核
        /// </summary>
        /// <returns></returns>
        public ActionResult HonorAudit()
        {
            try
            {
                var honnrList = honorService.LoadEntities(h => h.bReviewOfWorks == false).ToList();
                if (honnrList != null)
                {

                    sbHonor.Append("{" + "\"" + "honorAudit" + "\"" + ":" + "[");
                    foreach (var honors in honnrList)
                    {
                        sbHonor.Append("{" + "\"" + "id" + "\"" + ":" + "\"" + honors.ID + "\"" + "," + "\"" + "honorName" + "\"" + ":" + "\"" + honors.vcHonorName + "\"" + "," + "\"" + "guidTeacher" + "\"" + ":" + "\"" + honors.vcGuideTeacher + "\"" + "," + "\"" + "TechnicalType" + "\"" + ":" + "\"" + honors.vcTechnicalType + "\"" + "," + "\"" + "honorSetupTime" + "\"" + ":" + "\"" + honors.dSubmitTime + "\"" + "," + "\"" + "honorIntroduce" + "\"" + ":" + "\"" + honors.vcHonorIntroduce + "\"" + "}" + ",");
                    }
                    sbHonor.Append("]}");
                    honorAuditBackResult = sbHonor.ToString().Remove(sbHonor.ToString().LastIndexOf(','), 1);//移除最后一个逗号
                }
                else
                {
                    status = "fail";
                    mess = "网络不稳定，请稍后再试";
                    errorData = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}";
                }
            }
            catch (Exception)
            {
                status = "fail";
                mess = "网络不稳定，请稍后再试";
                errorData = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}";
            }
            if (status == "fail")
            {
                return Content(errorData);
            }
            else
            {
                return Content(honorAuditBackResult);
            }
        }
        /// <summary>
        /// 团队名称
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTeamAuditName()
        {
            string teamName = null;
            int id = Convert.ToInt32(Request.QueryString["id"]);
            try
            {
                var teamInfo = teamService.LoadEntities(t => t.ID == id).FirstOrDefault();
                if (teamInfo != null)
                {
                    teamName = teamInfo.vcTeamName;
                    status = "ok";
                }
                else
                {
                    status = "fail";
                    mess = "查询不到该团队名称";
                }
            }
            catch (Exception)
            {
                status = "fail";
                mess = "网络连接不稳定，请稍后再试";
            }
            if (status == "ok")
            {
                result= "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "teamName" + "\"" + ":" + "\"" + teamName + "\"" + "}";
            }
            else
            {
                result= "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}";
            }
            return Content(result);
        }
        /// <summary>
        /// 荣耀名称
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHonorAuditName()
        {
            string honorName = null;
            int id = Convert.ToInt32(Request.QueryString["id"]);
            try
            {
                var honorInfo = honorService.LoadEntities(h => h.ID == id).FirstOrDefault();
                if (honorInfo != null)
                {
                    honorName = honorInfo.vcHonorName;
                    status = "ok";
                }
                else
                {
                    status = "fail";
                    mess = "查询不到该团队名称";
                }
            }
            catch (Exception)
            {
                status = "fail";
                mess = "网络连接不稳定，请稍后再试";
            }
            if (status == "ok")
            {
                result = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "honorName" + "\"" + ":" + "\"" + honorName + "\"" + "}";
            }
            else
            {
                result = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}"; ;
            }
            return Content(result);
        }
        /// <summary>
        /// 审核团队结果
        /// </summary>
        /// <returns></returns>
        public ActionResult TeamAuditByTeacher()
        {
            string suggestion = Request.QueryString["suggistion"];
            int teamId =Convert.ToInt32(Request.QueryString["id"]);
            if (suggestion == "agree")
            {
                try
                {
                    var teamList = teamService.LoadEntities(t => t.ID == teamId).FirstOrDefault();
                    teamList.bCheckedTeacher = true;
                    if (teamList != null)
                    {
                        team teamInfo = new team();
                        teamInfo.ID = teamList.ID;
                        teamInfo.vcTeamSliderAddress = teamList.vcTeamSliderAddress;
                        teamInfo.vcTeamName = teamList.vcTeamName;
                        teamInfo.vcGuideTeacher = teamList.vcGuideTeacher;
                        teamInfo.vcTeamLogoAddress = teamList.vcTeamLogoAddress;
                        teamInfo.vcTeamIntroduce = teamList.vcTeamIntroduce;
                        teamInfo.dTeamSetupTime = teamList.dTeamSetupTime;
                        teamInfo.vcTeamMain = teamList.vcTeamMain;
                        teamInfo.bCheckedTeacher = teamList.bCheckedTeacher;//老师审核
                        teamInfo.bCheckedcounselor = teamList.bCheckedcounselor;//辅导员审核
                        teamInfo.IsDel = teamList.IsDel;
                        try
                        {
                            if (teamService.EditEntity(teamInfo))
                            {
                                status = "ok";
                                mess = "审核通过";
                            }
                            else
                            {
                                status = "fail";
                                mess = "审核不通过";
                            }
                        }
                        catch (Exception)
                        {
                            status = "fail";
                            mess = "网络连接不稳定，请稍后再试";
                        }
                    }
                }
                catch (Exception)
                {
                    status = "fail";
                    mess = "网络连接不稳定，请稍后再试";
                }
            }
            if (suggestion == "disagress")
            {
                status = "fail";
                mess = "审核不通过";
            }
            result = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}";
            return Content(result);
        }
        /// <summary>
        /// 审核荣耀结果
        /// </summary>
        /// <returns></returns>
        public ActionResult HonorAuditByTeacher()
        {
            string suggestion = Request.QueryString["suggistion"];
            int honorId = Convert.ToInt32(Request.QueryString["id"]);
            if (suggestion == "agree")
            {
                try
                {
                    var honorList = honorService.LoadEntities(h => h.ID == honorId).FirstOrDefault();
                    honorList.bReviewOfWorks = true;
                    if (honorList != null)
                    {
                        honor honorInfo = new honor();
                        honorInfo.ID = honorList.ID;
                        honorInfo.vcHonorName = honorList.vcHonorName;
                        honorInfo.dSubmitTime = honorList.dSubmitTime;
                        honorInfo.vcHonorIntroduce = honorList.vcHonorIntroduce;
                        honorInfo.vcGuideTeacher = honorList.vcGuideTeacher;
                        honorInfo.vcHonorSubmitAddress = honorList.vcHonorSubmitAddress;
                        honorInfo.vcHonorSliderAddress = honorList.vcHonorSliderAddress;
                        honorInfo.vcPreviewAddress = honorList.vcPreviewAddress;
                        honorInfo.vcNetConnectAddress = honorList.vcNetConnectAddress;
                        honorInfo.vcDownLoadAddress = honorList.vcDownLoadAddress;
                        honorInfo.vcTechnicalType = honorList.vcTechnicalType;
                        honorInfo.bReviewOfWorks = honorList.bReviewOfWorks;//审核
                        honorInfo.iTeamID = honorList.iTeamID;
                        honorInfo.bDownLoadUnable = honorList.bDownLoadUnable;
                        honorInfo.IsDel = honorList.IsDel;
                        try
                        {
                            if (honorService.EditEntity(honorInfo))
                            {
                                status = "ok";
                                mess = "审核通过";
                            }
                            else
                            {
                                status = "fail";
                                mess = "审核不通过";
                            }
                        }
                        catch (Exception)
                        {
                            status = "fail";
                            mess = "网络连接不稳定，请稍后再试";
                        }
                    }
                }
                catch (Exception)
                {
                    status = "fail";
                    mess = "网络连接不稳定，请稍后再试";
                }
            }
            if (suggestion == "disagress")
            {
                status = "fail";
                mess = "审核不通过";
            }
            result = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + mess + "\"" + "}";
            return Content(result);
        }
    }
}