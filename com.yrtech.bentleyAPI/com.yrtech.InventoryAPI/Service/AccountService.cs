using com.yrtech.InventoryAPI.DTO;
using System;
using com.yrtech.bentley.DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace com.yrtech.InventoryAPI.Service
{
    public class AccountService
    {
       Bentley db = new Bentley();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public List<AccountDto> Login(string accountId, string password)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AccountId", accountId),
                                                       new SqlParameter("@Password",password)};
            Type t = typeof(AccountDto);
            string sql = @"SELECT TOP 1 UserId,AccountId,AccountName,RoleTypeCode FROM UserInfo
                            WHERE AccountId = @AccountId AND [Password] = @Password";
            return db.Database.SqlQuery(t, sql, para).Cast<AccountDto>().ToList();
        }
        public List<Shop> GetShopByRole(string accountId, string roleTypeCode)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AccountId", accountId),
                                                       new SqlParameter("@RoleTypeCode",roleTypeCode)};
            Type t = typeof(AccountDto);
            string sql = "";
            if (roleTypeCode.ToUpper() == "SYSADMIN" || roleTypeCode.ToUpper() == "MARKET")
            {
                sql += @"SELECT ShopId,ShopCode,ShopName,ShopNameEn 
                        FROM Shop A";
            }
            else if (roleTypeCode.ToUpper() == "AREA")
            {
                sql += @"SELECT A.ShopId,A.ShopCode,A.ShopName,A.ShopNameEn 
                        FROM Shop A INNER JOIN UserInfo B ON A.AreaId = B.AreaId
                        WHERE B.AccountId = @AccountId";
            }
            else if (roleTypeCode.ToUpper() == "SHOP")
            {
                sql += @"SELECT A.ShopId,A.ShopCode,A.ShopName,A.ShopNameEn 
                        FROM Shop A INNER JOIN UserInfo B ON A.ShopId = B.ShopId
                        WHERE B.AccountId = @AccountId";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<Shop>().ToList();
        }

        public List<Area> GetAreaByRole(string accountId, string roleTypeCode)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AccountId", accountId),
                                                       new SqlParameter("@RoleTypeCode",roleTypeCode)};
            Type t = typeof(AccountDto);
            string sql = "";
            if (roleTypeCode.ToUpper() == "SYSADMIN" || roleTypeCode.ToUpper() == "MARKET")
            {
                sql += @"SELECT AreaId,AreaCode,AreaName,AreaNameEn 
                        FROM Area A";
            }
            else if (roleTypeCode.ToUpper() == "AREA")
            {
                sql += @"SELECT A.AreaId,A.AreaCode,A.AreaName,A.AreaNameEn 
                        FROM Area A INNER JOIN UserInfo B ON A.AreaId = B.AreaId
                        WHERE B.AccountId = @AccountId";
            }
            else if (roleTypeCode.ToUpper() == "SHOP")
            {
                sql += @"SELECT C.AreaId,C.AreaCode,C.AreaName,C.AreaNameEn 
                        FROM Shop A INNER JOIN UserInfo B ON A.ShopId = B.ShopId
                                    INNER JOIN Area C ON A.AreaId = C.AreaId
                        WHERE B.AccountId = @AccountId";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<Area>().ToList();
        }

    }
}