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
        #region CommitFile
        [HttpGet]
        [Route("CommitFile/ShopCommitFileRecordStatusSearch")]
        public APIResult ShopCommitFileRecordStatusSearch(string year)
        {
            try
            {
                ShopCommitFileRecordListDto shopCommitFileRecordList = new ShopCommitFileRecordListDto();
                shopCommitFileRecordList.ShopCommitFileRecordStatusList = commitFileService.ShopCommitFileRecordStatusSearch(year);
                shopCommitFileRecordList.ShopList = masterService.ShopSearch("", "", "", "");
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
        public APIResult ShopCommitFileRecordSearch(string shopId,string fileId)
        {
            try
            {
                List<ShopCommitFileRecord> shopCommitFileRecordList = commitFileService.ShopCommitFileRecordSearch(shopId,fileId);

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
                List<ShopCommitFileRecord> list = CommonHelper.DecodeString<List<ShopCommitFileRecord>>(upload.AnswerListJson);
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


    }
}
