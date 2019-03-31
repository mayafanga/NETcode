/************************************************************************************
* Copyright (c) 2018Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: BAR.TeamManager.Model.ViewModel
*文件名: TeamPlayersStripModel
*版本号: V1.0.0.0
*唯一标识: 7386cb45-e70c-4ade-8eb6-f9c378730160
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/12/11 21:52:56
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/12/11 21:52:56
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

namespace BAR.TeamManager.Model.ViewModel
{
    /// <summary>
    /// 团队队员信息复用接口json数据Model
    /// </summary>
    public class TeamPlayersStripModel
    {

        /// <summary>
        /// 队员Log
        /// </summary>
        public string PlayerPhoAdd { get; set; }
        /// <summary>
        /// 队员姓名
        /// </summary>
        public string PlayName { get; set; }
        /// <summary>
        /// 队员性别
        /// </summary>
        public string PlayGender { get; set; }
        /// <summary>
        /// 团队职务/身份：Teacher/Captain/Member
        /// </summary>
        public EnumType.IdentityEnumType PlayType { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PlayIphone { get; set; }
        /// <summary>
        /// 个人简介
        /// </summary>
        public string PlayIntroduction { get; set; }
        /// <summary>
        /// 兴趣爱好
        /// </summary>
        public string PlayHobby { get; set; }
        /// <summary>
        /// 成员ID/iUserID
        /// </summary>
        public int? PalyID { get; set; }
    }
}
