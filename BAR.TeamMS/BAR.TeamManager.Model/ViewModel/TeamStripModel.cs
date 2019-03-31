/************************************************************************************
*命名空间: BAR.TeamManager.Model.ViewModel
*文件名: TeamStripModel
*版本号: V1.0.0.0
*唯一标识: 2f5a4e53-dd94-4bd6-b47e-8c1131ba8642
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/12/11 21:35:28
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/12/11 21:35:28
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
    /// 所有团队首页 各个团队条复用接口json数据Model
    /// </summary>
    public class TeamStripModel
    {
        /// <summary>
        /// 团队Log
        /// </summary>
        public string TeamLogAdd { get; set; }
        /// <summary>
        /// 团队名
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// 团队老师
        /// </summary>
        public string TeamTeaName { get; set; }
        /// <summary>
        /// 团队队长ID
        /// </summary>
        public int? TeamCaptain { get; set; }
        /// <summary>
        /// 团队队长姓名
        /// </summary>
        public string TeamCaptainName { get; set; }
        /// <summary>
        /// 团队创建时间
        /// </summary>
        public string TeamTime { get; set; }
        /// <summary>
        /// 团队简介
        /// </summary>
        public string TeamIntroduce { get; set; }
        /// <summary>
        /// 团队主打
        /// </summary>
        public string TeamMain { get; set; }
        /// <summary>
        /// 团队ID
        /// </summary>
        public int TeamID { get; set; }
    }
    
}
