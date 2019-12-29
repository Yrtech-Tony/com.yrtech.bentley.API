using System;
using System.Collections.Generic;
using com.yrtech.bentley.DAL;

namespace com.yrtech.InventoryAPI.DTO
{
    [Serializable]
    public class MarketActionAfter2LeadsReportDto
    {
            public int MarketActionId { get; set; }
            public string ActionName { get; set; }
            public int ShopId { get; set; }
            public string ShopName { get; set; }
            public string ShopNameEn { get; set; }
            public int SeqNO { get; set; }
            public string CustomerName { get; set; }
            public string BPNO { get; set; }
            public Nullable<bool> OwnerCheck { get; set; }
            public Nullable<bool> TestDriverCheck { get; set; }
            public Nullable<bool> LeadsCheck { get; set; }
            public string InterestedModel { get; set; }
            public Nullable<bool> DealCheck { get; set; }
            public string DealModel { get; set; }
            public Nullable<int> InUserId { get; set; }
            public Nullable<System.DateTime> InDateTime { get; set; }
            public Nullable<int> ModifyUserId { get; set; }
            public Nullable<System.DateTime> ModifyDateTime { get; set; }
    }
}