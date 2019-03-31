using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.IDAL
{
    public partial interface IwebmasterDal : IBaseDal<webmaster>
    {
        /// <summary>
        /// 根据name获取数据库中的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<webmaster> GetWebmasterByName(string name);
    }
}
