using System;
using System.Collections.Generic;
using com.yrtech.bentley.DAL;

namespace com.yrtech.InventoryAPI.DTO
{
    [Serializable]
    public class MarketActionBefore21MainDto
    {
        public int MarketActionId { get; set; }
        public string TarketModelCode { get; set; }// 主推车型
        public MarketActionBefore21 MarketActionBefore21 { get; set; }
        public List<MarketActionBefore21ActivityProcess> ActivityProcess { get; set; }
    }
}