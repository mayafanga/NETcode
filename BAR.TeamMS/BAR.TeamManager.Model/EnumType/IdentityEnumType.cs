/************************************************************************************
* Copyright (c) 2018Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: BAR.TeamManager.Model.EnumType
*文件名: IdentityEnumType
*版本号: V1.0.0.0
*唯一标识: c93162f2-434e-4a4d-9277-4be772a1f475
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/12/11 21:55:46
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/12/11 21:55:46
*修改人: Administrator
*版本号: V1.0.0.0
*描述：
*
*
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.Model.EnumType
{
    /// <summary>
    /// 身份类型枚举
    /// </summary>
    public enum IdentityEnumType
    {
        //Teacher/Captain/Member/Admin
        /// <summary>
        /// 队员
        /// </summary>
        Member=2,
        /// <summary>
        /// 队长
        /// </summary>
        Captain=4,
        /// <summary>
        /// 教师
        /// </summary>
        Teacher=8,
        /// <summary>
        /// 管理员
        /// </summary>
        Admin=16
    }
}
