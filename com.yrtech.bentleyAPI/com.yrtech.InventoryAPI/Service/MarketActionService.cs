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
                                                        new SqlParameter("@EventTypeId", eventTypeId),};
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
    }
}