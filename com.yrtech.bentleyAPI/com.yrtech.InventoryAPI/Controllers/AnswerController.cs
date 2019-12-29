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
                    marketActionService.MarketActionBefore21ActivityProcessDelete(process.MarketActionId.ToString(), process.SeqNO.ToString());
                }
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
                marketActionBefore3MainDto.BugetDetailList = marketActionService.MarketActionBefore3BugetDetailSearch(marketActionId);
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
               MarketActionBefore3MainDto marketActionBefore3MainDto = CommonHelper.DecodeString<MarketActionBefore3MainDto> (upload.ListJson);

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
        [HttpPost]
        [Route("MarketAction/MarketActionBefore3TestDriverDelete")]
        public APIResult MarketActionBefore3TestDriverDelete(UploadData upload)
        {
            try
            {
                List<MarketActionBefore3TestDriver> list = CommonHelper.DecodeString<List<MarketActionBefore3TestDriver>>(upload.ListJson);
                foreach (MarketActionBefore3TestDriver testDriver in list)
                {
                    marketActionService.MarketActionBefore3TestDriverDelete(testDriver.MarketActionId.ToString(), testDriver.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("MarketAction/MarketActionBefore3DisplayModelDelete")]
        public APIResult MarketActionBefore3DisplayModelDelete(UploadData upload)
        {
            try
            {
                List<MarketActionBefore3DisplayModel> list = CommonHelper.DecodeString<List<MarketActionBefore3DisplayModel>>(upload.ListJson);
                foreach (MarketActionBefore3DisplayModel displayModel in list)
                {
                    marketActionService.MarketActionBefore3DisplayModelDelete(displayModel.MarketActionId.ToString(), displayModel.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("MarketAction/MarketActionBefore3BugetDetailDelete")]
        public APIResult MarketActionBefore3BugetDetailDelete(UploadData upload)
        {
            try
            {
                List<MarketActionBefore3BugetDetail> list = CommonHelper.DecodeString<List<MarketActionBefore3BugetDetail>>(upload.ListJson);
                foreach (MarketActionBefore3BugetDetail bugetDetail in list)
                {
                    marketActionService.MarketActionBefore3BugetDetailDelete(bugetDetail.MarketActionId.ToString(), bugetDetail.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
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
                List<MarketActionAfter2LeadsReportDto> marketActionTheDayFile = marketActionService.MarketActionAfter2LeadsReportSearch(marketActionId);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(marketActionTheDayFile) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("MarketAction/MarketActionAfter2LeadsReportSave")]
        public APIResult MarketActionAfter2LeadsReportSave(UploadData upload)
        {
            try
            {
                List<MarketActionAfter2LeadsReport> list = CommonHelper.DecodeString<List<MarketActionAfter2LeadsReport>>(upload.ListJson);
                foreach (MarketActionAfter2LeadsReport leadsReport in list)
                {
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
        [Route("MarketAction/MarketActionAfter2LeadsReportDownload")]
        public APIResult MarketActionAfter2LeadsReportDownload(string marketActionId)
        {
            try
            {
                ExcelDataController excelData = new ExcelDataController();
                excelData.MarketActionAfter2LeadsReportDownload(marketActionId);
                
                return new APIResult() { Status = true, Body = "" };
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
                marketActionAfter7MainDto.ActualExpense = marketActionService.MarketActionAfter7ActualExpenseSearch(marketActionId);
                marketActionAfter7MainDto.ActualProcess = marketActionService.MarketActionAfter7ActualProcessSearch(marketActionId);
                List< MarketActionLeadsCountDto> marketActionLeadsCountList= marketActionService.MarketActionLeadsCountSearch(marketActionId);// 需要和客户确认计算逻辑
                if (marketActionLeadsCountList!=null&& marketActionLeadsCountList.Count>0)
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
        [HttpPost]
        [Route("MarketAction/MarketActionAfter7ActualExpenseDelete")]
        public APIResult MarketActionAfter7ActualExpenseDelete(UploadData upload)
        {
            try
            {
                List<MarketActionAfter7ActualExpense> list = CommonHelper.DecodeString<List<MarketActionAfter7ActualExpense>>(upload.ListJson);
                foreach (MarketActionAfter7ActualExpense expense in list)
                {
                    marketActionService.MarketActionAfter7ActualExpenseDelete(expense.MarketActionId.ToString(), expense.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("MarketAction/MarketActionAfter7ActualProcessDelete")]
        public APIResult MarketActionAfter7ActualProcessDelete(UploadData upload)
        {
            try
            {
                List<MarketActionAfter7ActualProcess> list = CommonHelper.DecodeString<List<MarketActionAfter7ActualProcess>>(upload.ListJson);
                foreach (MarketActionAfter7ActualProcess process in list)
                {
                    marketActionService.MarketActionAfter7ActualProcessDelete(process.MarketActionId.ToString(), process.SeqNO.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region 30 days after
        [HttpPost]
        [Route("MarketAction/MarketActionAfter30LeadsReportUpdate")]
        public APIResult MarketActionAfter30LeadsReportUpdate(UploadData upload)
        {
            try
            {
                MarketActionAfter30MainDto marketActionAfter30MainDto = CommonHelper.DecodeString<MarketActionAfter30MainDto>(upload.ListJson);
                marketActionService.MarketActionAfter30LeadsReportUpdate(marketActionAfter30MainDto.MarketActionAfter30LeadsReportUpdate);
                foreach (MarketActionAfter2LeadsReport report in marketActionAfter30MainDto.LeadsReportList)
                {
                    marketActionService.MarketActionAfter2LeadsReportSave(report);

                }
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
        #endregion


    }
}
