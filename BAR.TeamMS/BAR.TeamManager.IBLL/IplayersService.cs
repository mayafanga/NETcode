/************************************************************************************
* Copyright (c) 2019Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: BAR.TeamManager.IBLL
*文件名: IplayersService
*版本号: V1.0.0.0
*唯一标识: 5fccbe54-ab77-42d4-adad-420d20a69bb2
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2019/1/2 15:28:07
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2019/1/2 15:28:07
*修改人: Administrator
*版本号: V1.0.0.0
*描述：
*
*
************************************************************************************/
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.IBLL
{
    public partial interface IplayersService : IBaseService<players>
    {
        bool AddEntityList(List<players> playList);
    }
}
