//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class MarketActionAfter2LeadsReport
    {
        public int MarketActionId { get; set; }
        public int SeqNO { get; set; }
        public string CustomerName { get; set; }
        public string TelNO { get; set; }
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
