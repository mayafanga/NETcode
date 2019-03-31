using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BAR.TeamManager.BLL;
using BAR.TeamManager.Common;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;

namespace BAR.TeamManager.Common
{
    public class GetID
    {
        /// <summary>
        ///通过登录人，获取iUserID，获取iTeamID
        /// </summary>
        /// <returns></returns>
        public static int GetTeamID(HttpCookie loginUserCookie, IteamapplicantService TeamapplicantService)
        {
            int teamId = 0;
            //完善团队信息的时候要知道是完善的哪个团队的信息
            if (loginUserCookie != null)
            {
                string userIdstr = loginUserCookie.Values["loginUserId"];//用户的ID
                int userId = Convert.ToInt32(userIdstr);
                try
                {
                    var teamApplicant = TeamapplicantService.LoadEntities(ta => ta.iUserID == userId).FirstOrDefault();
                    if (userIdstr != null && teamApplicant == null)
                    {
                        teamId = -1;
                    }
                    else if (userIdstr != null && teamApplicant != null)
                    {
                        teamId = (int)teamApplicant.iTeamID;
                    }
                    else
                    {
                        teamId = 0;
                    }
                }
                catch (Exception)
                {
                    teamId = 0;
                }
            }
            return teamId;
        }
    }
}
