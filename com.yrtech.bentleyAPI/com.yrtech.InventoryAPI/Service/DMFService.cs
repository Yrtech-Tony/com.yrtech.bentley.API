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
        public List<DMFItem> DMFItemSearch(string dmfItemId, string dmfItemName, string dmfItemNameEn, bool? expenseAccountChk, bool? publishChk)
        {
            if (dmfItemId == null) dmfItemId = "";
            if (dmfItemName == null) dmfItemName = "";
            if (dmfItemNameEn == null) dmfItemNameEn = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DMFItemId", dmfItemId),
                                                    new SqlParameter("@DMFItemName", dmfItemName),
                                                    new SqlParameter("@DMFItemNameEn", dmfItemNameEn)};

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
            if (expenseAccountChk.HasValue)
            {
                para.Concat(new SqlParameter[] { new SqlParameter("@ExpenseAccountChk", expenseAccountChk) });
                sql += " AND ExpenseAccountChk = @ExpenseAccountChk";
            }
            if (publishChk.HasValue)
            {
                para.Concat(new SqlParameter[] { new SqlParameter("@PublishChk", publishChk) });
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
        public List<ExpenseAccountDto> ExpenseAccountSearch(string expenseAccountId, string shopId, string dmfItemId, string marketActionId)
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
                       ,A.ReplyReason
                       ,CASE WHEN A.ApplyStatus IS NOT NULL AND A.ApplyStatus<>'' 
                            THEN A.ApplyStatus
                            WHEN  EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileType = 1)
                                 AND EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileType = 2)
                            THEN '待审批'
                            ELSE '未提交'  
                        END AS ApplyStatus
                     ,CASE WHEN A.ReplyStatus IS NOT NULL AND A.ReplyStatus<>'' 
                            THEN A.ReplyStatus
                            WHEN  EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileType = 3)
                                 AND EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileType = 4)
                                AND EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileType = 5)
                            THEN '待审批'
                            ELSE '未提交'  
                        END AS ApplyStatus
                    A.B.ShopName,B.ShopNameEn,C.DMFItemName,C.DMFItemNameEn,D.ActionName
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
        public List<ExpenseAccountFile> ExpenseAccountFileSearch(string expenseAccountId, string seqNO, string fileType)
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
        #region MonthSale
        public List<MonthSaleDto> MonthSaleSearch(string monthSaleId, string shopId)
        {
            if (monthSaleId == null) monthSaleId = "";
            if (shopId == null) shopId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MonthSaleId", monthSaleId),
                                                    new SqlParameter("@ShopId", shopId)};

            Type t = typeof(MonthSaleDto);

            string sql = "";
            sql = @"SELECT A.*,B.ShopCode,B.ShopName,B.ShopNameEn
                    FROM MonthSale A INNER JOIN Shop B ON A.ShopId = B.ShopId
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(monthSaleId))
            {
                sql += " AND MonthSaleId = @MonthSaleId";
            }
            if (!string.IsNullOrEmpty(shopId))
            {
                sql += " AND ShopId = @ShopId";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<MonthSaleDto>().ToList();
        }
        public void MonthSaleSave(MonthSale monthSale)
        {
            MonthSale findOne = db.MonthSale.Where(x => (x.MonthSaleId == monthSale.MonthSaleId)).FirstOrDefault();
            if (findOne == null)
            {
                monthSale.InDateTime = DateTime.Now;
                monthSale.ModifyDateTime = DateTime.Now;
                db.MonthSale.Add(monthSale);
            }
            else
            {
                findOne.ActualSaleAmt = monthSale.ActualSaleAmt;
                findOne.ActualSaleCount = monthSale.ActualSaleCount;
                findOne.ShopId = monthSale.ShopId;
                findOne.YearMonth = monthSale.YearMonth;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = monthSale.ModifyUserId;
            }
            db.SaveChanges();
        }
        public void MonthSaleDelete(string monthSaleId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MonthSaleId", monthSaleId) };
            string sql = @"DELETE MonthSale WHERE MonthSaleId = @MonthSaleId
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion


    }
}