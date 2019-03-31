using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.IBLL
{
    public partial interface IwebmasterService : IBaseService<webmaster>
    {
        // 根据ID得到对应Header的详细信息
        IQueryable<webmaster> LoadHeaderById(int id);
        // 根据ID得到对应Slider的详细信息
        IQueryable<webmaster> LoadSliderById(int id);
        // 根据ID得到对应Footer的详细信息
        IQueryable<webmaster> LoadFooterById(int id);
        //根据name获取数据库中的一行数据
        webmaster GetWebMasterByName(string name);
    }
}
