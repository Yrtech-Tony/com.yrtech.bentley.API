using System;
using System.Collections.Generic;
using com.yrtech.bentley.DAL;

namespace com.yrtech.InventoryAPI.DTO
{
    [Serializable]
    public class MarketActionBefore3MainDto
    {
        public int MarketActionId { get; set; }
        public List<MarketActionBefore3BugetDetail> BugetDetailList { get; set; }
        public List<MarketActionBefore3DisplayModel> DisplayModelList { get; set; }
        public List<MarketActionBefore3TestDriver> TestDriverList { get; set; }
    }
}