﻿using System;
using System.Collections.Generic;
using com.yrtech.bentley.DAL;

namespace com.yrtech.InventoryAPI.DTO
{
    [Serializable]
    public class MarketActionBefore21Dto
    {
        public int MarketActionId { get; set; }
        public string ActivityBackground { get; set; }
        public string ActivityObjective { get; set; }
        public string ActivityDesc { get; set; }
        public Nullable<decimal> Budget { get; set; }
        public Nullable<int> TargetParticipationOwnerCount { get; set; }
        public Nullable<int> TargetParticipationPCCount { get; set; }
        public Nullable<int> TargetLeadsOwnerCount { get; set; }
        public Nullable<int> TargetLeadsPCCount { get; set; }
        public Nullable<int> TargetTestDriveOwnerCount { get; set; }
        public Nullable<int> TargetTestDrivePCCount { get; set; }
        public Nullable<int> TargetOrdersOwnerCount { get; set; }
        public Nullable<int> TargetOrdersPCCount { get; set; }
        public string KeyVisionPic { get; set; }
        public string KeyVisionDesc { get; set; }
        public string KeyVisionApprovalCode { get; set; }
        public string KeyVisionApprovalName { get; set; }
        public string KeyVisionApprovalNameEn { get; set; }
        public string KeyVisionApprovalDesc { get; set; }
        public string POSDesignPic01 { get; set; }
        public string POSDesignPic02 { get; set; }
        public string POSDesignPic03 { get; set; }
        public string POSDesignPic04 { get; set; }
        public string POSDesignDesc01 { get; set; }
        public string POSDesignDesc02 { get; set; }
        public string POSDesignDesc03 { get; set; }
        public string POSDesignDesc04 { get; set; }
        public string PlaceIntroPic01 { get; set; }
        public string PlaceIntroPic02 { get; set; }
        public string PlaceIntroPic03 { get; set; }
        public string PlaceIntroPic04 { get; set; }
        public string PlaceIntroDesc01 { get; set; }
        public string PlaceIntroDesc02 { get; set; }
        public string PlaceIntroDesc03 { get; set; }
        public string PlaceIntroDesc04 { get; set; }
        public string TestDriverRoadMapPic01 { get; set; }
        public string TestDriverRoadMapPic02 { get; set; }
        public string TestDriverRoadMapPic03 { get; set; }
        public string TestDriverRoadMapPic04 { get; set; }
        public string TestDriverRoadMapDesc01 { get; set; }
        public string TestDriverRoadMapDesc02 { get; set; }
        public string TestDriverRoadMapDesc03 { get; set; }
        public string TestDriverRoadMapDesc04 { get; set; }
        public string OthersPic01 { get; set; }
        public string OthersPic02 { get; set; }
        public string OthersPic03 { get; set; }
        public string OthersPic04 { get; set; }
        public string OtherDesc01 { get; set; }
        public string OtherDesc02 { get; set; }
        public string OtherDesc03 { get; set; }
        public string OtherDesc04 { get; set; }
        public Nullable<int> InUserId { get; set; }
        public Nullable<System.DateTime> InDateTime { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
    }
}