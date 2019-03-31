using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.BLL
{
    public partial class teamService : BaseService<team>, IteamService
    {

        IteamapplicantService TeamAppliCantService;
        IplayersService PlayersService;
        IpersonalinformationService PersonalinformationService;
        IteamapplicationService TeamapplicationService { get; set; }
        IregisterloginService RegisterloginService = new registerloginService();

        #region 获取所有参加荣耀的团队内容
        /// <summary>
        /// 获取所有参加荣耀的团队内容
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public IQueryable<team> LoadTeamList(List<int> teamId)
        {
            var teamList = this.CurrentDBSession.teamDal.LoadEntities(t => teamId.Contains(t.ID));
            return teamList;
        } 
        #endregion

        #region 获取团队当前队长信息

        /// <summary>
        /// 获取团队当前队长信息
        /// </summary>
        /// <param name="id">团队ID</param>
        /// <returns></returns>
        public personalinformation GetTeamCaptain(int id)
        {
            personalinformation person = new personalinformation();
            //通过团队ID找到TeamApplicantID的记录
            var teamAppliCantInfo = TeamAppliCantService.LoadEntities(u => u.iTeamID == id).FirstOrDefault();
            if (teamAppliCantInfo != null)
            {
                //通过队员表找到队员表中的队长记录
                var playerInfo = PlayersService.LoadEntities(u => u.iTeamApplicantID == teamAppliCantInfo.ID && u.bPlayerType == true).FirstOrDefault();
                if (playerInfo != null)
                {
                    //根据队长记录中的ID找到队长个人信息
                    var PersonInfo = PersonalinformationService.LoadEntities(u => u.iUserID == playerInfo.iUserID).FirstOrDefault();
                    person = PersonInfo;
                }
            }
            return person;
        }
        #endregion

        #region 获取团队指导老师的信息
        /// <summary>
        /// 获取团队指导老师的信息
        /// </summary>
        /// <param name="id">团队ID</param>
        /// <returns></returns>
        public personalinformation GetTeamTeacher(int id)
        {
            personalinformation person = new personalinformation();
            var teamAppliCantInfo = TeamAppliCantService.LoadEntities(u => u.iTeamID == id).FirstOrDefault();
            if (teamAppliCantInfo != null)
            {
                person = PersonalinformationService.LoadEntities(u => u.iUserID == teamAppliCantInfo.iUserID).FirstOrDefault();
            }
            return person;

        }
        #endregion

        #region 通过姓名获取教师的详细信息
        /// <summary>
        /// 通过姓名获取教师的详细信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public personalinformation GetTeacherByName(string name)
        {
            Model.personalinformation teacherInfo = new personalinformation();
            try
            {
                var teacherlist = PersonalinformationService.LoadEntities(u => u.vcName == name).ToList();
                if (teacherlist != null)
                {
                    List<int?> teacherList = new List<int?>();
                    foreach (var item in teacherlist)
                    {
                        teacherList.Add(item.iUserID);
                    }
                    var teacherLogin = RegisterloginService.LoadEntities(u => teacherList.Contains(u.iUserID) && u.iIdentity == 8).FirstOrDefault();
                    if (teacherLogin != null)
                    {
                        teacherInfo = PersonalinformationService.LoadEntities(u => u.iUserID == teacherLogin.iUserID).FirstOrDefault();
                    }
                }
            }
            catch (Exception)
            {
                teacherInfo = null;
            }
            return teacherInfo;

        } 
        #endregion

        #region 获取团队申请表信息
        /// <summary>
        /// 获取团队申请表信息
        /// </summary>
        /// <param name="id">团队ID</param>
        /// <returns>获取团队申请信息</returns>
        public teamapplication GetTeamapplication(int id)
        {
            teamapplication TeamapplicationInfo = new teamapplication();
            var Teamapplicant = TeamAppliCantService.LoadEntities(u => u.iTeamID == id).FirstOrDefault();
            if (Teamapplicant != null)
            {
                TeamapplicationInfo = TeamapplicationService.LoadEntities(u => u.ID == Teamapplicant.iTeamApplicationID).FirstOrDefault();
            }
            return TeamapplicationInfo;
        }
        #endregion

        #region 获取团队所有队员的个人信息
        /// <summary>
        /// 获取团队所有队员的个人信息
        /// </summary>
        /// <param name="id">团队ID</param>
        /// <returns></returns>
        public List<personalinformation> GetTeamPlayerList(int id)
        {
            List<personalinformation> TeamPlayerList = new List<personalinformation>();
            //通过团队ID找到TeamApplicantID的记录
            var teamAppliCantInfo = TeamAppliCantService.LoadEntities(u => u.iTeamID == id).FirstOrDefault();
            if (teamAppliCantInfo != null)
            {
                //通过队员表找到队员表中的所有队员记录
                var playerInfo = PlayersService.LoadEntities(u => u.iTeamApplicantID == teamAppliCantInfo.ID && u.bPlayerType == false).ToList();
                if (playerInfo != null)
                {
                    //减少数据库访问数次
                    List<int?> playerInfoIDList = new List<int?>();
                    foreach (var item in playerInfo)
                    {
                        playerInfoIDList.Add(item.iUserID);
                    }
                    TeamPlayerList = PersonalinformationService.LoadEntities(u => playerInfoIDList.Contains(u.iUserID)).ToList();
                }
            }
            return TeamPlayerList;
        }
        #endregion

        #region 获取User所在Team
        /// <summary>
        /// 获取User所在Team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.team GetTeamByIuserId(int id)
        {
            Model.team teamInfo = new Model.team();
            var playerInfo = PlayersService.LoadEntities(u => u.iUserID == id).FirstOrDefault();
            if (playerInfo != null)
            {
                var teamApplicant = TeamAppliCantService.LoadEntities(u => u.ID == playerInfo.iTeamApplicantID).FirstOrDefault();
                if (teamApplicant != null)
                {
                    var team = this.LoadEntities(u => u.ID == teamApplicant.iTeamID && u.IsDel == false).FirstOrDefault();
                    teamInfo = team;
                }
            }
            return teamInfo;
        }
        #endregion

        #region 通过iUserID或List<iUserID>获取personinfo信息
        /// <summary>
        /// 获取personinfo信息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public Model.personalinformation GetPerson(int iUserID)
        {
            return this.PersonalinformationService.LoadEntities(u => u.iUserID == iUserID).FirstOrDefault();
        }
        /// <summary>
        /// 通过iUserID集合获取相对应的personinfo列表
        /// </summary>
        /// <param name="iUserIDList"></param>
        /// <returns></returns>
        public List<Model.personalinformation> GetPerson(List<int?> iUserIDList)
        {
            return this.PersonalinformationService.LoadEntities(u => iUserIDList.Contains(u.iUserID)).ToList();
        }
        #endregion

        #region 添加团队申请表信息        AddTeamapplication(teamapplication teamapplication)
        /// <summary>
        /// 添加团队申请表信息
        /// </summary>
        /// <param name="teamapplication"></param>
        /// <returns></returns>
        public teamapplication AddTeamapplication(teamapplication teamapplication)
        {
            return TeamapplicationService.AddEntity(teamapplication);
        }
        #endregion

        #region 删除团队申请表信息       DeleteTeamapplication(Model.teamapplication teamapplication)
        /// <summary>
        /// 删除团队申请表信息  
        /// </summary>
        /// <param name="teamapplication"></param>
        /// <returns></returns>
        public bool DeleteTeamapplication(Model.teamapplication teamapplication)
        {
            teamapplication.IsDel = true;
            return TeamapplicationService.EditEntity(teamapplication);
        }

        #endregion

        #region 添加团队申请人表  AddTeamapplicant
        /// <summary>
        /// 添加团队申请人表  
        /// </summary>
        /// <param name="teamapplicant"></param>
        /// <returns></returns>
        public Model.teamapplicant AddTeamapplicant(Model.teamapplicant teamapplicant)
        {
            return TeamAppliCantService.AddEntity(teamapplicant);
        }

        #endregion

        #region 删除团队申请人表 DeleteTeamapplicant
        /// <summary>
        /// 删除团队申请人表
        /// </summary>
        /// <param name="teamapplicant"></param>
        /// <returns></returns>
        public bool DeleteTeamapplicant(Model.teamapplicant teamapplicant)
        {
            teamapplicant.IsDel = true;
            return TeamAppliCantService.EditEntity(teamapplicant);
        }



        #endregion

        #region 添加队员列表 AddPlayerList
        public bool AddPlayerList(List<players> playerlist)
        {
            return PlayersService.AddEntityList(playerlist);
        }

        #endregion

        #region 添加队员  AddPlayer(players player)
        public bool AddPlayer(players player)
        {
            var Player = PlayersService.AddEntity(player);
            return Player != null && Player.ID != 0 ? true : false;
        }
        #endregion

        #region 修改队长身份 已注释
        //public bool EditTeamCaptionType(int iUserId)
        //{
        //    var TeamCaption = RegisterloginService.LoadEntities(u => u.iUserID == iUserId).FirstOrDefault();
        //    if (TeamCaption!=null&& TeamCaption.ID!=0)
        //    {
        //        TeamCaption.iIdentity = 4;
        //        return RegisterloginService.EditEntity(TeamCaption);
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
        #endregion

    }
}
