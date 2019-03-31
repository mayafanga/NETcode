using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.Model;

namespace BAR.TeamManager.IBLL
{
    public partial interface IhonorparticipantmemberService : IBaseService<honorparticipantmember>
    {
        /// <summary>
        /// 获取有此荣耀ID的荣耀参与成员内容
        /// </summary>
        /// <param name="honorId"></param>
        /// <returns></returns>
        IQueryable<honorparticipantmember> GetHonorMemberList(List<int> honorId);

        /// <summary>
        /// 武利敏
        /// 
        /// 找到此用户所对应的所有荣耀Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<int> GetHonorId(int userId);
    }
}
