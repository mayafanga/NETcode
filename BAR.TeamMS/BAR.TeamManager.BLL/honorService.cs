using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BAR.TeamManager.BLL
{
    /// <summary>
    /// 荣耀信息
    /// </summary>
    public partial class honorService : BaseService<honor>, IhonorService
    {
        /// <summary>
        /// 武利敏
        /// 根据HonorId拿到有关HonorId的所有信息
        /// </summary>
        /// <param name="honorId"></param>
        /// <returns></returns>
        public IQueryable<honor> GetHonorList(List<int> honorId)
        {
            var honorList = this.CurrentDBSession.honorDal.LoadEntities(h => honorId.Contains(h.ID));
            return honorList;
        }
        /// <summary>
        /// 武利敏
        /// 根据HonorName拿到有关HonorName的所有信息
        /// </summary>
        /// <param name="honorId"></param>
        /// <returns></returns>
        public IQueryable<honor> GetHonorListByName(List<string> honorName)
        {
            var honorList = this.CurrentDBSession.honorDal.LoadEntities(h => honorName.Contains(h.vcHonorName));
            return honorList;
        }
    }
}
