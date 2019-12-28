using System.Web.Http;
using com.yrtech.InventoryAPI.Service;
using com.yrtech.InventoryAPI.Common;
using System.Collections.Generic;
using System;
using com.yrtech.InventoryAPI.Controllers;
using com.yrtech.InventoryAPI.DTO;
using System.Net.Http;
using com.yrtech.bentley.DAL;

namespace com.yrtech.SurveyAPI.Controllers
{

    [RoutePrefix("bentley/api")]
    public class AnswerController : BaseController
    {
        CommitFileService commitFileService = new CommitFileService();
        MasterService masterService = new MasterService();
        MarketActionService marketActionService = new MarketActionService();
        #region CommitFile
        [HttpGet]
        [Route("CommitFile/ShopCommitFileRecordStatusSearch")]
        public APIResult ShopCommitFileRecordStatusSearch(string year, string shopId)
        {
            try
            {
                ShopCommitFileRecordListDto shopCommitFileRecordList = new ShopCommitFileRecordListDto();
                shopCommitFileRecordList.ShopCommitFileRecordStatusList = commitFileService.ShopCommitFileRecordStatusSearch(year, shopId);
                shopCommitFileRecordList.ShopList = masterService.ShopSearch(shopId, "", "", "");
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
        public APIResult ShopCommitFileRecordDelete(UploadData upload)
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
        public APIResult MarketActionSearch(string actionName, string year, string month, string marketActionStatusCode, string shopId, string eventTypeId)
        {
            try
            {
                List<MarketActionDto> marketActionList = marketActionService.MarketActionSearch(actionName, year, month, marketActionStatusCode, shopId, eventTypeId);

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
        [Route("MarketAction/MarketActionBefore21ActivityProcessSave")]
        public APIResult MarketActionBefore21ActivityProcessSave(MarketActionBefore21MainDto marketActionBefore21MainDto)
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
                marketActionService.MarketActionBefore21Save(marketActionBefore21MainDto.MarketActionBefore21);
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
        [HttpPost]
        [Route("MarketAction/MarketActionBefore21ActivityProcessDelete")]
        public APIResult MarketActionBefore21ActivityProcessDelete(UploadData upload)
        {
            try
            {
                List<MarketActionBefore21ActivityProcess> list = CommonHelper.DecodeString<List<MarketActionBefore21ActivityProcess>>(upload.ListJson);
                foreach (MarketActionBefore21ActivityProcess process in list)
                {
                    marketActionService.MarketActionBefore21ActivityProcessDelete(process.MarketActionId.ToString(),process.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #endregion


    }
}
