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
    
    public partial class MonthSale
    {
        public int MonthSaleId { get; set; }
        public string YearMonth { get; set; }
        public Nullable<int> ShopId { get; set; }
        public Nullable<int> ActualSaleCount { get; set; }
        public Nullable<decimal> ActualSaleAmt { get; set; }
        public Nullable<int> InUserId { get; set; }
        public Nullable<System.DateTime> InDateTime { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
    }
}
