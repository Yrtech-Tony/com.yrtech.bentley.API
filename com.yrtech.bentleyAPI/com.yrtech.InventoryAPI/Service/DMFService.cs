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
        public List<DMFItem> DMFItemSearch(string dmfItemId,string dmfItemName,string dmfItemNameEn,bool? expenseAccountChk,bool? publishChk)
        {
            if (dmfItemId == null) dmfItemId = "";
            if (dmfItemName == null) dmfItemName = "";
            if (dmfItemNameEn == null) dmfItemNameEn = "";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DMFItemId", dmfItemId),
                                                    new SqlParameter("@DMFItemName", dmfItemName),
                                                    new SqlParameter("@DMFItemNameEn", dmfItemNameEn),
                                                     new SqlParameter("@ExpenseAccountChk", expenseAccountChk),
                                                    new SqlParameter("@PublishChk", publishChk)};
            Type t = typeof(DMFItem);
            string sql = "";
             sql = @"SELECT A.* 
                    FROM DMFItem A 
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(dmfItemId))
            {
                sql += " AND DMFItemId = @DMFItemId";
            }
            if (!string.IsNullOrEmpty(dmfItemName))
            {
                sql += " AND DMFItemName = @DMFItemName";
            }
            if (!string.IsNullOrEmpty(dmfItemNameEn))
            {
                sql += " AND DMFItemNameEn = @DMFItemNameEn";
            }
            if (expenseAccountChk != null)
            {
                sql += " AND ExpenseAccountChk = @ExpenseAccountChk"; 
            }
            if (publishChk != null)
            {
                sql += " AND PublishChk = @PublishChk";
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
        #region ExpenseAccount
        public List<ExpenseAccountDto> ExpenseAccountSearch(string expenseAccountId, string shopId, string dmfItemId,string marketActionId)
        {
            if (expenseAccountId == null) expenseAccountId = "";
            if (shopId == null) shopId = "";
            if (dmfItemId == null) dmfItemId = "";
            if (marketActionId == null) marketActionId = "";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ExpenseAccountId", expenseAccountId),
                                                    new SqlParameter("@ShopId", shopId),
                                                    new SqlParameter("@DMFItemId", dmfItemId),
                                                    new SqlParameter("@MarketActionId", marketActionId)};
            Type t = typeof(ExpenseAccountDto);
            string sql = "";
            sql = @"SELECT A.ExpenseAccountId,A.DMFItemId,A.ShopId,A.MarketActionId,A.ExpenseAmt,A.ApprovalReason
                       ,A.ReplyReason,
                       CASE WHEN A.ApplyStatus IS NOT NULL AND A.ApplyStatus<>'' 
                            THEN A.ApplyStatus
                            WHEN NOT EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId)
                            THEN '未提交'
                            --WHEN NOT EXISTS()
                    B.ShopName,B.ShopNameEn,C.DMFItemName,C.DMFItemNameEn,D.ActionName
                    FROM ExpenseAccoutId A INNER JOIN Shop B ON A.ShopId = B.ShopId
                                            INNER JOIN DMFItem C ON A.DMFItemId = C.DMFItemId
                                            INNER JOIN MarketAction D ON A.MarketActionId = D.MarketActionId
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(expenseAccountId))
            {
                sql += " AND ExpenseAccountId = @ExpenseAccountId";
            }
            if (!string.IsNullOrEmpty(shopId))
            {
                sql += " AND ShopId = @ShopId";
            }
            if (!string.IsNullOrEmpty(dmfItemId))
            {
                sql += " AND DMFItemId = @DMFItemId";
            }
            if (!string.IsNullOrEmpty(marketActionId))
            {
                sql += " AND MarketActionId = @MarketActionId";
            }

            return db.Database.SqlQuery(t, sql, para).Cast<ExpenseAccountDto>().ToList();
        }
        public void ExpenseAccountSave(ExpenseAccount expenseAccount)
        {
            ExpenseAccount findOne = db.ExpenseAccount.Where(x => (x.ExpenseAccountId == expenseAccount.ExpenseAccountId)).FirstOrDefault();
            if (findOne == null)
            {
                expenseAccount.InDateTime = DateTime.Now;
                expenseAccount.ModifyDateTime = DateTime.Now;
                db.ExpenseAccount.Add(expenseAccount);
            }
            else
            {
                findOne.ApplyStatus = expenseAccount.ApplyStatus;
                findOne.ApprovalReason = expenseAccount.ApprovalReason;
                findOne.DMFItemId = expenseAccount.DMFItemId;
                findOne.ExpenseAmt = expenseAccount.ExpenseAmt;
                findOne.MarketActionId = expenseAccount.MarketActionId;
                findOne.ReplyReason = expenseAccount.ReplyReason;
                findOne.ReplyStatus = expenseAccount.ReplyStatus;
                findOne.ShopId = expenseAccount.ShopId;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = expenseAccount.ModifyUserId;
                
            }
            db.SaveChanges();
        }
        public void ExpenseAccountDelete(string expenseAccountId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ExpenseAccountId", expenseAccountId) };
            string sql = @"DELETE ExpenseAccount WHERE ExpenseAccountId = @ExpenseAccountId
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        public List<ExpenseAccountFile> ExpenseAccountFileSearch(string expenseAccountId,string seqNO,string fileType)
        {
            if (expenseAccountId == null) expenseAccountId = "";
            if (seqNO == null) seqNO = "";
            if (fileType == null) fileType = "";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ExpenseAccountId", expenseAccountId),
                                                    new SqlParameter("@SeqNO", seqNO),
                                                    new SqlParameter("@FileType", fileType)};
            Type t = typeof(ExpenseAccountFile);
            string sql = "";
            sql = @"SELECT A.*,B.ShopName,B.ShopNameEn,C.DMFItemName,C.DMFItemNameEn,D.ActionName
                    FROM ExpenseAccoutId A INNER JOIN Shop B ON A.ShopId = B.ShopId
                                            INNER JOIN DMFItem C ON A.DMFItemId = C.DMFItemId
                                            INNER JOIN MarketAction D ON A.MarketActionId = D.MarketActionId
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(expenseAccountId))
            {
                sql += " AND ExpenseAccountId = @ExpenseAccountId";
            }
            if (!string.IsNullOrEmpty(seqNO))
            {
                sql += " AND SeqNO = @SeqNO";
            }
            if (!string.IsNullOrEmpty(fileType))
            {
                sql += " AND FileType = @FileType";
            }
            
            return db.Database.SqlQuery(t, sql, para).Cast<ExpenseAccountFile>().ToList();
        }
        public void ExpenseAccountFileSave(ExpenseAccountFile expenseAccountFile)
        {
            if (expenseAccountFile.SeqNO == 0)
            {
                ExpenseAccountFile findOneMax = db.ExpenseAccountFile.Where(x => (x.ExpenseAccountId == expenseAccountFile.ExpenseAccountId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
                if (findOneMax == null)
                {
                    expenseAccountFile.SeqNO = 1;
                }
                else
                {
                    expenseAccountFile.SeqNO = findOneMax.SeqNO + 1;
                }
                expenseAccountFile.InDateTime = DateTime.Now;
                expenseAccountFile.ModifyDateTime = DateTime.Now;
                db.ExpenseAccountFile.Add(expenseAccountFile);

            }
            else
            {

            }
            db.SaveChanges();
        }
        public void ExpenseAccountFileDelete(string expenseAccountId, string seqNO)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ExpenseAccountId", expenseAccountId),
                                                        new SqlParameter("@SeqNO", seqNO)};
            string sql = @"DELETE ExpenseAccountFile WHERE ExpenseAccountId = @ExpenseAccountId AND SeqNO = @SeqNO
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion


    }
}