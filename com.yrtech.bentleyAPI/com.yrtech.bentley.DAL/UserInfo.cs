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
    
    public partial class UserInfo
    {
        public int UserId { get; set; }
        public string AccountId { get; set; }
        public string Password { get; set; }
        public string AccountName { get; set; }
        public string AccountNameEn { get; set; }
        public string TelNO { get; set; }
        public string Email { get; set; }
        public string RoleTypeCode { get; set; }
        public Nullable<int> ShopId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> InUserId { get; set; }
        public Nullable<System.DateTime> InDateTime { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
    }
}
