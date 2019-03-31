using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.IBLL
{
    public partial interface IhonorService : IBaseService<honor>
    {
        /// <summary>
        /// 武利敏
        /// 根据HonorId拿到有关HonorId的所有信息
        /// </summary>
        /// <param name="honorId"></param>
        /// <returns></returns>
        IQueryable<honor> GetHonorList(List<int> honorId);
        /// <summary>
        /// 武利敏
        /// 根据HonorName拿到有关HonorName的所有信息
        /// </summary>
        /// <param name="honorId"></param>
        /// <returns></returns>
        IQueryable<honor> GetHonorListByName(List<string> honorName);
    }
}
