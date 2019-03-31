/*
 * ==========================================================================================
 * 
 * 创建人： 李瑞森 
 * 创建时间：2018年11月30日 17：52
 * 文件介绍：团队控制器，用于团队相关页面（团队入驻申请页面、所有团队、团队详细）的操作
 * 
 * ==========================================================================================
 * 
 * 修改：
 * 修改人：
 * 修改时间：
 * 修改内容：
 * 修改内容简介：
 * 
 * 
 * 
 * 
 * ==========================================================================================
 */


using BAR.TeamManager.IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class TeamController : Controller
    {

        IteamService TeamService;
        IuserService UserService;
        public ActionResult Index()
        {
            return View();
        }
        #region 所有团队页
        /// <summary>
        ///团队模块首页（所有团队信息页面）
        ///对应：teamInformationAll.html
        /// </summary>
        /// <returns></returns>
        // GET: Team
        public ActionResult GetTeamAll()
        {
            //获取团队信息
            List<BAR.TeamManager.Model.ViewModel.TeamStripModel> jsonIndexModelList = new List<BAR.TeamManager.Model.ViewModel.TeamStripModel>();
            try
            {
                List<BAR.TeamManager.Model.team> TeamList = TeamService.LoadEntities(teamInfo => teamInfo.IsDel == false).ToList();
                foreach (BAR.TeamManager.Model.team item in TeamList)
                {
                    BAR.TeamManager.Model.ViewModel.TeamStripModel teamStripModel = new BAR.TeamManager.Model.ViewModel.TeamStripModel();
                    teamStripModel.TeamLogAdd = item.vcTeamLogoAddress;
                    teamStripModel.TeamName = item.vcTeamName;
                    teamStripModel.TeamTeaName = item.vcGuideTeacher;
                    try
                    {
                        teamStripModel.TeamTime = Convert.ToDateTime(item.dTeamSetupTime).ToString("yyyy-MM-dd");
                    }
                    catch (Exception)
                    {

                        teamStripModel.TeamTime = "2018-09-30 00：00";
                    }
                    teamStripModel.TeamIntroduce = item.vcTeamIntroduce;
                    teamStripModel.TeamMain = item.vcTeamMain;
                    teamStripModel.TeamID = item.ID;
                    //获取各个团队的队长信息
                    var teamcaptain = TeamService.GetTeamCaptain(item.ID);
                    if (teamcaptain != null)
                    {
                        teamStripModel.TeamCaptain = teamcaptain.iUserID;
                        teamStripModel.TeamCaptainName = teamcaptain.vcName;
                    }
                    if (teamStripModel != null)
                    {
                        jsonIndexModelList.Add(teamStripModel);
                    }
                }
            }
            catch (Exception)
            {
                jsonIndexModelList = null;
            }
            string jsonTxt = JsonConvert.SerializeObject(jsonIndexModelList, Newtonsoft.Json.Formatting.Indented);

            return jsonTxt != null ? Content(jsonTxt) : Content("{\"status\":\"no\",\"result\":\"信息不存在\"}");

        }
        #endregion

        #region 团队入驻页
        /// <summary>
        /// 团队入驻申请页面
        /// 对应：teamApplyaction.html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TeamApplyaction()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TeamApplyaction(string teamPlayers)
        {
            string TeamPlayer = teamPlayers;                                //团队队员iUserID，需分割提取
            string TeamName = Request["teamName"];                          //团队名称
            string TeamType = Request["teamType"];                          //团队类型
            string TeamTeacherName = Request["teamTeacherName"];            //团队指导老师姓名，需找到老师信息
            string TeamCaptainID = Request["teamCaptainID"];                //团队队长iUserID
            string TeamCaptainInfo = Request["teamCaptainInfo"];            //团队队长简介
            string TeamExpectResult = Request["teamExpectResult"];          //团队预期成果
            string TeamActivityPlan = Request["teamActivityPlan"];          //团队活动计划
            string TeamProSituation = Request["teamProSituation"];          //团队项目情况

            #region 获取教师：teacherInfo  获取队长：teamCaptainInfo
            //获取教师
            var teacherInfo = TeamService.GetTeacherByName(TeamTeacherName);    //通过姓名获取教师信息
            if (teacherInfo == null && teacherInfo.ID == 0)
            {
                return Content("{\"status\":\"no\",\"result\":\"该姓名的教师信息未查询到\"}");
            }
            //获取队长try 
            Model.personalinformation teamCaptainInfo = new Model.personalinformation();
            try
            {
                teamCaptainInfo = TeamService.GetPerson(Convert.ToInt32(TeamCaptainID));
                if (teamCaptainInfo != null && teamCaptainInfo.ID != 0)
                {
                    //获取队长成功
                }
                else
                {
                    return Content("{\"status\":\"no\",\"result\":\"队长信息未查询到\"}");
                }
            }
            catch (Exception)
            {
                return Content("{\"status\":\"no\",\"result\":\"获取队长信息出错\"}");
            }
            #endregion

            #region 获取队员 playerPersonList
            //获取队员
            string[] teamplayerArry = TeamPlayer.Split(',');
            List<int?> TeamplayerIuseridList = new List<int?>();
            List<Model.personalinformation> playerPersonList=new List<Model.personalinformation>();
            foreach (var item in teamplayerArry)
            {
                try
                {
                    TeamplayerIuseridList.Add(Convert.ToInt32(item));
                }
                catch (Exception)
                {
                    return Content("{\"status\":\"no\",\"result\":\"队员信息中存在错误\"}");
                }
            }
            playerPersonList = TeamService.GetPerson(TeamplayerIuseridList);  //如果学生iUserID中有重复的，查询结果将会忽略重复内容
            if (playerPersonList.Count == teamplayerArry.Length)
            {
                //创建队员表
            }
            else
            {
                return Content("{\"status\":\"no\",\"result\":\"提交的队员信息部分未查询到\"}");
            }
            #endregion

            #region 创建团队申请表 teamapplication
            Model.teamapplication teamapplicationInfo = new Model.teamapplication();
            teamapplicationInfo.dApplyTime = DateTime.Now;
            teamapplicationInfo.vcTeamType = TeamType;
            teamapplicationInfo.iApplyNumber = TeamplayerIuseridList.Count + 1;
            teamapplicationInfo.bApplyState = false;
            teamapplicationInfo.vcTeamExpectResult = TeamExpectResult;
            teamapplicationInfo.vcTeamActivityPlan = TeamActivityPlan;
            teamapplicationInfo.vcTeamProSituation = TeamProSituation;
            teamapplicationInfo.vcApplicantIntroduce = TeamCaptainInfo;
            teamapplicationInfo.IsDel = false;
            //向团队申请表添加信息
            var teamapplication = TeamService.AddTeamapplication(teamapplicationInfo);
            if (teamapplication.ID==0 && teamapplication ==null)
            {
                return Content("{\"status\":\"no\",\"result\":\"团队申请信息未能成功保存\"}");
            }
            #endregion

            #region 创建团队表 team
            Model.team teamInfo = new Model.team();
            teamInfo.vcTeamName = TeamName;
            teamInfo.vcGuideTeacher = teacherInfo.vcName;
            teamInfo.dTeamSetupTime = DateTime.Now;
            teamInfo.bCheckedTeacher = true;
            teamInfo.bCheckedcounselor = false;
            teamInfo.IsDel = false;
            //向团队表添加信息
            var team = TeamService.AddEntity(teamInfo);
            if (team.ID==0 && team ==null)
            {
                return Content("{\"status\":\"no\",\"result\":\"团队信息未能成功保存\"}");
            }
            #endregion

            #region 创建团队申请人表 teamapplicant
            Model.teamapplicant teamapplicant = new Model.teamapplicant();
            if (teamapplication.ID!=0 && teamapplication !=null && team.ID!=0 && team !=null)
            {
                Model.teamapplicant teamapplicantInfo = new Model.teamapplicant();
                teamapplicantInfo.iTeamApplicationID = teamapplication.ID;
                teamapplicantInfo.iTeamID = team.ID;
                teamapplicantInfo.iUserID = teacherInfo.iUserID;
                teamapplicantInfo.IsDel = false;
                //向团队申请人表添加信息
                teamapplicant = TeamService.AddTeamapplicant(teamapplicantInfo);
            }
            else
            {
                //删除前面添加到团队申请人表和团队表信息
                try
                {
                    team.IsDel = true;
                    bool teamFalg = TeamService.EditEntity(team);
                    bool teamapplicationFalg = TeamService.DeleteTeamapplication(teamapplication);
                    if (teamFalg == true && teamapplicationFalg == true)
                    {
                        return Content("{\"status\":\"no\",\"result\":\"团队申请失败，团队申请人表和团队表创建失败\"}");
                    }
                    else if(!teamFalg)
                    {
                        return Content("{\"status\":\"no\",\"result\":\"团队申请失败，团队表删除失败\"}");
                    }else if (!teamapplicationFalg)
                    {
                        return Content("{\"status\":\"no\",\"result\":\"团队申请失败，团队申请人表删除失败\"}");
                    }
                    else
                    {
                        return Content("{\"status\":\"no\",\"result\":\"团队申请失败，团队申请人表和团队表删除失败\"}");
                    }

                }
                catch (Exception)
                {

                    return Content("{\"status\":\"no\",\"result\":\"团队申请失败，团队申请人表和团队表删除出错\"}");
                }
            }
            #endregion

            #region 添加队员表 PlayFalg
            List<Model.players> playersList = new List<Model.players>();
            foreach (var item in playerPersonList)
            {
                Model.players playersItem = new Model.players();
                playersItem.iUserID = item.iUserID;
                playersItem.bPlayerType = false;
                playersItem.iTeamApplicantID = teamapplicant.ID;
                playersItem.IsDel = false;
                playersList.Add(playersItem);
            }
            bool PlayFalg = TeamService.AddPlayerList(playersList);
            if (!PlayFalg)
            {
                return Content("{\"status\":\"no\",\"result\":\"队员信息添加失败\"}");
            }
            #endregion

            #region 添加队长 TeamCaptionPlayerFlag
            bool TeamCaptionPlayerFlag;
            Model.players TeamCaptionPlayer = new Model.players();
            TeamCaptionPlayer.iUserID = teamCaptainInfo.iUserID;
            TeamCaptionPlayer.iTeamApplicantID = teamapplicant.ID;
            TeamCaptionPlayer.bPlayerType = true;
            TeamCaptionPlayer.IsDel = false;
            TeamCaptionPlayerFlag = TeamService.AddPlayer(TeamCaptionPlayer);
            if (!TeamCaptionPlayerFlag)
            {
                return Content("{\"status\":\"no\",\"result\":\"队长信息添加失败\"}");
            }
            #endregion

            //#region 修改队长身份  teamCaptainTypeFlag
            //bool teamCaptainTypeFlag = false;
            //if (teamCaptainInfo != null && teamCaptainInfo.ID != 0&& TeamCaptionPlayerFlag==true)
            //{
            //    teamCaptainTypeFlag = TeamService.EditTeamCaptionType(Convert.ToInt32(teamCaptainInfo.iUserID));
            //}
            //#endregion

            return Content("{\"status\":\"ok\",\"result\":\"添加成功\"}");
        }
        #endregion

        #region 团队详细信息

        [HttpGet]
        public ActionResult TeamInfoSingle()
        {
            return View();
        }
        /// <summary>
        /// 团队详细信息页面
        /// 对应：teamInfoSingle.html
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TeamInfoSingle(int id)
        {
            teamInfoSingleJsonModel teamInfoSingleJsonModel = new teamInfoSingleJsonModel();

            try
            {
                var team = TeamService.LoadEntities(u => u.ID == id).FirstOrDefault();
                if (team != null)
                {
                    teamInfoSingleJsonModel.TeamID = team.ID;
                    teamInfoSingleJsonModel.TeamName = team.vcTeamName;
                    teamInfoSingleJsonModel.TeamSliderAdd = team.vcTeamSliderAddress;
                    teamInfoSingleJsonModel.TeamMain = team.vcTeamMain;
                    teamInfoSingleJsonModel.TeamIntroduce = team.vcTeamIntroduce;
                    try
                    {
                        teamInfoSingleJsonModel.TeamSetupTime = Convert.ToDateTime(team.dTeamSetupTime).ToString("yyyy-MM-dd");
                    }
                    catch (Exception)
                    {
                        teamInfoSingleJsonModel.TeamSetupTime = "未知";
                    }
                    var TeamApplication = TeamService.GetTeamapplication(team.ID);
                    if (TeamApplication != null && TeamApplication.ID != 0)
                    {
                        teamInfoSingleJsonModel.TeamApplicationLocation = TeamApplication.vcTeamApplicationLocation;
                        teamInfoSingleJsonModel.TeamExpectResult = TeamApplication.vcTeamExpectResult;
                    }

                    #region 成员信息
                    //教师信息
                    BAR.TeamManager.Model.ViewModel.TeamPlayersStripModel teacherInfo = new TeamManager.Model.ViewModel.TeamPlayersStripModel();
                    var teacher = TeamService.GetTeamTeacher(team.ID);
                    if (teacher != null && teacher.ID != 0)
                    {
                        teacherInfo.PalyID = teacher.iUserID;
                        teacherInfo.PlayName = teacher.vcName;
                        teacherInfo.PlayGender = teacher.vcGender;
                        teacherInfo.PlayType = BAR.TeamManager.Model.EnumType.IdentityEnumType.Teacher;
                        teacherInfo.PlayIphone = teacher.cPhone;
                        teacherInfo.PlayIntroduction = teacher.vcPersonalIntroduce;
                        teacherInfo.PlayHobby = teacher.vcHobby;
                        teacherInfo.PlayerPhoAdd = UserService.GetUserPhotoAdd(Convert.ToInt32(teacher.iUserID));
                        teamInfoSingleJsonModel.TeamPlayersStripModelList.Add(teacherInfo);

                        teamInfoSingleJsonModel.TeamTeacherID = teacher.iUserID;
                        teamInfoSingleJsonModel.TeamTeacherName = teacher.vcName;

                    }
                    //队长信息
                    BAR.TeamManager.Model.ViewModel.TeamPlayersStripModel teamCaptainInfo = new TeamManager.Model.ViewModel.TeamPlayersStripModel();
                    var teamCaptain = TeamService.GetTeamCaptain(team.ID);
                    if (teamCaptain != null && teamCaptain.ID != 0)
                    {
                        teamCaptainInfo.PalyID = teamCaptain.iUserID;
                        teamCaptainInfo.PlayName = teamCaptain.vcName;
                        teamCaptainInfo.PlayGender = teamCaptain.vcGender;
                        teamCaptainInfo.PlayType = BAR.TeamManager.Model.EnumType.IdentityEnumType.Captain;
                        teamCaptainInfo.PlayIphone = teamCaptain.cPhone;
                        teamCaptainInfo.PlayIntroduction = teamCaptain.vcPersonalIntroduce;
                        teamCaptainInfo.PlayHobby = teamCaptain.vcHobby;
                        var teamCaptainUser = UserService.LoadEntities(u => u.ID == teamCaptain.iUserID).FirstOrDefault();
                        teamCaptainInfo.PlayerPhoAdd = UserService.GetUserPhotoAdd(Convert.ToInt32(teamCaptain.iUserID));
                        teamInfoSingleJsonModel.TeamPlayersStripModelList.Add(teamCaptainInfo);
                    }
                    List<TeamManager.Model.personalinformation> PlayerList = new List<TeamManager.Model.personalinformation>();
                    PlayerList = TeamService.GetTeamPlayerList(team.ID);
                    if (PlayerList != null)
                    {
                        foreach (var item in PlayerList)
                        {
                            BAR.TeamManager.Model.ViewModel.TeamPlayersStripModel PlayerInfo = new TeamManager.Model.ViewModel.TeamPlayersStripModel();
                            PlayerInfo.PalyID = item.iUserID;
                            PlayerInfo.PlayName = item.vcName;
                            PlayerInfo.PlayGender = item.vcGender;
                            PlayerInfo.PlayType = BAR.TeamManager.Model.EnumType.IdentityEnumType.Member;
                            PlayerInfo.PlayIphone = item.cPhone;
                            PlayerInfo.PlayIntroduction = item.vcPersonalIntroduce;
                            PlayerInfo.PlayHobby = item.vcHobby;
                            var PlayerUser = UserService.LoadEntities(u => u.ID == item.iUserID).FirstOrDefault();
                            PlayerInfo.PlayerPhoAdd = UserService.GetUserPhotoAdd(Convert.ToInt32(item.iUserID));
                            teamInfoSingleJsonModel.TeamPlayersStripModelList.Add(PlayerInfo);
                        }
                    }
                    #endregion

                }
            }
            catch (Exception)
            {
                teamInfoSingleJsonModel = null;
            }
            string jsonTxt = JsonConvert.SerializeObject(teamInfoSingleJsonModel, Newtonsoft.Json.Formatting.Indented);
            return jsonTxt != null ? Content(jsonTxt) : Content("{\"status\":\"no\",\"result\":\"信息不存在\"}");

        }
        #endregion

        #region 测试接口
        public ActionResult Test(string name)
        {
            var teamInfo = TeamService.GetTeacherByName(name);
            string jsonTxt = JsonConvert.SerializeObject(teamInfo, Newtonsoft.Json.Formatting.Indented);
            return Content(jsonTxt);

        }
        #endregion

        #region 通过学号查找预选队员

        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetPerson(string id)
        {
            personInfoModel personInfoModel = new personInfoModel();
            try
            {
                var userInfo = UserService.LoadEntities(u => u.vcUserAccount == id && u.IsDel == false).FirstOrDefault();
                if (userInfo.ID != 0 && userInfo != null)
                {
                    var personInfo = TeamService.GetPerson(userInfo.ID);
                    if (personInfo.ID != 0 && personInfo != null && personInfo.iUserID!=null && personInfo.vcName!=null && userInfo.vcUserAccount!=null&& personInfo.vcGender!=null && personInfo.vcMajor!=null && personInfo.cPhone!=null)
                    {
                        personInfoModel.iUserID = personInfo.iUserID;
                        personInfoModel.Name = personInfo.vcName;
                        personInfoModel.StudentID = userInfo.vcUserAccount;
                        personInfoModel.Gender = personInfo.vcGender;
                        personInfoModel.Major = personInfo.vcMajor;
                        personInfoModel.Phone = personInfo.cPhone;
                        try
                        {
                            int a = DateTime.Now.Year - Convert.ToDateTime(personInfo.dBirthday).Year;
                            a = a > 0 && a < 65 ? a : 20;
                            personInfoModel.Age = a.ToString();
                        }
                        catch (Exception)
                        {

                            personInfoModel.Age = "18";
                        }
                    }
                    else
                    {
                        return Content("{\"status\":\"no\",\"result\":\"学生信息不完善\"}");
                    }
                }
            }
            catch (Exception)
            {
                personInfoModel = null;
            }
            if (personInfoModel!=null)
            {
                string jsonTxt = JsonConvert.SerializeObject(personInfoModel, Newtonsoft.Json.Formatting.Indented);
                return Content(jsonTxt);
            }
             return Content("{\"status\":\"no\",\"result\":\"信息不存在\"}");
        }
        #endregion

        #region 通过学号查找预选队长

        public ActionResult GetCaptainInfo(string id)
        {
            var userInfo = UserService.LoadEntities(u => u.vcUserAccount == id && u.IsDel == false).FirstOrDefault();
            if (userInfo != null)
            {
                var personInfo = TeamService.GetPerson(userInfo.ID);
                if (personInfo!=null&&personInfo.vcEmail != null && personInfo.vcName != null && personInfo.vcGender != null && personInfo.vcMajor != null && personInfo.vcGrade != null && personInfo.cPhone != null)
                {
                    string jsonTxt = JsonConvert.SerializeObject(new { iUserID = personInfo.iUserID, studentID = id, Name = personInfo.vcName, Gender = personInfo.vcGender, Grade = personInfo.vcGrade, Major = personInfo.vcMajor, Phone = personInfo.cPhone, Email = personInfo.vcEmail }, Newtonsoft.Json.Formatting.Indented);
                    return Content(jsonTxt);
                }
                else
                {
                    return Content("{\"status\":\"no\",\"result\":\"学生信息不完善\"}");
                }
               
            }
            return Content("{\"status\":\"no\",\"result\":\"学号不存在\"}");
        } 
        #endregion
        
        #region 团队详细页ViewModel模板

        class teamInfoSingleJsonModel
        {
            /// <summary>
            /// 团队ID
            /// </summary>
            public int TeamID { get; set; }
            /// <summary>
            /// 团队名称
            /// </summary>
            public string TeamName { get; set; }
            /// <summary>
            /// 团队轮播图地址
            /// </summary>
            public string TeamSliderAdd { get; set; }
            /// <summary>
            /// 团队简介
            /// </summary>
            public string TeamIntroduce { get; set; }
            /// <summary>
            /// 团队主打
            /// </summary>
            public string TeamMain { get; set; }
            /// <summary>
            /// 预期效果
            /// </summary>
            public string TeamExpectResult { get; set; }
            /// <summary>
            /// 团队成立时间
            /// </summary>
            public string TeamSetupTime { get; set; }
            /// <summary>
            /// 团队注册地址
            /// </summary>
            public string TeamApplicationLocation { get; set; }
            /// <summary>
            /// 团队发起人（教师）
            /// </summary>
            public string TeamTeacherName { get; set; }
            /// <summary>
            /// 团队发起人ID
            /// </summary>
            public int? TeamTeacherID { get; set; }
            /// <summary>
            /// 团队成员链表
            /// </summary>
            public List<BAR.TeamManager.Model.ViewModel.TeamPlayersStripModel> TeamPlayersStripModelList = new List<TeamManager.Model.ViewModel.TeamPlayersStripModel>();
        }

        #endregion

        #region 数据模板
        public class personInfoModel
        {
            /// <summary>
            /// 
            /// </summary>
            public int? iUserID { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 学号
            /// </summary>
            public string StudentID { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public string Gender { get; set; }
            /// <summary>
            /// 年龄
            /// </summary>
            public string Age { get; set; }
            /// <summary>
            /// 专业
            /// </summary>
            public string Major { get; set; }
            /// <summary>
            /// 电话
            /// </summary>
            public string Phone { get; set; }
        }

        #endregion
    }
    
}