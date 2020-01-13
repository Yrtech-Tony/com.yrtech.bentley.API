using com.yrtech.bentley.DAL;
using System;
using System.Collections.Generic;

namespace com.yrtech.InventoryAPI.DTO
{
    [Serializable]
    public class MarketActionExportDto
    {
        public List<MarketActionDto> MarketActionList { get; set; }
       public List<MarketActionBefore21> MaketActionBefore21List { get; set; }

       public List<MarketActionAfter7> MarketActionAfter7List { get; set; }
       public List<MarketActionLeadsCountDto> LeadsCount { get; set; }



    }
}