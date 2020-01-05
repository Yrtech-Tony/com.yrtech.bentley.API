﻿using System.Web.Http;
using com.yrtech.InventoryAPI.Service;
using com.yrtech.InventoryAPI.Common;
using System.Collections.Generic;
using System;
using com.yrtech.InventoryAPI.Controllers;
using com.yrtech.InventoryAPI.DTO;
using System.Net.Http;
using com.yrtech.bentley.DAL;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;

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
        public APIResult MarketActionSearch(string actionName, string year, string month, string marketActionStatusCode, string shopId, string eventTypeId, string userId, string roleTypeCode)
        {
            try
            {

                List<MarketActionDto> marketActionListTemp = marketActionService.MarketActionSearch(actionName, year, month, marketActionStatusCode, shopId, eventTypeId);
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
                ExcelDataService excelDataService = new ExcelDataService();
                string filePath = excelDataService.MarketActionAfter2LeadsReportExport(year);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(new { FilePath= filePath })  };
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
                SendEmail("71443365@qq.com", "mou.junsheng@eland.co.kr", "主视觉审批", "主视觉审批", "", "http://yrtech.oss-cn-beijing-internal.aliyuncs.com/AODISatisfaction/%E5%A5%A5%E8%BF%AA%E6%BB%A1%E6%84%8F%E5%BA%A6%E6%8F%90%E5%8D%87%E4%B8%8A%E6%B5%B7%E4%B8%80%E6%B1%BD%E6%B2%AA%E5%A5%A5/A01-1/%E5%9B%9E%E6%89%A7%E9%82%AE%E4%BB%B6%E6%8B%8D%E7%85%A7.jpg");
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpGet]
        [Route("MarketAction/KeyVisionSendEmailToShop")]
        public APIResult KeyVisionSendEmailToShop(string marketActionId)
        {
            try
            {
                SendEmail("71443365@qq.com", "mou.junsheng@eland.co.kr", "主视觉审批修改意见", "主视觉审批修改意见", "", "http://yrtech.oss-cn-beijing-internal.aliyuncs.com/AODISatisfaction/%E5%A5%A5%E8%BF%AA%E6%BB%A1%E6%84%8F%E5%BA%A6%E6%8F%90%E5%8D%87%E4%B8%8A%E6%B5%B7%E4%B8%80%E6%B1%BD%E6%B2%AA%E5%A5%A5/A01-1/%E5%9B%9E%E6%89%A7%E9%82%AE%E4%BB%B6%E6%8B%8D%E7%85%A7.jpg");
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
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
                    List<HiddenCode> hiddenCodeList = masterService.HiddenCodeSearch("TargetModels", "", leadsReportDto.DealModelName);
                    if (hiddenCodeList != null && hiddenCodeList.Count > 0)
                    {
                        leadsReport.DealModel = hiddenCodeList[0].HiddenCodeId;
                    }
                    List<HiddenCode> hiddenCodeList_Insterested = masterService.HiddenCodeSearch("TargetModels", "", leadsReportDto.DealModelName);
                    if (hiddenCodeList_Insterested != null && hiddenCodeList_Insterested.Count > 0)
                    {
                        leadsReport.InterestedModel = hiddenCodeList_Insterested[0].HiddenCodeId;
                    }
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
                ExcelDataService excelDataService = new ExcelDataService();
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
        public APIResult MarketActionStatusCountSearch(string year)
        {
            try
            {
                List<MarketActionStatusCountDto> marketActionStatusCountListDto = marketActionService.MarketActionStatusCountSearch(year);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionStatusCountListDto) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        #endregion
        #endregion
        #region DMF
        [HttpGet]
        [Route("DMF/DMFItemSearch")]
        public APIResult DMFItemSearch(string DMFItemId, string DMFItemName, string DMFItemNameEn)
        {
            try
            {
                List<DMFItem> dmfItemList = dmfService.DMFItemSearch(DMFItemId, DMFItemName, DMFItemNameEn);

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
                dmfService.DMFItemSave(dmfItem);
                return new APIResult() { Status = true, Body = "" };
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


    }
}
