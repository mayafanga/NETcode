using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Common;
using BAR.TeamManager.Model;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class BackManageTeamController : Controller
    {
        IteamService TeamService { get; set; }
        IteamapplicantService TeamapplicantService { get; set; }
        IplayersService PlayersService { get; set; }
        // GET: BackManageTeam
        public ActionResult Index()
        {
            return View();
        }
        #region get请求的页面  没有点击页面上的按钮
        /// <summary>
        /// 获取团队表中的所有内容   分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTeamALL()
        {
            string jsonTxt = null;
            List<Model.ViewModel.BackDataModel.TeamAll> teamAllList = new List<Model.ViewModel.BackDataModel.TeamAll>();
            Model.ViewModel.BackDataModel.TeamAll team = new Model.ViewModel.BackDataModel.TeamAll();
            try
            {
                var teamAll = TeamService.LoadEntities(t => true).ToList();
                if (teamAll != null)
                {
                    foreach (var t in teamAll)
                    {
                        team.teamId = t.ID;
                        team.Logo = t.vcTeamLogoAddress;
                        team.teamName = t.vcTeamName;
                        team.guidTeacher = t.vcGuideTeacher;
                        team.SetupTime = (DateTime)t.dTeamSetupTime;
                        team.teamMain = t.vcTeamMain;
                        if (t.bCheckedTeacher == true && t.bCheckedcounselor == true)
                        {
                            team.check = "已审核";
                        }
                        else
                        {
                            team.check = "未审核";
                        }
                        team.teamIntroduce = t.vcTeamIntroduce;
                        teamAllList.Add(team);
                    }
                    var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
                    jsonTxt = JsonConvert.SerializeObject(teamAllList, Newtonsoft.Json.Formatting.Indented);
                }
                else
                {
                    jsonTxt = "空空如也";
                }
            }
            catch(Exception ex)
            {
                jsonTxt = "网络出现异常" + ex.ToString();
            }

            return Content(jsonTxt);
            
        }
        /// <summary>
        /// 拿到单个团队的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSoleTeam()
        {
            string jsonTxt = null;
            string id = Request["teamId"];
            if (id != null)
            {
                int teamId = Convert.ToInt32(id);
                try
                {
                    Model.ViewModel.BackDataModel.TeamSloe teamsole = new Model.ViewModel.BackDataModel.TeamSloe();
                    List<Model.ViewModel.BackDataModel.Person> personList = new List<Model.ViewModel.BackDataModel.Person>();
                    Model.ViewModel.BackDataModel.Person person = new Model.ViewModel.BackDataModel.Person();
                    var team = TeamService.LoadEntities(t => t.ID == teamId).FirstOrDefault();
                    if (team != null)
                    {
                        teamsole.Logo = team.vcTeamLogoAddress;
                        teamsole.teamName = team.vcTeamName;
                        teamsole.guidTeacher = team.vcGuideTeacher;
                        teamsole.teamMain = team.vcTeamMain;
                        teamsole.SetupTime = (DateTime)team.dTeamSetupTime;
                        teamsole.teamIntroduce = team.vcTeamIntroduce;
                        teamsole.teamSlider = team.vcTeamSliderAddress;
                        if (team.bCheckedcounselor == true && team.bCheckedTeacher == true)
                        {
                            teamsole.check = "已审核";
                        }
                        else
                        {
                            teamsole.check = "未审核";
                        }
                        //获取团队成员信息  1  在团队申请人表中找到teamId 找到团队申请人表ID   
                        //                  2  然后再队员表中通过团队申请人表ID找到所匹配的队员内容
                        var teamApplicant = TeamapplicantService.LoadEntities(ta => ta.iTeamID == team.ID).FirstOrDefault();
                        var playerList = PlayersService.LoadEntities(p => p.iTeamApplicantID == teamApplicant.ID).ToList();
                    }
                    else
                    {
                        jsonTxt = "空空如也";
                    }
                }
                catch(Exception ex)
                {
                    jsonTxt = "网络有问题" + ex.ToString();
                }
            }
            else
            {
                jsonTxt = "空空如也";
            }
            return Content(jsonTxt);
        }
        #endregion

        
    }
}