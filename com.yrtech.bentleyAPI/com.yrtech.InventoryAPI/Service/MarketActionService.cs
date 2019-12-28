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
    }
}