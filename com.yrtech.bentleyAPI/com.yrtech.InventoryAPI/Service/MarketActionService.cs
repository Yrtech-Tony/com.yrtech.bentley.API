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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<MarketActionDto> MarketActionSearch(string actionName,string year,string month,string marketActionStatusCode,string shopId,string eventTypeId)
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
             sql += @"SELECT A.MarketActionId,A.ShopId,B.ShopCode,B.ShopName,A.ActionCode,A.ActionName
	                            ,A.EventTypeId,C.EventTypeName,C.EventTypeNameEn
	                            ,A.MarketActionStatusCode,D.HiddenCodeName ASMarketActionStatusName,D.HiddenCodeNameEn AS MarketActionStatusNameEn
	                            ,A.MarketActionTargetModelCode,E.HiddenCodeName AS MarketActionTargetModelName,E.HiddenCodeNameEn AS MarketActionTargetModelNameEn
                            FROM MarketAction A INNER JOIN Shop B ON A.ShopId = B.ShopId
					                            INNER JOIN EventType C ON A.EventTypeId = C.EventTypeId
					                            INNER JOIN HiddenCode D ON A.MarketActionStatusCode = D.HiddenCodeId AND D.HiddenCodeGroup = 'MarketActionStatus'
					                            INNER JOIN HiddenCode E ON A.MarketActionTargetModelCode  = E.HiddenCodeId AND E.HiddenCodeGroup = 'TargetModels'
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
                sql += " AND A.marketActionStatusCode =@MarketActionStatusCode";
            }
            if (!string.IsNullOrEmpty(shopId))
            {
                sql += " AND A.ShopId =@ShopId";
            }
            if (!string.IsNullOrEmpty(eventTypeId))
            {
                sql += " AND A.EventTypeId =@EventTypeId";
            }
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionDto>().ToList();
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
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId)};
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
                findOne.KeyVisionPic = marketActionBefore21.KeyVisionPic;
                findOne.ModifyDateTime = marketActionBefore21.ModifyDateTime;
                findOne.ModifyUserId = marketActionBefore21.ModifyUserId;
                findOne.OtherDesc01 = marketActionBefore21.OtherDesc01;
                findOne.OtherDesc02 = marketActionBefore21.OtherDesc02;
                findOne.OtherDesc03 = marketActionBefore21.OtherDesc03;
                findOne.OtherDesc04 = marketActionBefore21.OtherDesc04;
                findOne.OthersPic01 = marketActionBefore21.OthersPic01;
                findOne.OthersPic02 = marketActionBefore21.OthersPic02;
                findOne.OthersPic03 = marketActionBefore21.OthersPic03;
                findOne.OthersPic04 = marketActionBefore21.OthersPic04;
                findOne.PlaceIntroDesc01 = marketActionBefore21.PlaceIntroDesc01;
                findOne.PlaceIntroDesc02 = marketActionBefore21.PlaceIntroDesc02;
                findOne.PlaceIntroDesc03 = marketActionBefore21.PlaceIntroDesc03;
                findOne.PlaceIntroDesc04 = marketActionBefore21.PlaceIntroDesc04;
                findOne.PlaceIntroPic01 = marketActionBefore21.PlaceIntroPic01;
                findOne.PlaceIntroPic02 = marketActionBefore21.PlaceIntroPic02;
                findOne.PlaceIntroPic03 = marketActionBefore21.PlaceIntroPic03;
                findOne.PlaceIntroPic04 = marketActionBefore21.PlaceIntroPic04;
                findOne.POSDesignDesc01 = marketActionBefore21.POSDesignDesc01;
                findOne.POSDesignDesc02 = marketActionBefore21.POSDesignDesc02;
                findOne.POSDesignDesc03 = marketActionBefore21.POSDesignDesc03;
                findOne.POSDesignDesc04 = marketActionBefore21.POSDesignDesc04;
                findOne.POSDesignPic01 = marketActionBefore21.POSDesignPic01;
                findOne.POSDesignPic02 = marketActionBefore21.POSDesignPic02;
                findOne.POSDesignPic03 = marketActionBefore21.POSDesignPic03;
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
                findOne.TestDriverRoadMapPic01 = marketActionBefore21.TestDriverRoadMapPic01;
                findOne.TestDriverRoadMapPic02 = marketActionBefore21.TestDriverRoadMapPic02;
                findOne.TestDriverRoadMapPic03 = marketActionBefore21.TestDriverRoadMapPic03;
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
            if (marketActionBefore21ActivityProcess.SeqNO == 0)
            {
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

            }
            else
            {
                MarketActionBefore21ActivityProcess findOne = db.MarketActionBefore21ActivityProcess.Where(x => (x.MarketActionId == marketActionBefore21ActivityProcess.MarketActionId&&x.SeqNO== marketActionBefore21ActivityProcess.SeqNO)).FirstOrDefault();
                findOne.ActivityDateTime = marketActionBefore21ActivityProcess.ActivityDateTime;
                findOne.Contents = marketActionBefore21ActivityProcess.Contents;
                findOne.Item = marketActionBefore21ActivityProcess.Item;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = marketActionBefore21ActivityProcess.ModifyUserId;
                findOne.Remark = marketActionBefore21ActivityProcess.Remark;
            }
            db.SaveChanges();
        }
        public void MarketActionBefore21ActivityProcessDelete(string marketActionId,string seqNO)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId), new SqlParameter("@SeqNO", seqNO), };
            string sql = @"DELETE MarketActionBefore21ActivityProcess WHERE MarketActionId = @MarketActionId AND SeqNO = @SeqNO
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion
        #region Before three days
        public List<MarketActionBefore3BugetDetail> MarketActionBefore3BugetDetailSearch(string marketActionId)
        {
            if (marketActionId == null) marketActionId = "";

            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId) };
            Type t = typeof(MarketActionBefore3BugetDetail);
            string sql = "";
            sql += @"SELECT A.* 
                    FROM [MarketActionBefore3BugetDetail] A 
                    WHERE MarketActionId = @MarketActionId";
            return db.Database.SqlQuery(t, sql, para).Cast<MarketActionBefore3BugetDetail>().ToList();
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
            if (marketActionBefore3TestDriver.SeqNO == 0)
            {
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

            }
            else
            {
                MarketActionBefore3TestDriver findOne = db.MarketActionBefore3TestDriver.Where(x => (x.MarketActionId == marketActionBefore3TestDriver.MarketActionId && x.SeqNO == marketActionBefore3TestDriver.SeqNO)).FirstOrDefault();
                findOne.DisplayModelColor = marketActionBefore3TestDriver.DisplayModelColor;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = marketActionBefore3TestDriver.ModifyUserId;
                findOne.Provider = marketActionBefore3TestDriver.Provider;
            }
            db.SaveChanges();
        }
        public void MarketActionBefore3DisplayModelSave(MarketActionBefore3DisplayModel marketActionBefore3DisplayModel)
        {
            if (marketActionBefore3DisplayModel.SeqNO == 0)
            {
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

            }
            else
            {
                MarketActionBefore3DisplayModel findOne = db.MarketActionBefore3DisplayModel.Where(x => (x.MarketActionId == marketActionBefore3DisplayModel.MarketActionId && x.SeqNO == marketActionBefore3DisplayModel.SeqNO)).FirstOrDefault();
                findOne.DisplayModelColor = marketActionBefore3DisplayModel.DisplayModelColor;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = marketActionBefore3DisplayModel.ModifyUserId;
                findOne.Provider = marketActionBefore3DisplayModel.Provider;
            }
            db.SaveChanges();
        }
        public void MarketActionBefore3BugetDetailSave(MarketActionBefore3BugetDetail marketActionBefore3BugetDetail)
        {
            if (marketActionBefore3BugetDetail.SeqNO == 0)
            {
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

            }
            else
            {
                MarketActionBefore3BugetDetail findOne = db.MarketActionBefore3BugetDetail.Where(x => (x.MarketActionId == marketActionBefore3BugetDetail.MarketActionId && x.SeqNO == marketActionBefore3BugetDetail.SeqNO)).FirstOrDefault();
                findOne.Counts = marketActionBefore3BugetDetail.Counts;
                findOne.ModifyDateTime = DateTime.Now;
                findOne.ModifyUserId = marketActionBefore3BugetDetail.ModifyUserId;
                findOne.Descs = marketActionBefore3BugetDetail.Descs;
                findOne.ItemName = marketActionBefore3BugetDetail.ItemName;
                findOne.UnitPrice = marketActionBefore3BugetDetail.UnitPrice;
            }
            db.SaveChanges();
        }
        public void MarketActionBefore3TestDriverDelete(string marketActionId, string seqNO)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId), new SqlParameter("@SeqNO", seqNO), };
            string sql = @"DELETE MarketActionBefore3TestDriver WHERE MarketActionId = @MarketActionId AND SeqNO = @SeqNO
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        public void MarketActionBefore3BugetDetailDelete(string marketActionId, string seqNO)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId), new SqlParameter("@SeqNO", seqNO), };
            string sql = @"DELETE arketActionBefore3BugetDetail WHERE MarketActionId = @MarketActionId AND SeqNO = @SeqNO
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        public void MarketActionBefore3DisplayModelDelete(string marketActionId, string seqNO)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@MarketActionId", marketActionId), new SqlParameter("@SeqNO", seqNO), };
            string sql = @"DELETE MarketActionBefore3DisplayModel WHERE MarketActionId = @MarketActionId AND SeqNO = @SeqNO
                        ";
            db.Database.ExecuteSqlCommand(sql, para);
        }
        #endregion
    }
}