﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace com.yrtech.bentley.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Bentley : DbContext
    {
        public Bentley()
            : base("name=Bentley")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<CommitFile> CommitFile { get; set; }
        public virtual DbSet<MarketAction> MarketAction { get; set; }
        public virtual DbSet<MarketActionAfter7ActualProcess> MarketActionAfter7ActualProcess { get; set; }
        public virtual DbSet<MarketActionBefore21ActivityProcess> MarketActionBefore21ActivityProcess { get; set; }
        public virtual DbSet<MarketActionBefore3BugetDetail> MarketActionBefore3BugetDetail { get; set; }
        public virtual DbSet<MarketActionBefore3DisplayModel> MarketActionBefore3DisplayModel { get; set; }
        public virtual DbSet<MarketActionBefore3TestDriver> MarketActionBefore3TestDriver { get; set; }
        public virtual DbSet<MarketActionTheDayFile> MarketActionTheDayFile { get; set; }
        public virtual DbSet<RoleType> RoleType { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<ShopCommitFileRecord> ShopCommitFileRecord { get; set; }
        public virtual DbSet<MarketActionAfter30LeadsReportUpdate> MarketActionAfter30LeadsReportUpdate { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<HiddenCode> HiddenCode { get; set; }
        public virtual DbSet<MarketActionBefore21> MarketActionBefore21 { get; set; }
        public virtual DbSet<MarketActionAfter7> MarketActionAfter7 { get; set; }
        public virtual DbSet<MarketActionAfter7ActualExpense> MarketActionAfter7ActualExpense { get; set; }
        public virtual DbSet<MarketActionAfter2LeadsReport> MarketActionAfter2LeadsReport { get; set; }
        public virtual DbSet<MarketActionAfter90File> MarketActionAfter90File { get; set; }
    }
}
