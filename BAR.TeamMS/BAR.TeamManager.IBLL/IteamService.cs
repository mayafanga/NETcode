using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.IBLL
{
    public partial interface IteamService : IBaseService<team>
    {
        /// <summary>
        ///  获取所有参加荣耀的团队内容
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        IQueryable<team> LoadTeamList(List<int> teamId);

        /// <summary>
        /// 获取团队队长信息
        /// </summary>
        /// <param name="id">团队ID</param>
        /// <returns>团队队长的个人信息</returns>
        personalinformation GetTeamCaptain(int id);
        /// <summary>
        /// 获取团队指导老师
        /// </summary>
        /// <param name="id">团队ID</param>
        /// <returns></returns>
        personalinformation GetTeamTeacher(int id);
        /// <summary>
        /// 获取团队申请表信息
        /// </summary>
        /// <param name="id">团队ID</param>
        /// <returns>获取团队申请信息</returns>
        teamapplication GetTeamapplication(int id);
        /// <summary>
        /// 获取团队所有队员的个人信息
        /// </summary>
        /// <param name="id">团队ID</param>
        /// <returns></returns>
        List<personalinformation> GetTeamPlayerList(int id);

        /// <summary>
        /// 通过用户iUserID获取所在团队信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Model.team GetTeamByIuserId(int id);
        /// <summary>
        /// 获取personinfo信息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        Model.personalinformation GetPerson(int iUserID);
        /// <summary>
        /// 通过iUserID集合获取相对应的personinfo列表
        /// </summary>
        /// <param name="iUserIDList"></param>
        /// <returns></returns>
        List<Model.personalinformation> GetPerson(List<int?> iUserIDList);
                /// <summary>
        /// 通过姓名获取教师的详细信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        personalinformation GetTeacherByName(string name);
        /// <summary>
        /// 添加团队申请表信息
        /// </summary>
        /// <param name="teamapplication"></param>
        /// <returns></returns>
        Model.teamapplication AddTeamapplication(Model.teamapplication teamapplication);
        /// <summary>
        /// 删除团队申请表信息
        /// </summary>
        /// <param name="teamapplication"></param>
        /// <returns></returns>
        bool DeleteTeamapplication(Model.teamapplication teamapplication);
        /// <summary>
        /// 添加团队申请人表
        /// </summary>
        /// <param name="teamapplicant"></param>
        /// <returns></returns>
        Model.teamapplicant AddTeamapplicant(Model.teamapplicant teamapplicant);
        /// <summary>
        /// 删除团队申请人表
        /// </summary>
        /// <param name="teamapplicant"></param>
        /// <returns></returns>
        bool DeleteTeamapplicant(Model.teamapplicant teamapplicant);
        /// <summary>
        /// 添加队员列表
        /// </summary>
        /// <param name="playerlist"></param>
        /// <returns></returns>
        bool AddPlayerList(List<players> playerlist);
        /// <summary>
        /// 添加 队员
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        bool AddPlayer(players player);
        /// <summary>
        ///// 修改队长身份
        ///// </summary>
        ///// <param name="iUserId"></param>
        ///// <returns></returns>
        //bool EditTeamCaptionType(int iUserId);
    }
}
