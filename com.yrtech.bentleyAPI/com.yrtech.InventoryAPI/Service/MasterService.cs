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
        public List<ShopDto> ShopSearch(string shopId,string shopCode,string shopName,string shopNameEn)
        {
            if (shopId == null) shopId = "";
            if (shopCode == null) shopCode = "";
            if (shopName == null) shopName = "";
            if (shopNameEn == null) shopNameEn = "";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ShopId", shopId),
                                                    new SqlParameter("@ShopCode", shopCode),
                                                    new SqlParameter("@ShopName", shopName),
                                                    new SqlParameter("@ShopNameEn", shopNameEn)};
            Type t = typeof(ShopDto);
            string sql = "";
             sql = @"SELECT A.*,B.AreaCode,B.AreaName,B.AreaNameEn 
                    FROM Shop A LEFT JOIN Area B ON A.AreaId = B.AreaId
                    WHERE 1=1";
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
            return db.Database.SqlQuery(t, sql, para).Cast<ShopDto>().ToList();
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
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = shop.ModifyUserId;
            }
            db.SaveChanges();
        }
        #endregion
        #region HiddenCode
        public List<HiddenCode> HiddenCodeSearch(string hiddenCodeGroup, string hiddenCode)
        {
            if (hiddenCodeGroup == null) hiddenCodeGroup = "";
            if (hiddenCode == null) hiddenCode = "";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@HiddenCodeGroup", hiddenCodeGroup),
                                                    new SqlParameter("@HiddenCode", hiddenCode)};
            Type t = typeof(HiddenCode);
            string sql = "";
            sql = @"SELECT * FROM [HiddenCode] WHERE 1=1";
            if (!string.IsNullOrEmpty(hiddenCodeGroup))
            {
                sql += " AND HiddenCodeGroup = @HiddenCodeGroup";
            }
            if (!string.IsNullOrEmpty(hiddenCode))
            {
                sql += " AND HiddenCodeId = @HiddenCode";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<HiddenCode>().ToList();
        }
        #endregion
        #region EventType
        public List<EventTypeDto> EventTypeSearch(string eventTypeId, string eventTypeName,string eventTypeNameEn)
        {
            if (eventTypeId == null) eventTypeId = "";
            if (eventTypeName == null) eventTypeName = "";
            if (eventTypeNameEn == null) eventTypeNameEn = "";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@EventTypeId", eventTypeId),
                                                    new SqlParameter("@EventTypeName", eventTypeName),
                                                    new SqlParameter("@EventTypeNameEn", eventTypeNameEn)};
            Type t = typeof(EventTypeDto);
            string sql = "";
            sql = @"SELECT A.*,B.HiddenCodeName AS EventModeName,B.HiddenCodeNameEn AS EventModeNameEn,C.AreaCode,C.AreaName,C.AreaNameEn 
                    FROM EventType A INNER JOIN HiddenCode B ON A.EventMode = B.HiddenCodeId AND B.HiddenCodeGroup = 'EventMode' 
                                     INNER JOIN Area C ON A.AreaId = C.AreaId
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(eventTypeId))
            {
                sql += " AND EventTypeId = @EventTypeId";
            }
            if (!string.IsNullOrEmpty(eventTypeName))
            {
                sql += " AND EventTypeName = @EventTypeName";
            }
            if (!string.IsNullOrEmpty(eventTypeNameEn))
            {
                sql += " AND EventTypeNameEn = @EventTypeNameEn";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<EventTypeDto>().ToList();
        }
        public void EventTypeSave(EventType eventType)
        {
            EventType findOne = db.EventType.Where(x => (x.EventTypeId == eventType.EventTypeId)).FirstOrDefault();
            if (findOne == null)
            {
                eventType.InDateTime = DateTime.Now;
                eventType.ModifyDateTime = DateTime.Now;
                db.EventType.Add(eventType);
            }
            else
            {
                findOne.ApprovalMaxAmt = eventType.ApprovalMaxAmt;
                findOne.AreaId = eventType.AreaId;
                findOne.EventMode = eventType.EventMode;
                findOne.EventTypeName = eventType.EventTypeName;
                findOne.EventTypeNameEn = eventType.EventTypeNameEn;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = eventType.ModifyUserId;
                findOne.ShowStatus = eventType.ShowStatus;
                
            }
            db.SaveChanges();
        }
        #endregion

    }
}