using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;

namespace BAR.TeamManager.BLL
{
    public partial class honorparticipantmemberService : BaseService<honorparticipantmember>, IhonorparticipantmemberService
    {
        /// <summary>
        /// 获取有此荣耀ID的荣耀参与成员内容
        /// </summary>
        /// <param name="honorId"></param>
        /// <returns></returns>
        public IQueryable<honorparticipantmember> GetHonorMemberList(List<int> honorId)
        {
            var honorMemberList = this.CurrentDBSession.honorparticipantmemberDal.LoadEntities(h => honorId.Contains(h.ID));
            return honorMemberList;
        }
        /// <summary>
        /// 武利敏
        /// 
        /// 找到此用户所对应的所有荣耀Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<int> GetHonorId(int userId)
        {
            var honorMemberList = this.CurrentDBSession.honorparticipantmemberDal.LoadEntities(h => h.iUserID == userId).ToList();
            List<int> honorList = new List<int>();
            foreach(var honorMember in honorMemberList)
            {
                honorList.Add((int)honorMember.iHonorID);
            }
            return honorList;
        }

    }
}
