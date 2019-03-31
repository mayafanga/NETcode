/************************************************************************************
* Copyright (c) 2019Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: BAR.TeamManager.BLL
*文件名: playersService
*版本号: V1.0.0.0
*唯一标识: e42545ad-c0b4-45fc-bb25-e9b768594801
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2019/1/2 15:26:53
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2019/1/2 15:26:53
*修改人: Administrator
*版本号: V1.0.0.0
*描述：
*
*
************************************************************************************/
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.BLL
{
    public partial class playersService : BaseService<players>, IplayersService
    {
        /// <summary>
        /// 添加队员集合
        /// </summary>
        /// <param name="playList"></param>
        /// <returns></returns>
        public bool AddEntityList(List<players> playList)
        {
            foreach (var item in playList)
            {
                this.CurrentDal.AddEntity(item);
            }
            return this.CurrentDBSession.SaveChanges();
        }
    }
}
