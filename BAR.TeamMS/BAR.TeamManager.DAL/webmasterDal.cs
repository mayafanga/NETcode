using BAR.TeamManager.IDAL;
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.DAL
{
    public partial class webmasterDal : BaseDal<webmaster>, IwebmasterDal
    {
        /// <summary>
        /// 根据name获取数据库中的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<webmaster> GetWebmasterByName(string name)
        {
            barteammanagedbEntities1 db = new barteammanagedbEntities1();
            var webMaster = from web in db.webmaster
                            where web.vcName.ToUpper() == name.ToUpper()
                            select web;
            return webMaster;
        }
    }
}
