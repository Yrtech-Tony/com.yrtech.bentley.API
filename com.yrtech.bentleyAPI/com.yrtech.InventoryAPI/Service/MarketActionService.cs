using com.yrtech.InventoryAPI.DTO;
using System;
using com.yrtech.bentley.DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace com.yrtech.InventoryAPI.Service
{
    public class MarketActionService
    {
        Bentley db = new Bentley();
        #region Common
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<MarketActionDto> MarketActionSearch(string actionName, string year, string month, string marketActionStatusCode, string shopId, string eventTypeId, bool? expenseAccountChk)
        {
            if (actionName == null) actionName = "";
            if (year == null) year = "";
            if (month == null) month = "";
            if (marketActionStatusCode == null) marketActionStatusCode = "";
            if (shopId == null) shopId = "";
            if (eventTypeId == null) eventTypeId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ActionName", actionName),
                                                        new SqlParameter("@Year", year),
                                                        new SqlParameter("@Month", month),
                                                        new SqlParameter("@MarketActionStatusCode", marketActionStatusCode),
                                                        new SqlParameter("@ShopId", shopId),
                                                        new SqlParameter("@EventTypeId", eventTypeId)};
            Type t = typeof(MarketActionDto);
            string sql = "";
            sql += @"SELECT A.MarketActionId,A.ShopId,B.ShopCode,B.ShopName,B.ShopNameEn,A.ActionCode,A.ActionName
		                    ,A.EventTypeId,C.EventTypeName,C.EventTypeNameEn,A.StartDate,A.EndDate,A.ActionPlace
                            ,A.ExpenseAccount,A.InUserId,A.InDateTime,A.ModifyUserId,A.ModifyDateTime
		                    ,A.MarketActionStatusCode,D.HiddenCodeName AS MarketActionStatusName,D.HiddenCodeNameEn AS MarketActionStatusNameEn
		                    ,A.MarketActionTargetModelCode,E.HiddenCodeName AS MarketActionTargetModelName,E.HiddenCodeNameEn AS MarketActionTargetModelNameEn
		                    ,CASE WHEN EXISTS(SELECT 1 FROM MarketActionBefore21 WHERE MarketActionId = A.MarketActionId) 
				                       OR EXISTS(SELECT 1 FROM MarketActionBefore21ActivityProcess WHERE MarketActionId = A.MarketActionId)
			                      THEN 'Commited'
			                      WHEN NOT EXISTS(SELECT 1 FROM MarketActionBefore21 WHERE MarketActionId = A.MarketActionId) 
				                       AND NOT EXISTS(SELECT 1 FROM MarketActionBefore21ActivityProcess WHERE MarketActionId = A.MarketActionId)
				                       AND DATEDIFF(DAY,GETDATE(),A.StartDate)<21
			                      THEN 'UnCommitTime'
			                      ELSE 'UnCommit'
	                        END AS 	Before3Weeks
	                        ,CASE WHEN EXISTS(SELECT 1 FROM MarketActionBefore3BugetDetail WHERE MarketActionId = A.MarketActionId) 
				                       OR EXISTS(SELECT 1 FROM MarketActionBefore3DisplayModel WHERE MarketActionId = A.MarketActionId)
				                       OR EXISTS(SELECT 1 FROM MarketActionBefore3TestDriver WHERE MarketActionId = A.MarketActionId)
			                      THEN 'Commited'
			                      WHEN NOT EXISTS(SELECT 1 FROM MarketActionBefore3BugetDetail WHERE MarketActionId = A.MarketActionId) 
				                       AND NOT EXISTS(SELECT 1 FROM MarketActionBefore3DisplayModel WHERE MarketActionId = A.MarketActionId)
				                       AND NOT EXISTS(SELECT 1 FROM MarketActionBefore3TestDriver WHERE MarketActionId = A.MarketActionId)
				                       AND DATEDIFF(DAY,GETDATE(),A.StartDate)<3
			                      THEN 'UnCommitTime'
			                      ELSE 'UnCommit'
	                        END AS 	Before3Days	
	                        ,CASE WHEN EXISTS(SELECT 1 FROM MarketActionTheDayFile WHERE MarketActionId = A.MarketActionId) 
			                      THEN 'Commited'
			                      WHEN NOT EXISTS(SELECT 1 FROM MarketActionTheDayFile WHERE MarketActionId = A.MarketActionId) 
				                       AND DATEDIFF(DAY,GETDATE(),A.StartDate)<0
			                      THEN 'UnCommitTime'
			                      ELSE 'UnCommit'
	                        END AS 	TheDays
	                        ,CASE WHEN EXISTS(SELECT 1 FROM MarketActionAfter2LeadsReport WHERE MarketActionId = A.MarketActionId) 
			                      THEN 'Commited'
			                      WHEN NOT EXISTS(SELECT 1 FROM MarketActionAfter2LeadsReport WHERE MarketActionId = A.MarketActionId) 
				                       AND DATEDIFF(DAY,A.StartDate,GETDATE())>2
			                      THEN 'UnCommitTime'
			                      ELSE 'UnCommit'
	                        END AS 	After2Days
	                         ,CASE WHEN EXISTS(SELECT 1 FROM MarketActionAfter7 WHERE MarketActionId = A.MarketActionId) 
				                       OR EXISTS(SELECT 1 FROM MarketActionAfter7ActualExpense WHERE MarketActionId = A.MarketActionId)
				                       OR EXISTS(SELECT 1 FROM MarketActionAfter7ActualProcess WHERE MarketActionId = A.MarketActionId)
			                      THEN 'Commited'
			                      WHEN NOT EXISTS(SELECT 1 FROM MarketActionAfter7 WHERE MarketActionId = A.MarketActionId) 
				                       AND NOT EXISTS(SELECT 1 FROM MarketActionAfter7ActualExpense WHERE MarketActionId = A.MarketActionId)
				                       AND NOT EXISTS(SELECT 1 FROM MarketActionAfter7ActualProcess WHERE MarketActionId = A.MarketActionId)
				                       AND DATEDIFF(DAY,GETDATE(),A.StartDate)<7
			                      THEN 'UnCommitTime'
			                      ELSE 'UnCommit'
	                        END AS 	After7Days	
	                        ,CASE WHEN EXISTS(SELECT 1 FROM MarketActionAfter30LeadsReportUpdate WHERE MarketActionId = A.MarketActionId) 
			                      THEN 'Commited'
			                      WHEN NOT EXISTS(SELECT 1 FROM MarketActionAfter30LeadsReportUpdate WHERE MarketActionId = A.MarketActionId) 
				                       AND DATEDIFF(DAY,A.StartDate,GETDATE())>30
			                      THEN 'UnCommitTime'
			                      ELSE 'UnCommit'
	                        END AS 	After1Months
	                        ,CASE WHEN EXISTS(SELECT 1 FROM MarketActionAfter90File WHERE MarketActionId = A.MarketActionId) 
			                      THEN 'Commited'
			                      WHEN NOT EXISTS(SELECT 1 FROM MarketActionAfter90File WHERE MarketActionId = A.MarketActionId) 
				                       AND DATEDIFF(DAY,A.StartDate,GETDATE())>90
			                      THEN 'UnCommitTime'
			                      ELSE 'UnCommit'
	                        END AS 	After3Months
                    FROM MarketAction A LEFT JOIN Shop B ON A.ShopId = B.ShopId
					                    LEFT JOIN EventType C ON A.EventTypeId = C.EventTypeId
					                    LEFT JOIN HiddenCode D ON A.MarketActionStatusCode = D.HiddenCodeId AND D.HiddenCodeGroup = 'MarketActionStatus'
					                    LEFT JOIN HiddenCode E ON A.MarketActionTargetModelCode  = E.HiddenCodeId AND E.HiddenCodeGroup = 'TargetModels'
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(actionName))
            {
                sql += " AND A.ActionName LIKE '%'+@ActionName+'%'";
            }
            if (!string.IsNullOrEmpty(year))
            {
                sql += " AND Year(A.StartDate)= @Year";
            }
            if (!string.IsNullOrEmpty(month))
            {
                sql += " AND Month(A.StartDate)= @Month";
            }
            if (!string.IsNullOrEmpty(marketActionStatusCode))
            {
                sql += " AND A.MarketActionStatusCode =@MarketActionStatusCode";
            }
            if (!string.IsNullOrEmpty(shopId))
            {
                sql += " AND A.ShopId =@ShopId";
            }
            if (!string.IsNullOrEmpty(eventTypeId))
            {
                sql += " AND A.EventTypeId =@EventTypeId";
            }
            if (expenseAccountChk.HasValue)
            {
                para = para.Concat(new SqlParameter[] { new SqlParameter("@ExpenseAccountChk", expenseAccountChk) }).ToArray();
                sql += " AND A.ExpenseAccount = @ExpenseAccountChk";
            }
            sql += " ORDER BY A.StartDate DESC";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionDto>().ToList();
        }
        // 查询未取消的市场活动
        public List<MarketAction> MarketActionNotCancelSearch(string eventTypeId)
        {
            if (eventTypeId == null) eventTypeId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@EventTypeId", eventTypeId) };
            Type t = typeof(MarketAction);
            string sql = "";
            sql += @"SELECT *
                    FROM MarketAction A 
                    WHERE A.MarketActionStatusCode<>2 AND  A.ExpenseAccount=1";

            if (!string.IsNullOrEmpty(eventTypeId))
            {
                sql += " AND A.EventTypeId =@EventTypeId";
            }
            sql += " ORDER BY A.StartDate DESC";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketAction>().ToList();
        }
        public List<MarketAction> MarketActionSearchById(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketAction);
            string sql = "";
            sql += @"SELECT * FROM MarketAction
                    WHERE 1=1 AND MarketActionId = @MarketActionId";

            return db.Database.SqlQuery(t, sql, para).Cast<MarketAction>().ToList();
        }
        public void MarketActionSave(MarketAction marketAction)
        {
            MarketAction findOne = db.MarketAction.Where(x => (x.MarketActionId == marketAction.MarketActionId)).FirstOrDefault();
            if (findOne == null)
            {
                marketAction.InDateTime = DateTime.Now;
                marketAction.ModifyDateTime = DateTime.Now;
                db.MarketAction.Add(marketAction);
            }
            else
            {
                findOne.ActionCode = marketAction.ActionCode;
                findOne.ActionName = marketAction.ActionName;
                findOne.ActionPlace = marketAction.ActionPlace;
                findOne.EndDate = marketAction.EndDate;
                findOne.StartDate = marketAction.StartDate;
                findOne.EventTypeId = marketAction.EventTypeId;
                findOne.ExpenseAccount = marketAction.ExpenseAccount;
                findOne.MarketActionStatusCode = marketAction.MarketActionStatusCode;
                findOne.MarketActionTargetModelCode = marketAction.MarketActionTargetModelCode;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = marketAction.ModifyUserId;
                findOne.ShopId = marketAction.ShopId;
            }

            db.SaveChanges();
        }
        public void MarketActionDelete(string marketActionId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            string sql = @"
                        DELETE MarketActionBefore21 WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionBefore21ActivityProcess WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionBefore3BugetDetail WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionBefore3DisplayModel WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionBefore3TestDriver WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionTheDayFile WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionAfter2LeadsReport WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionAfter30LeadsReportUpdate WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionAfter7 WHERE MarketActionId = @MarketActionId  
                        DELETE MarketActionAfter7ActualExpense WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionAfter7ActualProcess WHERE MarketActionId = @MarketActionId 
                        DELETE MarketActionAfter90File WHERE MarketActionId = @MarketActionId 
                        DELETE MarketAction WHERE MarketActionId = @MarketActionId 
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }

        #region Before three weeks
        public List<MarketActionBefore21> MarketActionBefore21Search(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionBefore21);
            string sql = "";
            sql += @"SELECT A.* 
                    FROM [MarketActionBefore21] A 
                    WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionBefore21>().ToList();
        }
        public void MarketActionBefore21Save(MarketActionBefore21 marketActionBefore21)
        {
            MarketActionBefore21 findOne = db.MarketActionBefore21.Where(x => (x.MarketActionId == marketActionBefore21.MarketActionId)).FirstOrDefault();
            if (findOne == null)
            {
                marketActionBefore21.InDateTime = DateTime.Now;
                marketActionBefore21.ModifyDateTime = DateTime.Now;
                db.MarketActionBefore21.Add(marketActionBefore21);
            }
            else
            {
                findOne.ActivityBackground = marketActionBefore21.ActivityBackground;
                findOne.ActivityDesc = marketActionBefore21.ActivityDesc;
                findOne.ActivityObjective = marketActionBefore21.ActivityObjective;
                findOne.Budget = marketActionBefore21.Budget;
                findOne.KeyVisionApprovalCode = marketActionBefore21.KeyVisionApprovalCode;
                findOne.KeyVisionApprovalDesc = marketActionBefore21.KeyVisionApprovalDesc;
                findOne.KeyVisionDesc = marketActionBefore21.KeyVisionDesc;
                if (marketActionBefore21.KeyVisionPic!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.KeyVisionPic = marketActionBefore21.KeyVisionPic;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = marketActionBefore21.ModifyUserId;
                findOne.OtherDesc01 = marketActionBefore21.OtherDesc01;
                findOne.OtherDesc02 = marketActionBefore21.OtherDesc02;
                findOne.OtherDesc03 = marketActionBefore21.OtherDesc03;
                findOne.OtherDesc04 = marketActionBefore21.OtherDesc04;
                if (marketActionBefore21.OthersPic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OthersPic01 = marketActionBefore21.OthersPic01;
                if (marketActionBefore21.OthersPic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OthersPic02 = marketActionBefore21.OthersPic02;
                if (marketActionBefore21.OthersPic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OthersPic03 = marketActionBefore21.OthersPic03;
                if (marketActionBefore21.OthersPic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OthersPic04 = marketActionBefore21.OthersPic04;
                findOne.PlaceIntroDesc01 = marketActionBefore21.PlaceIntroDesc01;
                findOne.PlaceIntroDesc02 = marketActionBefore21.PlaceIntroDesc02;
                findOne.PlaceIntroDesc03 = marketActionBefore21.PlaceIntroDesc03;
                findOne.PlaceIntroDesc04 = marketActionBefore21.PlaceIntroDesc04;
                if (marketActionBefore21.PlaceIntroPic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.PlaceIntroPic01 = marketActionBefore21.PlaceIntroPic01;
                if (marketActionBefore21.PlaceIntroPic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.PlaceIntroPic02 = marketActionBefore21.PlaceIntroPic02;
                if (marketActionBefore21.PlaceIntroPic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.PlaceIntroPic03 = marketActionBefore21.PlaceIntroPic03;
                if (marketActionBefore21.PlaceIntroPic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.PlaceIntroPic04 = marketActionBefore21.PlaceIntroPic04;
                findOne.POSDesignDesc01 = marketActionBefore21.POSDesignDesc01;
                findOne.POSDesignDesc02 = marketActionBefore21.POSDesignDesc02;
                findOne.POSDesignDesc03 = marketActionBefore21.POSDesignDesc03;
                findOne.POSDesignDesc04 = marketActionBefore21.POSDesignDesc04;
                if (marketActionBefore21.POSDesignPic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.POSDesignPic01 = marketActionBefore21.POSDesignPic01;
                if (marketActionBefore21.POSDesignPic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.POSDesignPic02 = marketActionBefore21.POSDesignPic02;
                if (marketActionBefore21.POSDesignPic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.POSDesignPic03 = marketActionBefore21.POSDesignPic03;
                if (marketActionBefore21.POSDesignPic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.POSDesignPic04 = marketActionBefore21.POSDesignPic04;
                findOne.TargetLeadsOwnerCount = marketActionBefore21.TargetLeadsOwnerCount;
                findOne.TargetLeadsPCCount = marketActionBefore21.TargetLeadsPCCount;
                findOne.TargetOrdersOwnerCount = marketActionBefore21.TargetOrdersOwnerCount;
                findOne.TargetOrdersPCCount = marketActionBefore21.TargetOrdersPCCount;
                findOne.TargetParticipationOwnerCount = marketActionBefore21.TargetParticipationOwnerCount;
                findOne.TargetParticipationPCCount = marketActionBefore21.TargetParticipationPCCount;
                findOne.TargetTestDriveOwnerCount = marketActionBefore21.TargetTestDriveOwnerCount;
                findOne.TargetTestDrivePCCount = marketActionBefore21.TargetTestDrivePCCount;
                findOne.TestDriverRoadMapDesc01 = marketActionBefore21.TestDriverRoadMapDesc01;
                findOne.TestDriverRoadMapDesc02 = marketActionBefore21.TestDriverRoadMapDesc02;
                findOne.TestDriverRoadMapDesc03 = marketActionBefore21.TestDriverRoadMapDesc03;
                findOne.TestDriverRoadMapDesc04 = marketActionBefore21.TestDriverRoadMapDesc04;
                if (marketActionBefore21.TestDriverRoadMapPic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.TestDriverRoadMapPic01 = marketActionBefore21.TestDriverRoadMapPic01;
                if (marketActionBefore21.TestDriverRoadMapPic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.TestDriverRoadMapPic02 = marketActionBefore21.TestDriverRoadMapPic02;
                if (marketActionBefore21.TestDriverRoadMapPic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.TestDriverRoadMapPic03 = marketActionBefore21.TestDriverRoadMapPic03;
                if (marketActionBefore21.TestDriverRoadMapPic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.TestDriverRoadMapPic04 = marketActionBefore21.TestDriverRoadMapPic04;

            }

            db.SaveChanges();
        }
        public List<MarketActionBefore21ActivityProcess> MarketActionBefore21ActivityProcessSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionBefore21ActivityProcess);
            string sql = "";
            sql += @"SELECT *  FROM [MarketActionBefore21ActivityProcess] WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionBefore21ActivityProcess>().ToList();
        }
        public void MarketActionBefore21ActivityProcessSave(MarketActionBefore21ActivityProcess marketActionBefore21ActivityProcess)
        {
            //if (marketActionBefore21ActivityProcess.SeqNO == 0)
            //{
            MarketActionBefore21ActivityProcess findOneMax = db.MarketActionBefore21ActivityProcess.Where(x => (x.MarketActionId == marketActionBefore21ActivityProcess.MarketActionId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
            if (findOneMax == null)
            {
                marketActionBefore21ActivityProcess.SeqNO = 1;
            }
            else
            {
                marketActionBefore21ActivityProcess.SeqNO = findOneMax.SeqNO + 1;
            }
            marketActionBefore21ActivityProcess.InDateTime = DateTime.Now;
            marketActionBefore21ActivityProcess.ModifyDateTime = DateTime.Now;
            db.MarketActionBefore21ActivityProcess.Add(marketActionBefore21ActivityProcess);

            //}
            //else
            //{
            //MarketActionBefore21ActivityProcess findOne = db.MarketActionBefore21ActivityProcess.Where(x => (x.MarketActionId == marketActionBefore21ActivityProcess.MarketActionId && x.SeqNO == marketActionBefore21ActivityProcess.SeqNO)).FirstOrDefault();
            //findOne.ActivityDateTime = marketActionBefore21ActivityProcess.ActivityDateTime;
            //findOne.Contents = marketActionBefore21ActivityProcess.Contents;
            //findOne.Item = marketActionBefore21ActivityProcess.Item;
            //findOne.ModifyDateTime = DateTime.Now;
            //findOne.ModifyUserId = marketActionBefore21ActivityProcess.ModifyUserId;
            //findOne.Remark = marketActionBefore21ActivityProcess.Remark;
            //}
            db.SaveChanges();
        }
        public void MarketActionBefore21ActivityProcessDelete(string marketActionId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            string sql = @"DELETE MarketActionBefore21ActivityProcess WHERE MarketActionId = @MarketActionId
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion
        #region Before three days
        public List<MarketActionBefore3BugetDetailDto> MarketActionBefore3BugetDetailSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionBefore3BugetDetailDto);
            string sql = "";
            sql += @"SELECT A.*,ISNULL(A.UnitPrice,0)*ISNULL(A.Counts,0) AS Total 
                    FROM [MarketActionBefore3BugetDetail] A 
                    WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionBefore3BugetDetailDto>().ToList();
        }
        public decimal MarketActionBefore3BugetSumAmtSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(decimal);
            string sql = "";
            sql += @"SELECT ISNULL(SUM(ISNULL(A.UnitPrice,0)*ISNULL(A.Counts,0)),0) AS BugetDetailSumAmt 
                    FROM [MarketActionBefore3BugetDetail] A 
                    WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<decimal>().FirstOrDefault();
        }
        public List<MarketActionBefore3DisplayModel> MarketActionBefore3DisplayModelSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionBefore3DisplayModel);
            string sql = "";
            sql += @"SELECT A.* 
                    FROM [MarketActionBefore3DisplayModel] A 
                    WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionBefore3DisplayModel>().ToList();
        }
        public List<MarketActionBefore3TestDriver> MarketActionBefore3TestDriverSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionBefore3TestDriver);
            string sql = "";
            sql += @"SELECT A.* 
                    FROM [MarketActionBefore3TestDriver] A 
                    WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionBefore3TestDriver>().ToList();
        }
        public void MarketActionBefore3TestDriverSave(MarketActionBefore3TestDriver marketActionBefore3TestDriver)
        {
            //if (marketActionBefore3TestDriver.SeqNO == 0)
            //{
            MarketActionBefore3TestDriver findOneMax = db.MarketActionBefore3TestDriver.Where(x => (x.MarketActionId == marketActionBefore3TestDriver.MarketActionId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
            if (findOneMax == null)
            {
                marketActionBefore3TestDriver.SeqNO = 1;
            }
            else
            {
                marketActionBefore3TestDriver.SeqNO = findOneMax.SeqNO + 1;
            }
            marketActionBefore3TestDriver.InDateTime = DateTime.Now;
            marketActionBefore3TestDriver.ModifyDateTime = DateTime.Now;
            db.MarketActionBefore3TestDriver.Add(marketActionBefore3TestDriver);

            //}
            //else
            //{
            //    MarketActionBefore3TestDriver findOne = db.MarketActionBefore3TestDriver.Where(x => (x.MarketActionId == marketActionBefore3TestDriver.MarketActionId && x.SeqNO == marketActionBefore3TestDriver.SeqNO)).FirstOrDefault();
            //    findOne.DisplayModelColor = marketActionBefore3TestDriver.DisplayModelColor;
            //    findOne.ModifyDateTime = DateTime.Now;
            //    findOne.ModifyUserId = marketActionBefore3TestDriver.ModifyUserId;
            //    findOne.Provider = marketActionBefore3TestDriver.Provider;
            //}
            db.SaveChanges();
        }
        public void MarketActionBefore3DisplayModelSave(MarketActionBefore3DisplayModel marketActionBefore3DisplayModel)
        {
            //if (marketActionBefore3DisplayModel.SeqNO == 0)
            //{
            MarketActionBefore3DisplayModel findOneMax = db.MarketActionBefore3DisplayModel.Where(x => (x.MarketActionId == marketActionBefore3DisplayModel.MarketActionId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
            if (findOneMax == null)
            {
                marketActionBefore3DisplayModel.SeqNO = 1;
            }
            else
            {
                marketActionBefore3DisplayModel.SeqNO = findOneMax.SeqNO + 1;
            }
            marketActionBefore3DisplayModel.InDateTime = DateTime.Now;
            marketActionBefore3DisplayModel.ModifyDateTime = DateTime.Now;
            db.MarketActionBefore3DisplayModel.Add(marketActionBefore3DisplayModel);

            //}
            //else
            //{
            //    MarketActionBefore3DisplayModel findOne = db.MarketActionBefore3DisplayModel.Where(x => (x.MarketActionId == marketActionBefore3DisplayModel.MarketActionId && x.SeqNO == marketActionBefore3DisplayModel.SeqNO)).FirstOrDefault();
            //    findOne.DisplayModelColor = marketActionBefore3DisplayModel.DisplayModelColor;
            //    findOne.ModifyDateTime = DateTime.Now;
            //    findOne.ModifyUserId = marketActionBefore3DisplayModel.ModifyUserId;
            //    findOne.Provider = marketActionBefore3DisplayModel.Provider;
            //}
            db.SaveChanges();
        }
        public void MarketActionBefore3BugetDetailSave(MarketActionBefore3BugetDetail marketActionBefore3BugetDetail)
        {
            //if (marketActionBefore3BugetDetail.SeqNO == 0)
            //{
            MarketActionBefore3BugetDetail findOneMax = db.MarketActionBefore3BugetDetail.Where(x => (x.MarketActionId == marketActionBefore3BugetDetail.MarketActionId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
            if (findOneMax == null)
            {
                marketActionBefore3BugetDetail.SeqNO = 1;
            }
            else
            {
                marketActionBefore3BugetDetail.SeqNO = findOneMax.SeqNO + 1;
            }
            marketActionBefore3BugetDetail.InDateTime = DateTime.Now;
            marketActionBefore3BugetDetail.ModifyDateTime = DateTime.Now;
            db.MarketActionBefore3BugetDetail.Add(marketActionBefore3BugetDetail);

            //}
            //else
            //{
            //    MarketActionBefore3BugetDetail findOne = db.MarketActionBefore3BugetDetail.Where(x => (x.MarketActionId == marketActionBefore3BugetDetail.MarketActionId && x.SeqNO == marketActionBefore3BugetDetail.SeqNO)).FirstOrDefault();
            //    findOne.Counts = marketActionBefore3BugetDetail.Counts;
            //    findOne.ModifyDateTime = DateTime.Now;
            //    findOne.ModifyUserId = marketActionBefore3BugetDetail.ModifyUserId;
            //    findOne.Descs = marketActionBefore3BugetDetail.Descs;
            //    findOne.ItemName = marketActionBefore3BugetDetail.ItemName;
            //    findOne.UnitPrice = marketActionBefore3BugetDetail.UnitPrice;
            //}
            db.SaveChanges();
        }
        public void MarketActionBefore3TestDriverDelete(string marketActionId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            string sql = @"DELETE MarketActionBefore3TestDriver WHERE MarketActionId = @MarketActionId 
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        public void MarketActionBefore3BugetDetailDelete(string marketActionId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            string sql = @"DELETE MarketActionBefore3BugetDetail WHERE MarketActionId = @MarketActionId 
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        public void MarketActionBefore3DisplayModelDelete(string marketActionId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            string sql = @"DELETE MarketActionBefore3DisplayModel WHERE MarketActionId = @MarketActionId
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion
        #region TheDays
        public List<MarketActionTheDayFile> MarketActionTheDayFileSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionTheDayFile);
            string sql = "";
            sql += @"SELECT A.* 
                    FROM [MarketActionTheDayFile] A 
                    WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionTheDayFile>().ToList();
        }
        public void MarketActionTheDayFileSave(MarketActionTheDayFile marketActionTheDayFile)
        {
            if (marketActionTheDayFile.SeqNO == 0)
            {
                MarketActionTheDayFile findOneMax = db.MarketActionTheDayFile.Where(x => (x.MarketActionId == marketActionTheDayFile.MarketActionId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
                if (findOneMax == null)
                {
                    marketActionTheDayFile.SeqNO = 1;
                }
                else
                {
                    marketActionTheDayFile.SeqNO = findOneMax.SeqNO + 1;
                }
                marketActionTheDayFile.InDateTime = DateTime.Now;
                marketActionTheDayFile.ModifyDateTime = DateTime.Now;
                db.MarketActionTheDayFile.Add(marketActionTheDayFile);

            }
            else
            {

            }
            db.SaveChanges();
        }
        public void MarketActionTheDayFileDelete(string marketActionId, string seqNO)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId), new SqlParameter("@SeqNO", seqNO), };
            string sql = @"DELETE MarketActionTheDayFile WHERE MarketActionId = @MarketActionId AND SeqNO = @SeqNO
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion
        #region two days after
        public List<MarketActionAfter2LeadsReportDto> MarketActionAfter2LeadsReportSearch(string marketActionId, string year)
        {
            if (marketActionId == null) marketActionId = "";
            if (year == null) year = "";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionAfter2LeadsReportDto);
            string sql = "";
            sql += @"SELECT A.*,B.ActionName,B.ShopId,C.ShopName,C.ShopNameEn,D.HiddenCodeName AS InterestedModelName,D.HiddenCodeNameEn AS InterestedModelNameEn
                    ,E.HiddenCodeName AS DealModelName,E.HiddenCodeNameEn AS DealModelNameEn
                   , CASE WHEN OwnerCheck=1 THEN '是' ELSE '否' END AS OwnerCheckName
                    ,CASE WHEN TestDriverCheck=1 THEN '是' ELSE '否' END AS TestDriverCheckName
                    ,CASE WHEN LeadsCheck=1 THEN '是' ELSE '否' END AS LeadsCheckName
                    ,CASE WHEN DealCheck=1 THEN '是' ELSE '否' END AS DealCheckName
                    FROM [MarketActionAfter2LeadsReport] A  INNER JOIN MarketAction B ON A.MarketActionId = B.MarketActionId
                                                            INNER JOIN Shop C ON B.ShopId = C.ShopId
                                                            LEFT JOIN HiddenCode D ON A.InterestedModel = D.HiddenCodeId AND D.HiddenCodeGroup = 'TargetModels'
                                                            LEFT JOIN HiddenCode E ON A.DealModel = E.HiddenCodeId AND E.HiddenCodeGroup = 'TargetModels'
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(marketActionId))
            {
                sql += " AND A.MarketActionId = @MarketActionId";

            }
            if (!string.IsNullOrEmpty(year))
            {
                sql += " AND Year(B.StartDate) = @Year";

            }
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionAfter2LeadsReportDto>().ToList();
        }
        public MarketActionAfter2LeadsReport MarketActionAfter2LeadsReportSave(MarketActionAfter2LeadsReport marketActionAfter2LeadsReport)
        {
            if (marketActionAfter2LeadsReport.SeqNO == 0)
            {
                MarketActionAfter2LeadsReport findOneMax = db.MarketActionAfter2LeadsReport.Where(x => (x.MarketActionId == marketActionAfter2LeadsReport.MarketActionId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
                if (findOneMax == null)
                {
                    marketActionAfter2LeadsReport.SeqNO = 1;
                }
                else
                {
                    marketActionAfter2LeadsReport.SeqNO = findOneMax.SeqNO + 1;
                }
                marketActionAfter2LeadsReport.InDateTime = DateTime.Now;
                marketActionAfter2LeadsReport.ModifyDateTime = DateTime.Now;
                db.MarketActionAfter2LeadsReport.Add(marketActionAfter2LeadsReport);

            }
            else
            {
                MarketActionAfter2LeadsReport findOne = db.MarketActionAfter2LeadsReport.Where(x => (x.MarketActionId == marketActionAfter2LeadsReport.MarketActionId && x.SeqNO == marketActionAfter2LeadsReport.SeqNO)).FirstOrDefault();
                if (findOne == null)
                {
                    marketActionAfter2LeadsReport.InDateTime = DateTime.Now;
                    marketActionAfter2LeadsReport.ModifyDateTime = DateTime.Now;
                    db.MarketActionAfter2LeadsReport.Add(marketActionAfter2LeadsReport);
                }
                else
                {
                    findOne.BPNO = marketActionAfter2LeadsReport.BPNO;
                    findOne.CustomerName = marketActionAfter2LeadsReport.CustomerName;
                    findOne.TelNO = marketActionAfter2LeadsReport.TelNO;
                    findOne.DealCheck = marketActionAfter2LeadsReport.DealCheck;
                    findOne.DealModel = marketActionAfter2LeadsReport.DealModel;
                    findOne.InterestedModel = marketActionAfter2LeadsReport.InterestedModel;
                    findOne.LeadsCheck = marketActionAfter2LeadsReport.LeadsCheck;
                    findOne.ModifyDateTime = DateTime.Now;
                    findOne.ModifyUserId = marketActionAfter2LeadsReport.ModifyUserId;
                    findOne.OwnerCheck = marketActionAfter2LeadsReport.OwnerCheck;
                    findOne.TestDriverCheck = marketActionAfter2LeadsReport.TestDriverCheck;
                }
            }
            db.SaveChanges();
            return marketActionAfter2LeadsReport;
        }
        public void MarketActionAfter2LeadsReportDelete(string marketActionId, string seqNO)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId), new SqlParameter("@SeqNO", seqNO) };
            string sql = @"DELETE MarketActionAfter2LeadsReport WHERE MarketActionId = @MarketActionId AND SeqNO = @SeqNO
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        public List<MarketActionLeadsCountDto> MarketActionLeadsCountSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionLeadsCountDto);
            string sql = "";
            sql += @"SELECT * FROM
                    (SELECT
                            ISNULL(SUM(CASE WHEN OwnerCheck= 1 AND LeadsCheck = 1 THEN 1 ELSE 0 END),0) AS LeadOwnerCount,
                            ISNULL(SUM(CASE WHEN OwnerCheck <> 1 AND LeadsCheck = 1 THEN 1 ELSE 0 END), 0) AS LeadPCCount,
                            ISNULL(SUM(CASE WHEN OwnerCheck = 1 AND TestDriverCheck = 1 THEN 1 ELSE 0 END), 0) AS TestDriverOwnerCount,
                            ISNULL(SUM(CASE WHEN OwnerCheck <> 1 AND TestDriverCheck = 1 THEN 1 ELSE 0 END), 0) AS TestDriverPCCount,
                            ISNULL(SUM(CASE WHEN OwnerCheck = 1 AND DealCheck = 1 THEN 1 ELSE 0 END), 0) AS ActualOrderOwnerCount,
                            ISNULL(SUM(CASE WHEN OwnerCheck <> 1 AND DealCheck = 1 THEN 1 ELSE 0 END), 0) AS ActualOrderPCCount
                    FROM MarketActionAfter2LeadsReport A 
                    WHERE   A.MarketActionId = @MarketActionId) X INNER JOIN 
                    (SELECT ISNULL(SUM(ISNULL(UnitPrice,0)*ISNULL(Counts,0)),0) AS ExpenseTotalAmt 
                    FROM MarketActionAfter7ActualExpense A WHERE A.MarketActionId = @MarketActionId) Y ON 1=1";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionLeadsCountDto>().ToList();
        }

        #endregion
        #region Seven days after
        public List<MarketActionAfter7> MarketActionAfter7Search(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionAfter7);
            string sql = "";
            sql += @"SELECT A.* 
                    FROM [MarketActionAfter7] A 
                    WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionAfter7>().ToList();
        }
        public void MarketActionAfter7Save(MarketActionAfter7 marketActionAfter7)
        {
            MarketActionAfter7 findOne = db.MarketActionAfter7.Where(x => (x.MarketActionId == marketActionAfter7.MarketActionId)).FirstOrDefault();
            if (findOne == null)
            {
                marketActionAfter7.InDateTime = DateTime.Now;
                marketActionAfter7.ModifyDateTime = DateTime.Now;
                db.MarketActionAfter7.Add(marketActionAfter7);
            }
            else
            {
                findOne.AttendenceOwnerCount = marketActionAfter7.AttendenceOwnerCount;
                findOne.AttendencePCCount = marketActionAfter7.AttendencePCCount;
                findOne.CarDisplayDesc01 = marketActionAfter7.CarDisplayDesc01;
                findOne.CarDisplayDesc02 = marketActionAfter7.CarDisplayDesc02;
                findOne.CarDisplayDesc03 = marketActionAfter7.CarDisplayDesc03;
                findOne.CarDisplayDesc04 = marketActionAfter7.CarDisplayDesc04;
                if (marketActionAfter7.CarDisplayPic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.CarDisplayPic01 = marketActionAfter7.CarDisplayPic01;
                if (marketActionAfter7.CarDisplayPic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.CarDisplayPic02 = marketActionAfter7.CarDisplayPic02;
                if (marketActionAfter7.CarDisplayPic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.CarDisplayPic03 = marketActionAfter7.CarDisplayPic03;
                if (marketActionAfter7.CarDisplayPic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.CarDisplayPic04 = marketActionAfter7.CarDisplayPic04;
                findOne.CustomerFeedback = marketActionAfter7.CustomerFeedback;
                findOne.CustomerStaffModelDesc01 = marketActionAfter7.CustomerStaffModelDesc01;
                findOne.CustomerStaffModelDesc02 = marketActionAfter7.CustomerStaffModelDesc02;
                findOne.CustomerStaffModelDesc03 = marketActionAfter7.CustomerStaffModelDesc03;
                findOne.CustomerStaffModelDesc04 = marketActionAfter7.CustomerStaffModelDesc04;
                if (marketActionAfter7.CustomerStaffModelPic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.CustomerStaffModelPic01 = marketActionAfter7.CustomerStaffModelPic01;
                if (marketActionAfter7.CustomerStaffModelPic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.CustomerStaffModelPic02 = marketActionAfter7.CustomerStaffModelPic02;
                if (marketActionAfter7.CustomerStaffModelPic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.CustomerStaffModelPic03 = marketActionAfter7.CustomerStaffModelPic03;
                if (marketActionAfter7.CustomerStaffModelPic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.CustomerStaffModelPic04 = marketActionAfter7.CustomerStaffModelPic04;
                findOne.HightLights = marketActionAfter7.HightLights;
                findOne.ImproveArea = marketActionAfter7.ImproveArea;
                findOne.MarketSaleTeamAdvice = marketActionAfter7.MarketSaleTeamAdvice;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = marketActionAfter7.ModifyUserId;
                findOne.OnLineAdDesc01 = marketActionAfter7.OnLineAdDesc01;
                findOne.OnLineAdDesc02 = marketActionAfter7.OnLineAdDesc02;
                findOne.OnLineAdDesc03 = marketActionAfter7.OnLineAdDesc03;
                findOne.OnLineAdDesc04 = marketActionAfter7.OnLineAdDesc04;
                if (marketActionAfter7.OnLineAdPic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OnLineAdPic01 = marketActionAfter7.OnLineAdPic01;
                if (marketActionAfter7.OnLineAdPic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OnLineAdPic02 = marketActionAfter7.OnLineAdPic02;
                if (marketActionAfter7.OnLineAdPic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OnLineAdPic03 = marketActionAfter7.OnLineAdPic03;
                if (marketActionAfter7.OnLineAdPic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OnLineAdPic04 = marketActionAfter7.OnLineAdPic04;
                findOne.OthersDesc01 = marketActionAfter7.OthersDesc01;
                findOne.OthersDesc02 = marketActionAfter7.OthersDesc02;
                findOne.OthersDesc03 = marketActionAfter7.OthersDesc03;
                findOne.OthersDesc04 = marketActionAfter7.OthersDesc04;
                if (marketActionAfter7.OthersPic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OthersPic01 = marketActionAfter7.OthersPic01;
                if (marketActionAfter7.OthersPic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OthersPic02 = marketActionAfter7.OthersPic02;
                if (marketActionAfter7.OthersPic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OthersPic03 = marketActionAfter7.OthersPic03;
                if (marketActionAfter7.OthersPic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.OthersPic04 = marketActionAfter7.OthersPic04;
                findOne.PlaceDesc01 = marketActionAfter7.PlaceDesc01;
                findOne.PlaceDesc02 = marketActionAfter7.PlaceDesc02;
                findOne.PlaceDesc03 = marketActionAfter7.PlaceDesc03;
                findOne.PlaceDesc04 = marketActionAfter7.PlaceDesc04;
                if (marketActionAfter7.PlacePic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.PlacePic01 = marketActionAfter7.PlacePic01;
                if (marketActionAfter7.PlacePic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.PlacePic02 = marketActionAfter7.PlacePic02;
                if (marketActionAfter7.PlacePic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.PlacePic03 = marketActionAfter7.PlacePic03;
                if (marketActionAfter7.PlacePic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.PlacePic04 = marketActionAfter7.PlacePic04;
                findOne.RegisterLiveShowDesc01 = marketActionAfter7.RegisterLiveShowDesc01;
                findOne.RegisterLiveShowDesc02 = marketActionAfter7.RegisterLiveShowDesc02;
                findOne.RegisterLiveShowDesc03 = marketActionAfter7.RegisterLiveShowDesc03;
                findOne.RegisterLiveShowDesc04 = marketActionAfter7.RegisterLiveShowDesc04;
                if (marketActionAfter7.RegisterLiveShowPic01!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.RegisterLiveShowPic01 = marketActionAfter7.RegisterLiveShowPic01;
                if (marketActionAfter7.RegisterLiveShowPic02!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.RegisterLiveShowPic02 = marketActionAfter7.RegisterLiveShowPic02;
                if (marketActionAfter7.RegisterLiveShowPic03!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.RegisterLiveShowPic03 = marketActionAfter7.RegisterLiveShowPic03;
                if (marketActionAfter7.RegisterLiveShowPic04!="https://yrsurvey.oss-cn-beijing.aliyuncs.com/Bentley/fail2.png")
                    findOne.RegisterLiveShowPic04 = marketActionAfter7.RegisterLiveShowPic04;

            }

            db.SaveChanges();
        }
        public List<MarketActionAfter7ActualExpenseDto> MarketActionAfter7ActualExpenseSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionAfter7ActualExpenseDto);
            string sql = "";
            sql += @"SELECT A.*,ISNULL(A.UnitPrice,0)*ISNULL(Counts,0) AS Total  FROM [MarketActionAfter7ActualExpense] A WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionAfter7ActualExpenseDto>().ToList();
        }
        public void MarketActionAfter7ActualExpenseSave(MarketActionAfter7ActualExpense marketActionAfter7ActualExpense)
        {
            //if (marketActionAfter7ActualExpense.SeqNO == 0)
            //{
            MarketActionAfter7ActualExpense findOneMax = db.MarketActionAfter7ActualExpense.Where(x => (x.MarketActionId == marketActionAfter7ActualExpense.MarketActionId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
            if (findOneMax == null)
            {
                marketActionAfter7ActualExpense.SeqNO = 1;
            }
            else
            {
                marketActionAfter7ActualExpense.SeqNO = findOneMax.SeqNO + 1;
            }
            marketActionAfter7ActualExpense.InDateTime = DateTime.Now;
            marketActionAfter7ActualExpense.ModifyDateTime = DateTime.Now;
            db.MarketActionAfter7ActualExpense.Add(marketActionAfter7ActualExpense);

            //}
            //else
            //{
            //    MarketActionAfter7ActualExpense findOne = db.MarketActionAfter7ActualExpense.Where(x => (x.MarketActionId == marketActionAfter7ActualExpense.MarketActionId && x.SeqNO == marketActionAfter7ActualExpense.SeqNO)).FirstOrDefault();
            //    findOne.Descs = marketActionAfter7ActualExpense.Descs;
            //    findOne.Item = marketActionAfter7ActualExpense.Item;
            //    findOne.ModifyDateTime = DateTime.Now;
            //    findOne.ModifyUserId = marketActionAfter7ActualExpense.ModifyUserId;
            //    findOne.Counts = marketActionAfter7ActualExpense.Counts;
            //    findOne.UnitPrice = marketActionAfter7ActualExpense.UnitPrice;
            //}
            db.SaveChanges();
        }
        public void MarketActionAfter7ActualExpenseDelete(string marketActionId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            string sql = @"DELETE MarketActionAfter7ActualExpense WHERE MarketActionId = @MarketActionId 
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }

        public List<MarketActionAfter7ActualProcess> MarketActionAfter7ActualProcessSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionAfter7ActualProcess);
            string sql = "";
            sql += @"SELECT *  FROM [MarketActionAfter7ActualProcess] WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionAfter7ActualProcess>().ToList();
        }
        public void MarketActionAfter7ActualProcessSave(MarketActionAfter7ActualProcess marketActionAfter7ActualProcess)
        {
            //if (marketActionAfter7ActualProcess.SeqNO == 0)
            //{
            MarketActionAfter7ActualProcess findOneMax = db.MarketActionAfter7ActualProcess.Where(x => (x.MarketActionId == marketActionAfter7ActualProcess.MarketActionId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
            if (findOneMax == null)
            {
                marketActionAfter7ActualProcess.SeqNO = 1;
            }
            else
            {
                marketActionAfter7ActualProcess.SeqNO = findOneMax.SeqNO + 1;
            }
            marketActionAfter7ActualProcess.InDateTime = DateTime.Now;
            marketActionAfter7ActualProcess.ModifyDateTime = DateTime.Now;
            db.MarketActionAfter7ActualProcess.Add(marketActionAfter7ActualProcess);

            //}
            //else
            //{
            //    MarketActionAfter7ActualProcess findOne = db.MarketActionAfter7ActualProcess.Where(x => (x.MarketActionId == marketActionAfter7ActualProcess.MarketActionId && x.SeqNO == marketActionAfter7ActualProcess.SeqNO)).FirstOrDefault();
            //    findOne.ActivityDateTime = marketActionAfter7ActualProcess.ActivityDateTime;
            //    findOne.Contents = marketActionAfter7ActualProcess.Contents;
            //    findOne.Item = marketActionAfter7ActualProcess.Item;
            //    findOne.ModifyDateTime = DateTime.Now;
            //    findOne.ModifyUserId = marketActionAfter7ActualProcess.ModifyUserId;
            //    findOne.Remark = marketActionAfter7ActualProcess.Remark;

            //}
            db.SaveChanges();
        }
        public void MarketActionAfter7ActualProcessDelete(string marketActionId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            string sql = @"DELETE MarketActionAfter7ActualProcess WHERE MarketActionId = @MarketActionId 
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion
        #region 30 days after
        public void MarketActionAfter30LeadsReportUpdate(MarketActionAfter30LeadsReportUpdate marketActionAfter30LeadsReportUpdate)
        {

            MarketActionAfter30LeadsReportUpdate findOne = db.MarketActionAfter30LeadsReportUpdate.Where(x => (x.MarketActionId == marketActionAfter30LeadsReportUpdate.MarketActionId)).FirstOrDefault();
            if (findOne == null)
            {
                marketActionAfter30LeadsReportUpdate.InDateTime = DateTime.Now;
                marketActionAfter30LeadsReportUpdate.ModifyDateTime = DateTime.Now;
                db.MarketActionAfter30LeadsReportUpdate.Add(marketActionAfter30LeadsReportUpdate);
            }

            else
            {
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = marketActionAfter30LeadsReportUpdate.ModifyUserId;
            }

            db.SaveChanges();
        }
        #endregion
        #region 3 months  after
        public List<MarketActionAfter90File> MarketActionAfter90FileSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionAfter90File);
            string sql = "";
            sql += @"SELECT A.* 
                    FROM [MarketActionAfter90File] A 
                    WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionAfter90File>().ToList();
        }
        public void MarketActionAfter90FileSave(MarketActionAfter90File marketActionAfter90File)
        {
            if (marketActionAfter90File.SeqNO == 0)
            {
                MarketActionAfter90File findOneMax = db.MarketActionAfter90File.Where(x => (x.MarketActionId == marketActionAfter90File.MarketActionId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
                if (findOneMax == null)
                {
                    marketActionAfter90File.SeqNO = 1;
                }
                else
                {
                    marketActionAfter90File.SeqNO = findOneMax.SeqNO + 1;
                }
                marketActionAfter90File.InDateTime = DateTime.Now;
                marketActionAfter90File.ModifyDateTime = DateTime.Now;
                db.MarketActionAfter90File.Add(marketActionAfter90File);

            }
            else
            {

            }
            db.SaveChanges();
        }
        public void MarketActionAfter90FileDelete(string marketActionId, string seqNO)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId), new SqlParameter("@SeqNO", seqNO), };
            string sql = @"DELETE MarketActionAfter90File WHERE MarketActionId = @MarketActionId AND SeqNO = @SeqNO
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion
        #region 总览
        public List<MarketActionStatusCountDto> MarketActionStatusCountSearch(string year, List<Shop> roleTypeShop)
        {
            if (year == null) year = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Year", year) };
            Type t = typeof(MarketActionStatusCountDto);
            string sql = "";
            sql += @"SELECT ISNULL(SUM(Before21Count),0) AS Before21Count 
	                       ,ISNULL(SUM(Before3Count),0) AS Before3Count
	                       ,ISNULL(SUM(After2Count),0) AS After2Count
	                       ,ISNULL(SUM(After7Count),0) AS After7Count
	                       ,ISNULL(SUM(After30Count),0) AS After30Count
                    FROM (
                            SELECT 
                            CASE WHEN NOT EXISTS(SELECT 1 FROM MarketActionBefore21 WHERE MarketActionId = A.MarketActionId)
		                            AND NOT EXISTS(SELECT 1 FROM MarketActionBefore21ActivityProcess WHERE MarketActionId = A.MarketActionId)
				                            THEN 1
				                            ELSE 0
			                            END AS Before21Count
                            ,CASE WHEN NOT EXISTS(SELECT 1 FROM MarketActionBefore3BugetDetail WHERE MarketActionId = A.MarketActionId)
		                            AND NOT EXISTS(SELECT 1 FROM MarketActionBefore3DisplayModel WHERE MarketActionId = A.MarketActionId)
		                            AND NOT EXISTS(SELECT 1 FROM MarketActionBefore3TestDriver WHERE MarketActionId = A.MarketActionId)
                                    AND EXISTS (SELECT TOP 1 EventTypeName FROM EventType WHERE EventTypeId = A.EventTypeId AND EventTypeName NOT IN ('数字营销','广告及宣传','线上平台线索获取'))
				                            THEN 1
				                            ELSE 0
			                            END AS Before3Count
                            ,CASE WHEN NOT EXISTS(SELECT 1 FROM MarketActionAfter2LeadsReport WHERE MarketActionId = A.MarketActionId)
				                            THEN 1
				                            ELSE 0
			                            END AS After2Count
                            ,CASE WHEN NOT EXISTS(SELECT 1 FROM MarketActionAfter7 WHERE MarketActionId = A.MarketActionId)
		                            AND NOT EXISTS(SELECT 1 FROM MarketActionAfter7ActualExpense WHERE MarketActionId = A.MarketActionId)
		                            AND NOT EXISTS(SELECT 1 FROM MarketActionAfter7ActualProcess WHERE MarketActionId = A.MarketActionId)
				                            THEN 1
				                            ELSE 0
			                            END  AS After7Count
                            ,CASE WHEN NOT EXISTS(SELECT 1 FROM MarketActionAfter30LeadsReportUpdate WHERE MarketActionId = A.MarketActionId)
				                            THEN 1
				                            ELSE 0
			                            END AS After30Count
                            FROM MarketAction A WHERE 1=1 AND A.MarketActionStatusCode<>2 ";
            if (!string.IsNullOrEmpty(year))
            {
                sql += " AND Year(A.StartDate) = @Year";

            }
            if (roleTypeShop != null && roleTypeShop.Count > 0)
            {
                sql += " AND A.ShopId IN( ";
                foreach (Shop shop in roleTypeShop)
                {
                    if (roleTypeShop.IndexOf(shop) == roleTypeShop.Count - 1)
                    {
                        sql += shop.ShopId;
                    }
                    else
                    {
                        sql += shop.ShopId + ",";
                    }
                }
                sql += ")";
            }
            sql += " ) B";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionStatusCountDto>().ToList();
        }
        #endregion
    }
}