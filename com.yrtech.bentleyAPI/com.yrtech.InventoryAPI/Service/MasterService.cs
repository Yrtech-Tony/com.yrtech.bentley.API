using com.yrtech.InventoryAPI.DTO;
using System;
using com.yrtech.bentley.DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace com.yrtech.InventoryAPI.Service
{
    public class MasterService
    {
        Bentley db = new Bentley();
        #region Shop
        public List<Shop> ShopSearch(string shopId,string shopCode,string shopName,string shopNameEn)
        {
            if (shopId == null) shopId = "";
            if (shopCode == null) shopCode = "";
            if (shopName == null) shopName = "";
            if (shopNameEn == null) shopNameEn = "";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ShopId", shopId),
                                                    new SqlParameter("@ShopCode", shopCode),
                                                    new SqlParameter("@ShopName", shopName),
                                                    new SqlParameter("@ShopNameEn", shopNameEn),};
            Type t = typeof(Shop);
            string sql = "";
             sql = @"SELECT * FROM Shop WHERE 1=1";
            if (!string.IsNullOrEmpty(shopId))
            {
                sql += " AND ShopId = @ShopId";
            }
            if (!string.IsNullOrEmpty(shopCode))
            {
                sql += " AND ShopCode = @ShopCode";
            }
            if (!string.IsNullOrEmpty(shopName))
            {
                sql += " AND ShopName = @ShopName";
            }
            if (!string.IsNullOrEmpty(shopNameEn))
            {
                sql += " AND ShopNameEn = @ShopNameEn";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<Shop>().ToList();
        }
        public void ShopSave(Shop shop)
        {
            Shop findOne = db.Shop.Where(x => (x.ShopId ==shop.ShopId)).FirstOrDefault();
            if (findOne == null)
            {
                shop.InDateTime = DateTime.Now;
                shop.ModifyDateTime = DateTime.Now;
                db.Shop.Add(shop);
            }
            else
            {
                findOne.ShopCode = shop.ShopCode;
                findOne.ShopName = shop.ShopName;
                findOne.ShopNameEn = shop.ShopNameEn;
                findOne.AreaId = shop.AreaId;
                findOne.Budget = shop.Budget;
                findOne.Balance = shop.Balance;
                findOne.City = shop.City;
            }
            db.SaveChanges();
        }
        #endregion

    }
}