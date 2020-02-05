﻿using System.Web.Http;
using System.Linq;
using com.yrtech.InventoryAPI.Service;
using com.yrtech.InventoryAPI.Common;
using System.Collections.Generic;
using System;
using com.yrtech.InventoryAPI.Controllers;
using com.yrtech.InventoryAPI.DTO;
using com.yrtech.bentley.DAL;
using System.Web.Configuration;

namespace com.yrtech.SurveyAPI.Controllers
{

    [RoutePrefix("bentley/api")]
    public class AnswerController : BaseController
    {
        CommitFileService commitFileService = new CommitFileService();
        MasterService masterService = new MasterService();
        MarketActionService marketActionService = new MarketActionService();
        AccountService accountService = new AccountService();
        DMFService dmfService = new DMFService();
        ExcelDataService excelDataService = new ExcelDataService();

        #region CommitFile
        [HttpGet]
        [Route("CommitFile/ShopCommitFileRecordStatusSearch")]
        public APIResult ShopCommitFileRecordStatusSearch(string year, string shopId, string userId, string roleTypeCode)
        {
            try
            {
                ShopCommitFileRecordListDto shopCommitFileRecordList = new ShopCommitFileRecordListDto();
                shopCommitFileRecordList.ShopCommitFileRecordStatusList = commitFileService.ShopCommitFileRecordStatusSearch(year, shopId);

                // 按照权限查询显示经销商信息
                List<Shop> roleTypeShopList = accountService.GetShopByRole(userId, roleTypeCode);
                List<ShopDto> shopListTemp = masterService.ShopSearch(shopId, "", "", "");
                List<ShopDto> shopList = new List<ShopDto>();
                foreach (ShopDto shopdto in shopListTemp)
                {
                    foreach (Shop shop in roleTypeShopList)
                    {
                        if (shopdto.ShopId == shop.ShopId)
                        {
                            shopList.Add(shopdto);
                        }
                    }
                }

                shopCommitFileRecordList.ShopList = shopList;
                shopCommitFileRecordList.CommitFileList = commitFileService.CommitFileSearch(year);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(shopCommitFileRecordList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }

        [HttpGet]
        [Route("CommitFile/ShopCommitFileRecordSearch")]
        public APIResult ShopCommitFileRecordSearch(string shopId, string fileId)
        {
            try
            {
                List<ShopCommitFileRecord> shopCommitFileRecordList = commitFileService.ShopCommitFileRecordSearch(shopId, fileId);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(shopCommitFileRecordList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("CommitFile/ShopCommitFileRecordSave")]
        public APIResult ShopCommitFileRecordSave(ShopCommitFileRecord shopCommitFileRecord)
        {
            try
            {
                commitFileService.ShopCommitFileRecordSave(shopCommitFileRecord);
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("CommitFile/ShopCommitFileRecordDelete")]
        public APIResult ShopCommitFileRecordDelete([FromBody]UploadData upload)
        {
            try
            {
                List<ShopCommitFileRecord> list = CommonHelper.DecodeString<List<ShopCommitFileRecord>>(upload.ListJson);
                foreach (ShopCommitFileRecord record in list)
                {
                    commitFileService.ShopCommitFileRecordDelete(record.ShopId.ToString(), record.FileId.ToString(), record.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region MarketAction
        [HttpGet]
        [Route("MarketAction/MarketActionSearch")]
        public APIResult MarketActionSearch(string actionName, string year, string month, string marketActionStatusCode, string shopId, string eventTypeId, bool? expenseAccountChk, string userId, string roleTypeCode)
        {
            try
            {

                List<MarketActionDto> marketActionListTemp = marketActionService.MarketActionSearch(actionName, year, month, marketActionStatusCode, shopId, eventTypeId, expenseAccountChk);
                List<Shop> roleTypeShopList = accountService.GetShopByRole(userId, roleTypeCode);
                List<MarketActionDto> marketActionList = new List<MarketActionDto>();

                foreach (MarketActionDto marketActionDto in marketActionListTemp)
                {
                    foreach (Shop shop in roleTypeShopList)
                    {
                        if (marketActionDto.ShopId == shop.ShopId)
                        {
                            marketActionList.Add(marketActionDto);
                        }
                    }
                }
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpGet]
        [Route("MarketAction/MarketActionNotCancelSearch")]
        public APIResult MarketActionNotCancelSearch(string eventTypeId,string userId, string roleTypeCode)
        {
            try
            {

                List<MarketAction> marketActionListTemp = marketActionService.MarketActionNotCancelSearch( eventTypeId);
                List<Shop> roleTypeShopList = accountService.GetShopByRole(userId, roleTypeCode);
                List<MarketAction> marketActionList = new List<MarketAction>();

                foreach (MarketAction marketAction in marketActionListTemp)
                {
                    foreach (Shop shop in roleTypeShopList)
                    {
                        if (marketAction.ShopId == shop.ShopId)
                        {
                            marketActionList.Add(marketAction);
                        }
                    }
                }
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpGet]
        [Route("MarketAction/MarketActionSearchById")]
        public APIResult MarketActionSearchById(string marketActionId)
        {
            try
            {

                List<MarketAction> marketActionList = marketActionService.MarketActionSearchById(marketActionId);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpGet]
        [Route("MarketAction/MarketActionExport")]
        public APIResult MarketActionExportSearch(string actionName, string year, string month, string marketActionStatusCode, string shopId, string eventTypeId, bool? expenseAccountChk, string userId, string roleTypeCode)
        {
            try
            {
                string filePath = excelDataService.MarketActionExport(actionName, year, month, marketActionStatusCode, shopId, eventTypeId, expenseAccountChk, userId, roleTypeCode);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(new { FilePath = filePath }) };

            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("MarketAction/MarketActionSave")]
        public APIResult MarketActionSave(MarketAction marketAction)
        {
            try
            {
                marketActionService.MarketActionSave(marketAction);
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("MarketAction/MarketActionDelete")]
        public APIResult MarketActionDelete(UploadData upload)
        {
            try
            {
                List<MarketAction> list = CommonHelper.DecodeString<List<MarketAction>>(upload.ListJson);
                foreach (MarketAction marketAction in list)
                {
                    marketActionService.MarketActionDelete(marketAction.MarketActionId.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpGet]
        [Route("MarketAction/MarketActionAllLeadsReportExport")]
        public APIResult MarketActionAllLeadsReportExport(string year)
        {
            try
            {
                string filePath = excelDataService.MarketActionAllLeadsReportExport(year);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(new { FilePath = filePath }) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }

        #region Before21
        [HttpGet]
        [Route("MarketAction/MarketActionBefore21Search")]
        public APIResult MarketActionBefore21Search(string marketActionId)
        {
            try
            {
                MarketActionBefore21MainDto marketActionBefore21MainDto = new MarketActionBefore21MainDto();
                List<MarketActionBefore21> marketActionBefore21List = marketActionService.MarketActionBefore21Search(marketActionId);
                if (marketActionBefore21List != null && marketActionBefore21List.Count > 0)
                {
                    marketActionBefore21MainDto.MarketActionBefore21 = marketActionBefore21List[0];
                }
                marketActionBefore21MainDto.ActivityProcess = marketActionService.MarketActionBefore21ActivityProcessSearch(marketActionId);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionBefore21MainDto) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("MarketAction/MarketActionBefore21Save")]
        public APIResult MarketActionBefore21Save(UploadData upload)
        {
            try
            {
                #region MyRegion
                //MarketActionBefore21 marketActionBefore21 = new MarketActionBefore21();
                //marketActionBefore21.ActivityBackground = marketActionBefore21MainDto.MarketActionBefore21.ActivityBackground;
                //marketActionBefore21.ActivityDesc = marketActionBefore21MainDto.MarketActionBefore21.ActivityDesc;
                //marketActionBefore21.ActivityObjective = marketActionBefore21MainDto.MarketActionBefore21.ActivityObjective;
                //marketActionBefore21.Budget = marketActionBefore21MainDto.MarketActionBefore21.Budget;
                //marketActionBefore21.InDateTime = marketActionBefore21MainDto.MarketActionBefore21.InDateTime;
                //marketActionBefore21.InUserId = marketActionBefore21MainDto.MarketActionBefore21.InUserId;
                //marketActionBefore21.KeyVisionApprovalCode = marketActionBefore21MainDto.MarketActionBefore21.KeyVisionApprovalCode;
                //marketActionBefore21.KeyVisionApprovalDesc = marketActionBefore21MainDto.MarketActionBefore21.KeyVisionApprovalDesc;
                //marketActionBefore21.KeyVisionDesc = marketActionBefore21MainDto.MarketActionBefore21.KeyVisionDesc;
                //marketActionBefore21.KeyVisionPic = marketActionBefore21MainDto.MarketActionBefore21.KeyVisionPic;
                //marketActionBefore21.ModifyDateTime = marketActionBefore21MainDto.MarketActionBefore21.ModifyDateTime;
                //marketActionBefore21.ModifyUserId = marketActionBefore21MainDto.MarketActionBefore21.ModifyUserId;
                //marketActionBefore21.OtherDesc01 = marketActionBefore21MainDto.MarketActionBefore21.OtherDesc01;
                //marketActionBefore21.OtherDesc02 = marketActionBefore21MainDto.MarketActionBefore21.OtherDesc02;
                //marketActionBefore21.OtherDesc03 = marketActionBefore21MainDto.MarketActionBefore21.OtherDesc03;
                //marketActionBefore21.OtherDesc04 = marketActionBefore21MainDto.MarketActionBefore21.OtherDesc04;
                //marketActionBefore21.OthersPic01 = marketActionBefore21MainDto.MarketActionBefore21.OthersPic01;
                //marketActionBefore21.OthersPic02 = marketActionBefore21MainDto.MarketActionBefore21.OthersPic02;
                //marketActionBefore21.OthersPic03 = marketActionBefore21MainDto.MarketActionBefore21.OthersPic03;
                //marketActionBefore21.OthersPic04 = marketActionBefore21MainDto.MarketActionBefore21.OthersPic04;
                //marketActionBefore21.PlaceIntroDesc01 = marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroDesc01;
                //marketActionBefore21.PlaceIntroDesc02 = marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroDesc02;
                //marketActionBefore21.PlaceIntroDesc03 = marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroDesc03;
                //marketActionBefore21.PlaceIntroDesc04 = marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroDesc04;
                //marketActionBefore21.PlaceIntroPic01 = marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic01;
                //marketActionBefore21.PlaceIntroPic02 = marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic02;
                //marketActionBefore21.PlaceIntroPic03 = marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic03;
                //marketActionBefore21.PlaceIntroPic04 = marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic04;
                //marketActionBefore21.POSDesignDesc01 = marketActionBefore21MainDto.MarketActionBefore21.POSDesignDesc01;
                //marketActionBefore21.POSDesignDesc02 = marketActionBefore21MainDto.MarketActionBefore21.POSDesignDesc02;
                //marketActionBefore21.POSDesignDesc03 = marketActionBefore21MainDto.MarketActionBefore21.POSDesignDesc03;
                //marketActionBefore21.POSDesignDesc04 = marketActionBefore21MainDto.MarketActionBefore21.POSDesignDesc04;
                //marketActionBefore21.POSDesignPic01 = marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic01;
                //marketActionBefore21.POSDesignPic02 = marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic02;
                //marketActionBefore21.POSDesignPic03 = marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic03;
                //marketActionBefore21.POSDesignPic04 = marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic04;
                //marketActionBefore21.TargetLeadsOwnerCount = marketActionBefore21MainDto.MarketActionBefore21.TargetLeadsOwnerCount;
                //marketActionBefore21.TargetLeadsPCCount = marketActionBefore21MainDto.MarketActionBefore21.TargetLeadsPCCount;
                //marketActionBefore21.TargetOrdersOwnerCount = marketActionBefore21MainDto.MarketActionBefore21.TargetOrdersOwnerCount;
                //marketActionBefore21.TargetOrdersPCCount = marketActionBefore21MainDto.MarketActionBefore21.TargetOrdersPCCount;
                //marketActionBefore21.TargetParticipationOwnerCount = marketActionBefore21MainDto.MarketActionBefore21.TargetParticipationOwnerCount;
                //marketActionBefore21.TargetParticipationPCCount = marketActionBefore21MainDto.MarketActionBefore21.TargetParticipationPCCount;
                //marketActionBefore21.TargetTestDriveOwnerCount = marketActionBefore21MainDto.MarketActionBefore21.TargetTestDriveOwnerCount;
                //marketActionBefore21.TargetTestDrivePCCount = marketActionBefore21MainDto.MarketActionBefore21.TargetTestDrivePCCount;
                //marketActionBefore21.TestDriverRoadMapDesc01 = marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapDesc01;
                //marketActionBefore21.TestDriverRoadMapDesc02 = marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapDesc02;
                //marketActionBefore21.TestDriverRoadMapDesc03 = marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapDesc03;
                //marketActionBefore21.TestDriverRoadMapDesc04 = marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapDesc04;
                //marketActionBefore21.TestDriverRoadMapPic01 = marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic01;
                //marketActionBefore21.TestDriverRoadMapPic02 = marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic02;
                //marketActionBefore21.TestDriverRoadMapPic03 = marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic03;
                //marketActionBefore21.TestDriverRoadMapPic04 = marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic04;
                #endregion
                MarketActionBefore21MainDto marketActionBefore21MainDto = CommonHelper.DecodeString<MarketActionBefore21MainDto>(upload.ListJson);

                // 更新主推车型
                List<MarketAction> marketActionList = marketActionService.MarketActionSearchById(marketActionBefore21MainDto.MarketActionId.ToString());
                foreach (MarketAction market in marketActionList)
                {
                    market.MarketActionTargetModelCode = marketActionBefore21MainDto.TarketModelCode;
                    marketActionService.MarketActionSave(market);
                }

                marketActionBefore21MainDto.MarketActionBefore21.KeyVisionPic = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.KeyVisionPic);
                marketActionBefore21MainDto.MarketActionBefore21.OthersPic01 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.OthersPic01);
                marketActionBefore21MainDto.MarketActionBefore21.OthersPic02 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.OthersPic02);
                marketActionBefore21MainDto.MarketActionBefore21.OthersPic03 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.OthersPic03);
                marketActionBefore21MainDto.MarketActionBefore21.OthersPic04 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.OthersPic04);

                marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic01 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic01);
                marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic02 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic02);
                marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic03 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic03);
                marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic04 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.PlaceIntroPic04);

                marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic01 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic01);
                marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic02 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic02);
                marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic03 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic03);
                marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic04 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.POSDesignPic04);

                marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic01 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic01);
                marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic02 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic02);
                marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic03 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic03);
                marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic04 = UploadBase64Pic("", marketActionBefore21MainDto.MarketActionBefore21.TestDriverRoadMapPic04);

                marketActionService.MarketActionBefore21Save(marketActionBefore21MainDto.MarketActionBefore21);
                // 先全部删除活动流程，然后统一再保存一边
                marketActionService.MarketActionBefore21ActivityProcessDelete(marketActionBefore21MainDto.MarketActionId.ToString());
                foreach (MarketActionBefore21ActivityProcess process in marketActionBefore21MainDto.ActivityProcess)
                {
                    marketActionService.MarketActionBefore21ActivityProcessSave(process);
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        //[HttpPost]
        //[Route("MarketAction/MarketActionBefore21ActivityProcessDelete")]
        //public APIResult MarketActionBefore21ActivityProcessDelete(UploadData upload)
        //{
        //    try
        //    {
        //        List<MarketActionBefore21ActivityProcess> list = CommonHelper.DecodeString<List<MarketActionBefore21ActivityProcess>>(upload.ListJson);
        //        foreach (MarketActionBefore21ActivityProcess process in list)
        //        {
        //            marketActionService.MarketActionBefore21ActivityProcessDelete(process.MarketActionId.ToString(), process.SeqNO.ToString());
        //        }
        //        return new APIResult() { Status = true, Body = "" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new APIResult() { Status = false, Body = ex.Message.ToString() };
        //    }

        //}
        [HttpGet]
        [Route("MarketAction/KeyVisionSendEmailToBMC")]
        public APIResult KeyVisionSendEmailToBMC(string marketActionId)
        {
            try
            {
                string marketactionName = "";
                List<MarketAction> marketAction = marketActionService.MarketActionSearchById(marketActionId);
                List<ShopDto> shop = new List<ShopDto>();
                if (marketAction != null && marketAction.Count > 0)
                {
                    marketactionName = marketAction[0].ActionName;
                    shop = masterService.ShopSearch(marketAction[0].ShopId.ToString(), "", "", "");
                }
                SendEmail(WebConfigurationManager.AppSettings["KeyVisionEmail_To"], WebConfigurationManager.AppSettings["KeyVisionEmail_CC"]
                        , "主视觉画面审批", "宾利经销商【" + shop[0].ShopName + "】的市场活动【" + marketactionName + "】的画面审核已提交，请审核", "", "");
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                CommonHelper.log(ex.Message.ToString());
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpGet]
        [Route("MarketAction/KeyVisionSendEmailToShop")]
        public APIResult KeyVisionSendEmailToShop(string marketActionId)
        {
            try
            {
                string marketactionName = "";
                List<MarketAction> marketAction = marketActionService.MarketActionSearchById(marketActionId);
                List<ShopDto> shop = new List<ShopDto>();
                List<UserInfoDto> userinfo = new List<UserInfoDto>();
                if (marketAction != null && marketAction.Count > 0)
                {
                    marketactionName = marketAction[0].ActionName;
                    shop = masterService.ShopSearch(marketAction[0].ShopId.ToString(), "", "", "");
                    userinfo = masterService.UserInfoSearch("", "", shop[0].ShopName.ToString());
                }
                SendEmail(userinfo[0].Email, WebConfigurationManager.AppSettings["KeyVisionEmail_CC"], "主视觉审批修改意见", "宾利经销商【" + shop[0].ShopName + "】的市场活动【" + marketactionName + "】的画面审核意见已更新,请登陆DMN系统查看，并按要求完成更新", "", "");
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                CommonHelper.log(ex.Message.ToString());
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        #endregion
        #region Before3
        [HttpGet]
        [Route("MarketAction/MarketActionBefore3Search")]
        public APIResult MarketActionBefore3Search(string marketActionId)
        {
            try
            {
                MarketActionBefore3MainDto marketActionBefore3MainDto = new MarketActionBefore3MainDto();
                marketActionBefore3MainDto.BugetDetailSumAmt = marketActionService.MarketActionBefore3BugetSumAmtSearch(marketActionId);
                marketActionBefore3MainDto.BugetDetailListDto = marketActionService.MarketActionBefore3BugetDetailSearch(marketActionId);
                marketActionBefore3MainDto.DisplayModelList = marketActionService.MarketActionBefore3DisplayModelSearch(marketActionId);
                marketActionBefore3MainDto.TestDriverList = marketActionService.MarketActionBefore3TestDriverSearch(marketActionId);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionBefore3MainDto) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("MarketAction/MarketActionBefore3Save")]
        public APIResult MarketActionBefore3Save(UploadData upload)
        {
            try
            {
                MarketActionBefore3MainDto marketActionBefore3MainDto = CommonHelper.DecodeString<MarketActionBefore3MainDto>(upload.ListJson);

                // 先全部删除然后再保存

                marketActionService.MarketActionBefore3BugetDetailDelete(marketActionBefore3MainDto.MarketActionId.ToString());
                marketActionService.MarketActionBefore3DisplayModelDelete(marketActionBefore3MainDto.MarketActionId.ToString());
                marketActionService.MarketActionBefore3TestDriverDelete(marketActionBefore3MainDto.MarketActionId.ToString());
                foreach (MarketActionBefore3BugetDetail bugetDetail in marketActionBefore3MainDto.BugetDetailList)
                {
                    marketActionService.MarketActionBefore3BugetDetailSave(bugetDetail);
                }
                foreach (MarketActionBefore3DisplayModel displayModel in marketActionBefore3MainDto.DisplayModelList)
                {
                    marketActionService.MarketActionBefore3DisplayModelSave(displayModel);
                }
                foreach (MarketActionBefore3TestDriver testDriver in marketActionBefore3MainDto.TestDriverList)
                {
                    marketActionService.MarketActionBefore3TestDriverSave(testDriver);
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        //[HttpPost]
        //[Route("MarketAction/MarketActionBefore3TestDriverDelete")]
        //public APIResult MarketActionBefore3TestDriverDelete(UploadData upload)
        //{
        //    try
        //    {
        //        List<MarketActionBefore3TestDriver> list = CommonHelper.DecodeString<List<MarketActionBefore3TestDriver>>(upload.ListJson);
        //        foreach (MarketActionBefore3TestDriver testDriver in list)
        //        {
        //            marketActionService.MarketActionBefore3TestDriverDelete(testDriver.MarketActionId.ToString(), testDriver.SeqNO.ToString());
        //        }
        //        return new APIResult() { Status = true, Body = "" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new APIResult() { Status = false, Body = ex.Message.ToString() };
        //    }

        //}
        //[HttpPost]
        //[Route("MarketAction/MarketActionBefore3DisplayModelDelete")]
        //public APIResult MarketActionBefore3DisplayModelDelete(UploadData upload)
        //{
        //    try
        //    {
        //        List<MarketActionBefore3DisplayModel> list = CommonHelper.DecodeString<List<MarketActionBefore3DisplayModel>>(upload.ListJson);
        //        foreach (MarketActionBefore3DisplayModel displayModel in list)
        //        {
        //            marketActionService.MarketActionBefore3DisplayModelDelete(displayModel.MarketActionId.ToString(), displayModel.SeqNO.ToString());
        //        }
        //        return new APIResult() { Status = true, Body = "" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new APIResult() { Status = false, Body = ex.Message.ToString() };
        //    }

        //}
        //[HttpPost]
        //[Route("MarketAction/MarketActionBefore3BugetDetailDelete")]
        //public APIResult MarketActionBefore3BugetDetailDelete(UploadData upload)
        //{
        //    try
        //    {
        //        List<MarketActionBefore3BugetDetail> list = CommonHelper.DecodeString<List<MarketActionBefore3BugetDetail>>(upload.ListJson);
        //        foreach (MarketActionBefore3BugetDetail bugetDetail in list)
        //        {
        //            marketActionService.MarketActionBefore3BugetDetailDelete(bugetDetail.MarketActionId.ToString(), bugetDetail.SeqNO.ToString());
        //        }
        //        return new APIResult() { Status = true, Body = "" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new APIResult() { Status = false, Body = ex.Message.ToString() };
        //    }

        //}
        #endregion
        #region TheDays
        [HttpGet]
        [Route("MarketAction/MarketActionTheDayFileSearch")]
        public APIResult MarketActionTheDayFileSearch(string marketActionId)
        {
            try
            {
                List<MarketActionTheDayFile> marketActionTheDayFile = marketActionService.MarketActionTheDayFileSearch(marketActionId);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionTheDayFile) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("MarketAction/MarketActionTheDayFileSave")]
        public APIResult MarketActionTheDayFileSave(MarketActionTheDayFile marketActionTheDayFile)
        {
            try
            {

                marketActionService.MarketActionTheDayFileSave(marketActionTheDayFile);
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("MarketAction/MarketActionTheDayFileDelete")]
        public APIResult MarketActionTheDayFileDelete(UploadData upload)
        {
            try
            {
                List<MarketActionTheDayFile> list = CommonHelper.DecodeString<List<MarketActionTheDayFile>>(upload.ListJson);
                foreach (MarketActionTheDayFile theDayFile in list)
                {
                    marketActionService.MarketActionTheDayFileDelete(theDayFile.MarketActionId.ToString(), theDayFile.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region After2
        [HttpGet]
        [Route("MarketAction/MarketActionAfter2LeadsReportSearch")]
        public APIResult MarketActionAfter2LeadsReportSearch(string marketActionId)
        {
            try
            {
                List<MarketActionAfter2LeadsReportDto> marketAfterLeadsReportList = marketActionService.MarketActionAfter2LeadsReportSearch(marketActionId, "");
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketAfterLeadsReportList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("MarketAction/MarketActionAfter2LeadsReportImport")]
        public APIResult MarketActionAfter2LeadsReportImport(UploadData upload)
        {
            try
            {
                List<MarketActionAfter2LeadsReportDto> list = CommonHelper.DecodeString<List<MarketActionAfter2LeadsReportDto>>(upload.ListJson);
                foreach (MarketActionAfter2LeadsReportDto leadsReportDto in list)
                {
                    MarketActionAfter2LeadsReport leadsReport = new MarketActionAfter2LeadsReport();
                    leadsReport.BPNO = leadsReportDto.BPNO;
                    leadsReport.CustomerName = leadsReportDto.CustomerName;
                    if (leadsReportDto.DealCheckName == "是")
                    { leadsReport.DealCheck = true; }
                    else
                    {
                        leadsReport.DealCheck = false;
                    }
                    if (!string.IsNullOrEmpty(leadsReportDto.DealModelName))
                    {
                        List<HiddenCode> hiddenCodeList = masterService.HiddenCodeSearch("TargetModels", "", leadsReportDto.DealModelName);
                        if (hiddenCodeList != null && hiddenCodeList.Count > 0)
                        {
                            leadsReport.DealModel = hiddenCodeList[0].HiddenCodeId;
                        }
                    }
                    if (!string.IsNullOrEmpty(leadsReportDto.InterestedModelName))
                    {
                        List<HiddenCode> hiddenCodeList_Insterested = masterService.HiddenCodeSearch("TargetModels", "", leadsReportDto.InterestedModelName);
                        if (hiddenCodeList_Insterested != null && hiddenCodeList_Insterested.Count > 0)
                        {
                            leadsReport.InterestedModel = hiddenCodeList_Insterested[0].HiddenCodeId;
                        }
                    }
                    leadsReport.InUserId = leadsReportDto.InUserId;
                    if (leadsReportDto.LeadsCheckName == "是")
                    { leadsReport.LeadsCheck = true; }
                    else
                    {
                        leadsReport.LeadsCheck = false;
                    }
                    leadsReport.MarketActionId = leadsReportDto.MarketActionId;
                    leadsReport.ModifyDateTime = DateTime.Now;
                    leadsReport.ModifyUserId = leadsReportDto.ModifyUserId;
                    if (leadsReportDto.OwnerCheckName == "是")
                    { leadsReport.OwnerCheck = true; }
                    else
                    {
                        leadsReport.OwnerCheck = false;
                    }
                    leadsReport.TelNO = leadsReportDto.TelNO;
                    if (leadsReportDto.TestDriverCheckName == "是")
                    { leadsReport.TestDriverCheck = true; }
                    else
                    {
                        leadsReport.TestDriverCheck = false;
                    }
                    marketActionService.MarketActionAfter2LeadsReportSave(leadsReport);
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("MarketAction/MarketActionAfter2LeadsReportSave")]
        public APIResult MarketActionAfter2LeadsReportSave(MarketActionAfter2LeadsReport marketActionAfter2LeadsReport)
        {
            try
            {
                // List<MarketActionAfter2LeadsReport> list = CommonHelper.DecodeString<List<MarketActionAfter2LeadsReport>>(upload.ListJson);

                //foreach (MarketActionAfter2LeadsReport leadsReport in list)
                //{
                marketActionAfter2LeadsReport = marketActionService.MarketActionAfter2LeadsReportSave(marketActionAfter2LeadsReport);
                //}
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionAfter2LeadsReport) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("MarketAction/MarketActionAfter2LeadsReportDelete")]
        public APIResult MarketActionAfter2LeadsReportDelete(UploadData upload)
        {
            try
            {
                List<MarketActionAfter2LeadsReport> list = CommonHelper.DecodeString<List<MarketActionAfter2LeadsReport>>(upload.ListJson);
                foreach (MarketActionAfter2LeadsReport leadsReport in list)
                {
                    marketActionService.MarketActionAfter2LeadsReportDelete(leadsReport.MarketActionId.ToString(), leadsReport.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }

        [HttpGet]
        [Route("MarketAction/MarketActionAfter2LeadsReportExport")]
        public APIResult MarketActionAfter2LeadsReportExport(string marketActionId)
        {
            try
            {
                string filePath = excelDataService.MarketActionAfter2LeadsReportExport(marketActionId);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(new { FilePath = filePath }) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        #endregion
        #region After7
        [HttpGet]
        [Route("MarketAction/MarketActionAfter7Search")]
        public APIResult MarketActionAfter7Search(string marketActionId)
        {
            try
            {
                MarketActionAfter7MainDto marketActionAfter7MainDto = new MarketActionAfter7MainDto();
                List<MarketActionAfter7> marketActionAfter7List = marketActionService.MarketActionAfter7Search(marketActionId);
                if (marketActionAfter7List != null && marketActionAfter7List.Count > 0)
                {
                    marketActionAfter7MainDto.MarketActionAfter7 = marketActionAfter7List[0];
                }
                marketActionAfter7MainDto.ActualExpenseDto = marketActionService.MarketActionAfter7ActualExpenseSearch(marketActionId);
                marketActionAfter7MainDto.ActualProcess = marketActionService.MarketActionAfter7ActualProcessSearch(marketActionId);
                List<MarketActionLeadsCountDto> marketActionLeadsCountList = marketActionService.MarketActionLeadsCountSearch(marketActionId);// 需要和客户确认计算逻辑
                if (marketActionLeadsCountList != null && marketActionLeadsCountList.Count > 0)
                {
                    marketActionAfter7MainDto.LeadsCount = marketActionLeadsCountList[0];
                }
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionAfter7MainDto) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("MarketAction/MarketActionAfter7Save")]
        public APIResult MarketActionAfter7Save(UploadData upload)
        {
            try
            {
                MarketActionAfter7MainDto marketActionAfter7MainDto = CommonHelper.DecodeString<MarketActionAfter7MainDto>(upload.ListJson);

                marketActionAfter7MainDto.MarketActionAfter7.CarDisplayPic01 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.CarDisplayPic01);
                marketActionAfter7MainDto.MarketActionAfter7.CarDisplayPic02 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.CarDisplayPic02);
                marketActionAfter7MainDto.MarketActionAfter7.CarDisplayPic03 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.CarDisplayPic03);
                marketActionAfter7MainDto.MarketActionAfter7.CarDisplayPic04 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.CarDisplayPic04);

                marketActionAfter7MainDto.MarketActionAfter7.CustomerStaffModelPic01 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.CustomerStaffModelPic01);
                marketActionAfter7MainDto.MarketActionAfter7.CustomerStaffModelPic02 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.CustomerStaffModelPic02);
                marketActionAfter7MainDto.MarketActionAfter7.CustomerStaffModelPic03 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.CustomerStaffModelPic03);
                marketActionAfter7MainDto.MarketActionAfter7.CustomerStaffModelPic04 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.CustomerStaffModelPic04);

                marketActionAfter7MainDto.MarketActionAfter7.OnLineAdPic01 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.OnLineAdPic01);
                marketActionAfter7MainDto.MarketActionAfter7.OnLineAdPic02 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.OnLineAdPic02);
                marketActionAfter7MainDto.MarketActionAfter7.OnLineAdPic03 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.OnLineAdPic03);
                marketActionAfter7MainDto.MarketActionAfter7.OnLineAdPic04 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.OnLineAdPic04);

                marketActionAfter7MainDto.MarketActionAfter7.OthersPic01 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.OthersPic01);
                marketActionAfter7MainDto.MarketActionAfter7.OthersPic02 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.OthersPic02);
                marketActionAfter7MainDto.MarketActionAfter7.OthersPic03 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.OthersPic03);
                marketActionAfter7MainDto.MarketActionAfter7.OthersPic04 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.OthersPic04);

                marketActionAfter7MainDto.MarketActionAfter7.PlacePic01 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.PlacePic01);
                marketActionAfter7MainDto.MarketActionAfter7.PlacePic02 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.PlacePic02);
                marketActionAfter7MainDto.MarketActionAfter7.PlacePic03 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.PlacePic03);
                marketActionAfter7MainDto.MarketActionAfter7.PlacePic04 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.PlacePic04);

                marketActionAfter7MainDto.MarketActionAfter7.RegisterLiveShowPic01 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.RegisterLiveShowPic01);
                marketActionAfter7MainDto.MarketActionAfter7.RegisterLiveShowPic02 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.RegisterLiveShowPic02);
                marketActionAfter7MainDto.MarketActionAfter7.RegisterLiveShowPic03 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.RegisterLiveShowPic03);
                marketActionAfter7MainDto.MarketActionAfter7.RegisterLiveShowPic04 = UploadBase64Pic("", marketActionAfter7MainDto.MarketActionAfter7.RegisterLiveShowPic04);



                marketActionService.MarketActionAfter7Save(marketActionAfter7MainDto.MarketActionAfter7);

                // 先删除再全部保存
                marketActionService.MarketActionAfter7ActualExpenseDelete(marketActionAfter7MainDto.MarketActionId.ToString());
                marketActionService.MarketActionAfter7ActualProcessDelete(marketActionAfter7MainDto.MarketActionId.ToString());
                foreach (MarketActionAfter7ActualExpense expense in marketActionAfter7MainDto.ActualExpense)
                {
                    marketActionService.MarketActionAfter7ActualExpenseSave(expense);
                }
                foreach (MarketActionAfter7ActualProcess process in marketActionAfter7MainDto.ActualProcess)
                {
                    marketActionService.MarketActionAfter7ActualProcessSave(process);
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        //[HttpPost]
        //[Route("MarketAction/MarketActionAfter7ActualExpenseDelete")]
        //public APIResult MarketActionAfter7ActualExpenseDelete(UploadData upload)
        //{
        //    try
        //    {
        //        List<MarketActionAfter7ActualExpense> list = CommonHelper.DecodeString<List<MarketActionAfter7ActualExpense>>(upload.ListJson);
        //        foreach (MarketActionAfter7ActualExpense expense in list)
        //        {
        //            marketActionService.MarketActionAfter7ActualExpenseDelete(expense.MarketActionId.ToString(), expense.SeqNO.ToString());
        //        }
        //        return new APIResult() { Status = true, Body = "" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new APIResult() { Status = false, Body = ex.Message.ToString() };
        //    }

        //}
        //[HttpPost]
        //[Route("MarketAction/MarketActionAfter7ActualProcessDelete")]
        //public APIResult MarketActionAfter7ActualProcessDelete(UploadData upload)
        //{
        //    try
        //    {
        //        List<MarketActionAfter7ActualProcess> list = CommonHelper.DecodeString<List<MarketActionAfter7ActualProcess>>(upload.ListJson);
        //        foreach (MarketActionAfter7ActualProcess process in list)
        //        {
        //            marketActionService.MarketActionAfter7ActualProcessDelete(process.MarketActionId.ToString(), process.SeqNO.ToString());
        //        }
        //        return new APIResult() { Status = true, Body = "" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new APIResult() { Status = false, Body = ex.Message.ToString() };
        //    }

        //}
        #endregion
        #region 30 days after
        [HttpPost]
        [Route("MarketAction/MarketActionAfter30LeadsReportUpdate")]
        public APIResult MarketActionAfter30LeadsReportUpdate(MarketActionAfter30LeadsReportUpdate marketActionAfter30LeadsReportUpdate)
        {
            try
            {
                // MarketActionAfter30MainDto marketActionAfter30MainDto = CommonHelper.DecodeString<MarketActionAfter30MainDto>(upload.ListJson);
                marketActionService.MarketActionAfter30LeadsReportUpdate(marketActionAfter30LeadsReportUpdate);
                //foreach (MarketActionAfter2LeadsReport report in marketActionAfter30MainDto.LeadsReportList)
                //{
                //    marketActionService.MarketActionAfter2LeadsReportSave(report);

                //}
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region 3 months after
        [HttpGet]
        [Route("MarketAction/MarketActionAfter90FileSearch")]
        public APIResult MarketActionAfter90FileSearch(string marketActionId)
        {
            try
            {
                List<MarketActionAfter90File> marketActionAfter90File = marketActionService.MarketActionAfter90FileSearch(marketActionId);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionAfter90File) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("MarketAction/MarketActionAfter90FileSave")]
        public APIResult MarketActionAfter90FileSave(MarketActionAfter90File marketActionAfter90File)
        {
            try
            {

                marketActionService.MarketActionAfter90FileSave(marketActionAfter90File);
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("MarketAction/MarketActionAfter90FileDelete")]
        public APIResult MarketActionAfter90FileDelete(UploadData upload)
        {
            try
            {
                List<MarketActionAfter90File> list = CommonHelper.DecodeString<List<MarketActionAfter90File>>(upload.ListJson);
                foreach (MarketActionAfter90File file in list)
                {
                    marketActionService.MarketActionAfter90FileDelete(file.MarketActionId.ToString(), file.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region 总览
        [HttpGet]
        [Route("MarketAction/MarketActionStatusCountSearch")]
        public APIResult MarketActionStatusCountSearch(string year, string userId, string roleTypeCode)
        {
            try
            {

                List<MarketActionStatusCountDto> marketActionStatusCountListDto = marketActionService.MarketActionStatusCountSearch(year, accountService.GetShopByRole(userId, roleTypeCode));
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionStatusCountListDto) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        #endregion
        #endregion
        #region DMFItem
        [HttpGet]
        [Route("DMF/DMFItemSearch")]
        public APIResult DMFItemSearch(string dmfItemId, string dmfItemName, string dmfItemNameEn, bool? expenseAccountChk, bool? publishChk)
        {
            try
            {
                List<DMFItem> dmfItemList = dmfService.DMFItemSearch(dmfItemId, dmfItemName, dmfItemNameEn, expenseAccountChk, publishChk);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(dmfItemList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }

        [HttpPost]
        [Route("DMF/DMFItemSave")]
        public APIResult DMFItemSave(DMFItem dmfItem)
        {
            try
            {
                dmfItem = dmfService.DMFItemSave(dmfItem);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(dmfItem) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("DMF/DMFItemDelete")]
        public APIResult DMFItemDelete(UploadData upload)
        {
            try
            {
                List<DMFItem> list = CommonHelper.DecodeString<List<DMFItem>>(upload.ListJson);
                // 需要添加一个已经使用不能删除的验证。后期添加
                foreach (DMFItem dfmItem in list)
                {
                    dmfService.DMFItemDelete(dfmItem.DMFItemId.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region DMF
        [HttpGet]
        [Route("DMF/DMFSearch")]
        public APIResult DMFSearch(string shopId,string userId,string roleTypeCode)
        {
            try
            {
                List<Shop> roleTypeShopList = accountService.GetShopByRole(userId, roleTypeCode);
                List<DMFDto> dmfList = new List<DMFDto>();
                List<DMFDto> dmfListTemp = dmfService.DMFSearch(shopId);

                foreach (DMFDto dmfDto in dmfListTemp)
                {
                    foreach (Shop shop in roleTypeShopList)
                    {
                        if (dmfDto.ShopId == shop.ShopId)
                        {
                            dmfList.Add(dmfDto);

                        }
                    }
                }
                return new APIResult() { Status = true, Body = CommonHelper.Encode(dmfList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpGet]
        [Route("DMF/DMFQuarterSearch")]
        public APIResult DMFQuarterSearch(string shopId)
        {
            try
            {
                DMFQuarterMainDto dmfQuarterMainDto = new DMFQuarterMainDto();
                // 季度
                List<DMFDto> dmfList = dmfService.DMFSearch(shopId);
                List<DMFDto> dmfQuarterList = dmfService.DMFQuarterSearch(shopId);
                foreach (DMFDto quarter in dmfQuarterList)
                {
                    foreach (DMFDto dmf in dmfList)
                    {
                        if (quarter.ShopId == dmf.ShopId)
                        {
                            quarter.ActualAmt = dmf.ActualAmt;
                            quarter.DiffAmt = dmf.DiffAmt;
                        }
                    }
                }

                dmfQuarterMainDto.DMFQuarterList = dmfQuarterList;
                dmfQuarterMainDto.DMFDetailList = dmfService.DMFDetailSearch("", shopId, "", ""); ;
                return new APIResult() { Status = true, Body = CommonHelper.Encode(dmfQuarterMainDto) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        #endregion
        #region DMFDetail
        [HttpGet]
        [Route("DMF/DMFDetailSearch")]
        public APIResult DMFDetailSearch(string dmfDetailId, string shopId, string dmfItemId,string dmfItemName)
        {
            try
            {
                List<DMFDetailDto> dmfDetailList = dmfService.DMFDetailSearch(dmfDetailId, shopId, dmfItemId, dmfItemName);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(dmfDetailList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }

        [HttpPost]
        [Route("DMF/DMFDetailSave")]
        public APIResult DMFDetailSave(DMFDetail dmfDetail)
        {
            try
            {
                List<DMFDetailDto> detailList = dmfService.DMFDetailSearch("", dmfDetail.ShopId.ToString(), dmfDetail.DMFItemId.ToString(), "");
                if (detailList != null && detailList.Count != 0 && detailList[0].DMFDetailId != dmfDetail.DMFDetailId)
                {
                    return new APIResult() { Status = false, Body = "保存失败,同一经销商不能添加重复项目" };
                }
                List<DMFItem> itemList = dmfService.DMFItemSearch(dmfDetail.DMFItemId.ToString(), "", "", null, null);
                if (itemList != null && itemList.Count > 0 && itemList[0].ExpenseAccountChk == false)
                {
                    return new APIResult() { Status = false, Body = "保存失败,不能添加费用报销项目" };
                }
                dmfDetail = dmfService.DMFDetailSave(dmfDetail);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(dmfDetail) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("DMF/DMFDetailDelete")]
        public APIResult DMFDetailDelete(UploadData upload)
        {
            try
            {
                List<DMFDetail> list = CommonHelper.DecodeString<List<DMFDetail>>(upload.ListJson);
                foreach (DMFDetail dmfDetail in list)
                {
                    dmfService.DMFDetailDelete(dmfDetail.DMFDetailId.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpGet]
        [Route("DMF/DMFDetailExport")]
        public APIResult DMFDetailExport(string shopId, string dmfItemName)
        {
            try
            {
                string filePath = excelDataService.DMFDetailExport(shopId, dmfItemName);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(new { FilePath = filePath }) };

            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("DMF/DMFDetailImport")]
        public APIResult DMFDetailImport(UploadData upload)
        {
            try
            {
                List<DMFDetailDto> list = CommonHelper.DecodeString<List<DMFDetailDto>>(upload.ListJson);
                foreach (DMFDetailDto dmfDetailDto in list)
                {
                    DMFDetail dmfDetail = new DMFDetail();
                    List<ShopDto> shopList = masterService.ShopSearch("", "", dmfDetailDto.ShopName, "");
                    if (shopList != null && shopList.Count > 0)
                    {
                        dmfDetail.ShopId = shopList[0].ShopId;
                    }
                    List<DMFItem> dmfItemList = dmfService.DMFItemSearch("", dmfDetailDto.DMFItemName, "", null, null);
                    if (dmfItemList != null && dmfItemList.Count > 0)
                    {
                        dmfDetail.DMFItemId = dmfItemList[0].DMFItemId;
                    }
                    dmfDetail.AcutalAmt = dmfDetailDto.AcutalAmt;
                    dmfDetail.Budget = dmfDetailDto.Budget;
                    dmfDetail.InUserId = dmfDetailDto.InUserId;
                    dmfDetail.ModifyUserId = dmfDetailDto.ModifyUserId;
                    dmfDetail.Remark = dmfDetailDto.Remark;
                    dmfService.DMFDetailSave(dmfDetail);

                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region ExpenseAccount
        [HttpGet]
        [Route("DMF/ExpenseAccountSearch")]
        public APIResult ExpenseAccountSearch(string expenseAccountId, string shopId, string dmfItemId, string marketActionId, string userId, string roleTypeCode)
        {
            try
            {
                List<Shop> roleTypeShopList = accountService.GetShopByRole(userId, roleTypeCode);
                List<ExpenseAccountDto> expenseAccountList = new List<ExpenseAccountDto>();
                List<ExpenseAccountDto> expenseAccountListTemp = dmfService.ExpenseAccountSearch(expenseAccountId, shopId, dmfItemId, marketActionId);

                foreach (ExpenseAccountDto expenseAccountDto in expenseAccountListTemp)
                {
                    foreach (Shop shop in roleTypeShopList)
                    {
                        if (expenseAccountDto.ShopId == shop.ShopId)
                        {
                            expenseAccountList.Add(expenseAccountDto);

                        }
                    }
                }
                return new APIResult() { Status = true, Body = CommonHelper.Encode(expenseAccountList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }

        [HttpPost]
        [Route("DMF/ExpenseAccountSave")]
        public APIResult ExpenseAccountSave(ExpenseAccount expenseAccount)
        {
            try
            {
                expenseAccount = dmfService.ExpenseAccountSave(expenseAccount);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(expenseAccount) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("DMF/ExpenseAccountDelete")]
        public APIResult ExpenseAccountDelete(UploadData upload)
        {
            try
            {
                List<ExpenseAccount> list = CommonHelper.DecodeString<List<ExpenseAccount>>(upload.ListJson);
                // 需要确认什么条件下不能删除
                foreach (ExpenseAccount expenseAccount in list)
                {
                    dmfService.ExpenseAccountDelete(expenseAccount.ExpenseAccountId.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpGet]
        [Route("DMF/ExpenseAccountExport")]
        public APIResult ExpenseAccountExport(string shopId)
        {
            try
            {
                string filePath = excelDataService.ExpenseAccountExport(shopId);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(new { FilePath = filePath }) };

            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpGet]
        [Route("DMF/ExpenseAccountFileSearch")]
        public APIResult ExpenseAccountFileSearch(string expenseAccountId, string seqNO, string fileType)
        {
            try
            {
                List<ExpenseAccountFile> expenseAccountFileList = dmfService.ExpenseAccountFileSearch(expenseAccountId, seqNO, fileType);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(expenseAccountFileList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }

        [HttpPost]
        [Route("DMF/ExpenseAccountFileSave")]
        public APIResult ExpenseAccountFileSave(ExpenseAccountFile expenseAccount)
        {
            try
            {
                dmfService.ExpenseAccountFileSave(expenseAccount);
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("DMF/ExpenseAccountFileDelete")]
        public APIResult ExpenseAccountFileDelete(UploadData upload)
        {
            try
            {
                List<ExpenseAccountFile> list = CommonHelper.DecodeString<List<ExpenseAccountFile>>(upload.ListJson);
                foreach (ExpenseAccountFile expenseAccountFile in list)
                {
                    dmfService.ExpenseAccountFileDelete(expenseAccountFile.ExpenseAccountId.ToString(), expenseAccountFile.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region MonthSale
        [HttpGet]
        [Route("DMF/MonthSaleSearch")]
        public APIResult MonthSaleSearch(string monthSaleId, string shopId)
        {
            try
            {
                List<MonthSaleDto> monthSaleList = dmfService.MonthSaleSearch(monthSaleId, shopId, "");

                return new APIResult() { Status = true, Body = CommonHelper.Encode(monthSaleList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }

        [HttpPost]
        [Route("DMF/MonthSaleSave")]
        public APIResult MonthSaleSave(MonthSale monthSale)
        {
            try
            {
                List<MonthSaleDto> monthSaleList = dmfService.MonthSaleSearch("", monthSale.ShopId.ToString(), monthSale.YearMonth);
                if (monthSaleList != null && monthSaleList.Count != 0&& monthSaleList[0].MonthSaleId!= monthSale.MonthSaleId)
                {
                    return new APIResult() { Status = false, Body = "保存失败,同一经销商年月不能重复" };
                }
                dmfService.MonthSaleSave(monthSale);
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("DMF/MonthSaleImport")]
        public APIResult MonthSaleImport(UploadData upload)
        {
            try
            {
                List<MonthSaleDto> list = CommonHelper.DecodeString<List<MonthSaleDto>>(upload.ListJson);
                foreach (MonthSaleDto monthSaleDto in list)
                {
                    foreach (MonthSaleDto monthSaleDto1 in list)
                    {
                        if (monthSaleDto != monthSaleDto1 && monthSaleDto.ShopId == monthSaleDto1.ShopId && monthSaleDto.YearMonth == monthSaleDto1.YearMonth)
                        {
                            return new APIResult() { Status = false, Body = "导入失败,经销商名称及年月重复，请检查文件" };
                        }
                    }
                }
                foreach (MonthSaleDto monthSaleDto in list)
                {
                    List<ShopDto> shopList = masterService.ShopSearch("", "", monthSaleDto.ShopName, "");
                    if (shopList != null && shopList.Count > 0)
                    {
                        monthSaleDto.ShopId = shopList[0].ShopId;
                    }
                    List<MonthSaleDto> monthSaleList = dmfService.MonthSaleSearch("", monthSaleDto.ShopId.ToString(), monthSaleDto.YearMonth);
                    if (monthSaleList != null && monthSaleList.Count != 0&& monthSaleDto.MonthSaleId!= monthSaleList[0].MonthSaleId)
                    {
                        return new APIResult() { Status = false, Body = "导入失败,同一经销商年月不能重复，请检查文件" }; 
                    }
                }
                    foreach (MonthSaleDto monthSaleDto in list)
                {
                    MonthSale monthSale = new MonthSale();
                    List<ShopDto> shopList = masterService.ShopSearch("", "", monthSaleDto.ShopName, "");
                    if (shopList != null && shopList.Count > 0)
                    {
                        monthSale.ShopId = shopList[0].ShopId;
                    }
                    monthSale.ActualSaleAmt = monthSaleDto.ActualSaleAmt;
                    monthSale.ActualSaleCount = monthSaleDto.ActualSaleCount;
                    monthSale.InUserId = monthSaleDto.InUserId;
                    monthSale.ModifyUserId = monthSaleDto.ModifyUserId;
                    monthSale.YearMonth = monthSaleDto.YearMonth;
                    dmfService.MonthSaleSave(monthSale);

                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("DMF/MonthSaleDelete")]
        public APIResult MonthSaleDelete(UploadData upload)
        {
            try
            {
                List<MonthSale> list = CommonHelper.DecodeString<List<MonthSale>>(upload.ListJson);
                foreach (MonthSale monthSale in list)
                {
                    dmfService.MonthSaleDelete(monthSale.MonthSaleId.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion

    }
}
