//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BAR.TeamManager.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class team
    {
        public int ID { get; set; }
        public string vcTeamSliderAddress { get; set; }
        public string vcTeamName { get; set; }
        public string vcGuideTeacher { get; set; }
        public string vcTeamLogoAddress { get; set; }
        public string vcTeamIntroduce { get; set; }
        public Nullable<System.DateTime> dTeamSetupTime { get; set; }
        public string vcTeamMain { get; set; }
        public Nullable<bool> bCheckedTeacher { get; set; }
        public Nullable<bool> bCheckedcounselor { get; set; }
        public Nullable<bool> IsDel { get; set; }
    }
}