using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.BLL
{
    public partial class webmasterService : BaseService<webmaster>, IwebmasterService
    {

        /// <summary>
        /// 通过headID得到head里的数据
        /// </summary>
        /// <param name="id">头部ID</param>
        /// <returns></returns>
        public IQueryable<webmaster> LoadHeaderById(int id)
        {
            var headerList = this.CurrentDal.LoadEntities(header => header.ID == id);
            return headerList;
        }

        /// <summary>
        /// 通过sliderID得到slider的数据
        /// </summary>
        /// <param name="id">轮播图ID</param>
        /// <returns></returns>
        public IQueryable<webmaster> LoadSliderById(int id)
        {
            var sliderList = this.CurrentDal.LoadEntities(slider => slider.ID == id);
            return sliderList;
        }
        /// <summary>
        /// 通过footerID得到footer的数据
        /// </summary>
        /// <param name="id">尾部的ID</param>
        /// <returns></returns>
        public IQueryable<webmaster> LoadFooterById(int id)
        {
            var footerList = this.CurrentDal.LoadEntities(footer => footer.ID == id);
            return footerList;
        }
        /// <summary>
        /// 根据name获取数据库中的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public webmaster GetWebMasterByName(string name)
        {
            var webMaster = this.CurrentDBSession.webmasterDal.GetWebmasterByName(name).FirstOrDefault();
            return webMaster;
        }

    }
}
