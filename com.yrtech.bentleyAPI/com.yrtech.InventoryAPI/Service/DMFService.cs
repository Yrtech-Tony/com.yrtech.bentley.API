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
                para = para.Concat(new SqlParameter[] { new SqlParameter("@ExpenseAccountChk", expenseAccountChk) }).ToArray();
                sql += " AND ExpenseAccountChk = @ExpenseAccountChk";
            }
            if (publishChk.HasValue)
            {
                para = para.Concat(new SqlParameter[] { new SqlParameter("@PublishChk", publishChk) }).ToArray();
                sql += " AND PublishChk = @PublishChk";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<DMFItem>().ToList();
        }
        public DMFItem DMFItemSave(DMFItem dmfItem)
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

                dmfItem = findOne;
            }
            db.SaveChanges();
            return dmfItem;
        }
        public void DMFItemDelete(string dmfItemId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DMFItemId", dmfItemId) };
            string sql = @"DELETE DMFItem WHERE DMFItemId = @DMFItemId
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion
        #region DMFDetail
        public List<DMFDetailDto> DMFDetailSearch(string dmfDetailId, string shopId, string dmfItemId,string dmfItemName)
        {
            if (dmfDetailId == null) dmfDetailId = "";
            if (shopId == null) shopId = "";
            if (dmfItemId == null) dmfItemId = "";
            if (dmfItemName == null) dmfItemName = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DMFDetailId", dmfDetailId),
                                                    new SqlParameter("@ShopId", shopId),
                                                    new SqlParameter("@DMFItemId", dmfItemId)};

            Type t = typeof(DMFDetailDto);

            string sql = "";
            sql = @"  SELECT * FROM (
                        SELECT ISNULL(D.DMFDetailId,0) AS DMFDetailId,A.ShopId,A.DMFItemId,ISNULL(D.Budget,0) AS Budget
							    ,A.AcutalAmt,ISNULL(D.Remark,'') AS Remark
							    ,B.ShopCode,B.ShopName,B.ShopNameEn,C.DMFItemName,C.DMFItemNameEn 
					    FROM (
							    SELECT ShopId,DMFItemId,ISNULL(SUM(ExpenseAmt),0) AS AcutalAmt
							    FROM ExpenseAccount  
							    WHERE ApplyStatus='通过'
							    GROUP BY ShopId,DMFItemId) A INNER JOIN Shop B ON A.ShopId = B.ShopId
														    INNER JOIN DMFItem C ON A.DMFItemId = C.DMFItemId
														LEFT JOIN DMFDetail D ON A.ShopId = D.ShopId AND A.DMFItemId = D.DMFItemId	
                    UNION ALL
					 
					SELECT A.[DMFDetailId],A.[ShopId],A.[DMFItemId],A.[Budget],ISNULL(A.[AcutalAmt],0) AS AcutalAmt
                           ,A.[Remark],B.ShopCode,B.ShopName,B.ShopNameEn,C.DMFItemName,C.DMFItemNameEn 
                    FROM DMFDetail A INNER JOIN Shop B ON A.ShopId = B.ShopId
                                    INNER JOIN DMFItem C ON A.DMFItemId = C.DMFItemId
                    WHERE NOT EXISTS(SELECT 1 FROM ExpenseAccount WHERE ShopId = A.ShopId AND DMFItemId = A.DMFItemId)
                    ) X WHERE 1=1";
            if (!string.IsNullOrEmpty(dmfDetailId))
            {
                sql += " AND DMFDetailId = @DMFDetailId";
            }
            if (!string.IsNullOrEmpty(shopId))
            {
                sql += " AND X.ShopId = @ShopId";
            }
            if (!string.IsNullOrEmpty(dmfItemId))
            {
                sql += " AND X.DMFItemId = @DMFItemId";
            }
            if (!string.IsNullOrEmpty(dmfItemName))
            {
                sql += " AND X.dmfItemName LIKE '%'+ @DMFItemId+ '%'";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<DMFDetailDto>().ToList();
        }
        public DMFDetail DMFDetailSave(DMFDetail dmfDetail)
        {
            DMFDetail findOne = db.DMFDetail.Where(x => (x.DMFDetailId == dmfDetail.DMFDetailId)).FirstOrDefault();
            if (findOne == null)
            {
                dmfDetail.InDateTime = DateTime.Now;
                dmfDetail.ModifyDateTime = DateTime.Now;
                db.DMFDetail.Add(dmfDetail);
            }
            else
            {
                findOne.AcutalAmt = dmfDetail.AcutalAmt;
                findOne.Budget = dmfDetail.Budget;
                findOne.DMFItemId = dmfDetail.DMFItemId;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = dmfDetail.ModifyUserId;
                findOne.Remark = dmfDetail.Remark;
                findOne.ShopId = dmfDetail.ShopId;

                dmfDetail = findOne;
            }
            db.SaveChanges();
            return dmfDetail;
        }
        public void DMFDetailDelete(string dmfDetailId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DMFDetailId", dmfDetailId) };
            string sql = @"DELETE DMFDetail WHERE DMFDetailId = @DMFDetailId
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion
        #region DMF
        public List<DMFDto> DMFSearch(string shopId)
        {
            if (shopId == null) shopId = "";
             SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ShopId", shopId) };

            Type t = typeof(DMFDto);

            string sql = "";
            sql = @"                 
                SELECT DISTINCT A.ShopId,A.ShopCode,A.ShopName,A.ShopNameEn
			                ,ISNULL(B.ActualMonthSaleCount,0) AS ActualMonthSaleCount
			                ,ISNULL(B.ActualMonthSaleAmt,0) AS ActualMonthSaleAmt
                FROM Shop A LEFT JOIN
                            (SELECT ShopId
		                            ,ISNULL(SUM(ActualSaleCount),0) AS ActualMonthSaleCount
		                            ,ISNULL(SUM(ActualSaleAmt),0) AS ActualMonthSaleAmt
                            FROM dbo.MonthSale GROUP BY ShopId) B ON A.ShopId = B.ShopId
               WHERE 1=1";
            if (!string.IsNullOrEmpty(shopId))
            {
                sql += " AND A.ShopId = @ShopId";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<DMFDto>().ToList();
        }
        public List<DMFDto> DMFQuarterSearch(string shopId)
        {
            if (shopId == null) shopId = "";
             SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ShopId", shopId) };

            Type t = typeof(DMFDto);

            string sql = "";
            sql = @"                 
                SELECT A.ShopId,A.ShopName,A.ShopNameEn,Y.Quarters
	                ,ISNULL(SUM(Y.ActualSaleCount),0) AS ActualMonthSaleCount
	                ,ISNULL(SUM(Y.ActualSaleAmt),0) AS ActualMonthSaleAmt
                FROM Shop A INNER JOIN 
			                (SELECT ShopId,CASE WHEN MonthStr IN ('01','1','02','2','3','03') THEN 'Q1'
								                WHEN MonthStr IN ('04','4','05','5','06','6') THEN 'Q2'
								                WHEN MonthStr IN ('07','7','08','8','09','9') THEN 'Q3'
								                ELSE 'Q4' END AS Quarters
				                  ,ActualSaleAmt,ActualSaleCount
			                FROM 
				                (SELECT ShopId
					                ,CASE WHEN LEN(YearMonth)=7 THEN LEFT(YearMonth,2)
						                   WHEN LEN(YearMonth)=6 THEN LEFT(YearMonth,1)
						                   ELSE '' END AS MonthStr
					                ,ActualSaleAmt,ActualSaleCount
				                FROM MonthSale) X) Y ON A.ShopId = Y.ShopId
                WHERE 1=1 
               ";
            if (!string.IsNullOrEmpty(shopId))
            {
                sql += " AND A.ShopId = @ShopId";
            }
            sql += " GROUP BY A.ShopId,A.ShopName,A.ShopNameEn,Y.Quarters";
            return db.Database.SqlQuery(t, sql, para).Cast<DMFDto>().ToList();
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
                            WHEN  EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileTypeCode = 1)
                                 AND EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileTypeCode = 2)
                            THEN '待审批'
                            ELSE '未提交'  
                        END AS ApplyStatus
                     ,CASE WHEN A.ReplyStatus IS NOT NULL AND A.ReplyStatus<>'' 
                            THEN A.ReplyStatus
                            WHEN  EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileTypeCode = 3)
                                 AND EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileTypeCode = 4)
                                AND EXISTS(SELECT 1 FROM ExpenseAccountFile WHERE ExpenseAccountId = A.ExpenseAccountId AND FileTypeCode = 5)
                            THEN '待审批'
                            ELSE '未提交'  
                        END AS ReplyStatus,
                    B.ShopName,B.ShopNameEn,C.DMFItemName,C.DMFItemNameEn,D.ActionName
                    FROM ExpenseAccount A INNER JOIN Shop B ON A.ShopId = B.ShopId
                                            INNER JOIN DMFItem C ON A.DMFItemId = C.DMFItemId
                                            INNER JOIN MarketAction D ON A.MarketActionId = D.MarketActionId
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(expenseAccountId))
            {
                sql += " AND A.ExpenseAccountId = @ExpenseAccountId";
            }
            if (!string.IsNullOrEmpty(shopId))
            {
                sql += " AND A.ShopId = @ShopId";
            }
            if (!string.IsNullOrEmpty(dmfItemId))
            {
                sql += " AND A.DMFItemId = @DMFItemId";
            }
            if (!string.IsNullOrEmpty(marketActionId))
            {
                sql += " AND A.MarketActionId = @MarketActionId";
            }

            return db.Database.SqlQuery(t, sql, para).Cast<ExpenseAccountDto>().ToList();
        }
        public ExpenseAccount ExpenseAccountSave(ExpenseAccount expenseAccount)
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
                expenseAccount = findOne;
            }
            db.SaveChanges();

            return expenseAccount;
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
            sql = @"SELECT A.*  FROM dbo.ExpenseAccountFile A WHERE 1=1";
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
                sql += " AND FileTypeCode = @FileType";
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
                sql += " AND A.ShopId = @ShopId";
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