﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class barteammanagedbEntities1 : DbContext
    {
        public barteammanagedbEntities1()
            : base("name=barteammanagedbEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<activitieapplication> activitieapplication { get; set; }
        public virtual DbSet<activity> activity { get; set; }
        public virtual DbSet<administrator> administrator { get; set; }
        public virtual DbSet<audit> audit { get; set; }
        public virtual DbSet<authority> authority { get; set; }
        public virtual DbSet<comment> comment { get; set; }
        public virtual DbSet<eventapplicant> eventapplicant { get; set; }
        public virtual DbSet<honor> honor { get; set; }
        public virtual DbSet<honorparticipantmember> honorparticipantmember { get; set; }
        public virtual DbSet<participatmembers> participatmembers { get; set; }
        public virtual DbSet<personalinformation> personalinformation { get; set; }
        public virtual DbSet<players> players { get; set; }
        public virtual DbSet<registerlogin> registerlogin { get; set; }
        public virtual DbSet<score> score { get; set; }
        public virtual DbSet<teacher> teacher { get; set; }
        public virtual DbSet<team> team { get; set; }
        public virtual DbSet<teamapplicant> teamapplicant { get; set; }
        public virtual DbSet<teamapplication> teamapplication { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<userauthority> userauthority { get; set; }
        public virtual DbSet<webmaster> webmaster { get; set; }
    }
}