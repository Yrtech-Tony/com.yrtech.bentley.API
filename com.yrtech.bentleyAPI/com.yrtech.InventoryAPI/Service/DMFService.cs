using com.yrtech.InventoryAPI.DTO;
using System;
using com.yrtech.bentley.DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace com.yrtech.InventoryAPI.Service
{
    public class DMFService
    {
        Bentley db = new Bentley();
        #region DMFItem
        public List<DMFItem> DMFItemSearch(string DMFItemId,string DMFItemName,string DMFItemNameEn)
        {
            if (DMFItemId == null) DMFItemId = "";
            if (DMFItemName == null) DMFItemName = "";
            if (DMFItemNameEn == null) DMFItemNameEn = "";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ShopId", DMFItemId),
                                                    new SqlParameter("@ShopCode", DMFItemName),
                                                    new SqlParameter("@ShopName", DMFItemNameEn)};
            Type t = typeof(DMFItem);
            string sql = "";
             sql = @"SELECT A.*,B.AreaCode,B.AreaName,B.AreaNameEn 
                    FROM Shop A LEFT JOIN Area B ON A.AreaId = B.AreaId
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(DMFItemId))
            {
                sql += " AND DMFItemId = @DMFItemId";
            }
            if (!string.IsNullOrEmpty(DMFItemName))
            {
                sql += " AND DMFItemName = @DMFItemName";
            }
            if (!string.IsNullOrEmpty(DMFItemNameEn))
            {
                sql += " AND DMFItemNameEn = @DMFItemNameEn";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<DMFItem>().ToList();
        }
        public void DMFItemSave(DMFItem dmfItem)
        {
            DMFItem findOne = db.DMFItem.Where(x => (x.DMFItemId == dmfItem.DMFItemId)).FirstOrDefault();
            if (findOne == null)
            {
                dmfItem.InDateTime = DateTime.Now;
                dmfItem.ModifyDateTime = DateTime.Now;
                db.DMFItem.Add(dmfItem);
            }
            else
            {
                findOne.DMFItemName = dmfItem.DMFItemName;
                findOne.DMFItemNameEn = dmfItem.DMFItemNameEn;
                findOne.DMFItemRemark = dmfItem.DMFItemRemark;
                findOne.ExpenseAccountChk = dmfItem.ExpenseAccountChk;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = dmfItem.ModifyUserId;
                findOne.PublishChk = dmfItem.PublishChk;
            }
            db.SaveChanges();
        }
        public void DMFItemDelete(string dmfItemId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DMFItemId", dmfItemId) };
            string sql = @"DELETE DMFItem WHERE DMFItemId = @DMFItemId
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion


    }
}