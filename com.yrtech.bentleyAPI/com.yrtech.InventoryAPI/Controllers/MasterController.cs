using System.Web.Http;
using com.yrtech.InventoryAPI.Service;
using com.yrtech.InventoryAPI.Common;
using System.Collections.Generic;
using System;
using com.yrtech.bentley;
using com.yrtech.InventoryAPI.DTO;
using com.yrtech.bentley.DAL;

namespace com.yrtech.InventoryAPI.Controllers
{
    [RoutePrefix("bentley/api")]
    public class MasterController : BaseController
    {
        MasterService masterService = new MasterService();
        #region Shop
        [HttpGet]
        [Route("Master/ShopSearch")]
        public APIResult ShopSearch(string shopId, string shopCode, string shopName, string shopNameEn)
        {
            try
            {
                List<ShopDto> shopList = masterService.ShopSearch(shopId,shopCode,shopName,shopNameEn);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(shopList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("Master/ShopSave")]
        public APIResult ShopSave(Shop shop)
        {
            try
            {
                shop = masterService.ShopSave(shop);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(shop) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("Master/ShopDelete")]
        public APIResult ShopDelete(UploadData upload)
        {
            try
            {
                List<Shop> list = CommonHelper.DecodeString<List<Shop>>(upload.ListJson);
                // 需要添加一个已经使用不能删除的验证。后期添加
                foreach (Shop shop in list)
                {
                    masterService.ShopDelete(shop.ShopId.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region Shop
        [HttpGet]
        [Route("Master/AreaSearch")]
        public APIResult AreaSearch(string areaId, string areaName, string areaNameEn)
        {
            try
            {
                List<Area> areaList = masterService.AreaSearch(areaId, areaName, areaNameEn);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(areaList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("Master/AreaSave")]
        public APIResult AreaSave(Area area)
        {
            try
            {
                area = masterService.AreaSave(area);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(area) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("Master/AreaDelete")]
        public APIResult AreaDelete(UploadData upload)
        {
            try
            {
                List<Area> list = CommonHelper.DecodeString<List<Area>>(upload.ListJson);
                // 需要添加一个已经使用不能删除的验证。后期添加
                foreach (Area area in list)
                {
                    masterService.AreaDelete(area.AreaId.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region EventType
        [HttpGet]
        [Route("Master/EventTypeSearch")]
        public APIResult EventTypeSearch(string eventTypeId, string eventTypeName, string eventTypeNameEn,bool? showStatus)
        {
            try
            {
                List<EventTypeDto> eventTypeList = masterService.EventTypeSearch(eventTypeId,eventTypeName,eventTypeNameEn, showStatus);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(eventTypeList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("Master/EventTypeSave")]
        public APIResult EventTypeSave(EventType eventType)
        {
            try
            {
                eventType = masterService.EventTypeSave(eventType);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(eventType) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("Master/EventTypeDelete")]
        public APIResult EventTypeDelete(UploadData upload)
        {
            try
            {
                List<EventType> list = CommonHelper.DecodeString<List<EventType>>(upload.ListJson);
                // 需要添加一个已经使用不能删除的验证。后期添加
                foreach (EventType eventType in list)
                {
                    masterService.EventTypeDelete(eventType.EventTypeId.ToString());
                }
                return new APIResult() { Status = true, Body = "" };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        #endregion
        #region HiddenCode
        [HttpGet]
        [Route("Master/HiddenCodeSearch")]
        public APIResult HiddenCodeSearch(string hiddenCodeGroup, string hiddenCode)
        {
            try
            {
                List<HiddenCode> hiddenCodeList = masterService.HiddenCodeSearch(hiddenCodeGroup, hiddenCode,"");

                return new APIResult() { Status = true, Body = CommonHelper.Encode(hiddenCodeList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        #endregion
        #region UserInfo
        [HttpGet]
        [Route("Master/UserInfoSearch")]
        public APIResult UserInfoSearch(string userId, string accountId, string accountName)
        {
            try
            {
                List<UserInfoDto> userInfoList = masterService.UserInfoSearch(userId,accountId,accountName);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(userInfoList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        [HttpPost]
        [Route("Master/UserInfoSave")]
        public APIResult UserInfoSave(UserInfo userInfo)
        {
            try
            {
                userInfo = masterService.UserInfoSave(userInfo);
                return new APIResult() { Status = true, Body = CommonHelper.Encode(userInfo) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }

        }
        [HttpPost]
        [Route("Master/UserInfoDelete")]
        public APIResult UserInfoDelete(UploadData upload)
        {
            try
            {
                List<UserInfo> list = CommonHelper.DecodeString<List<UserInfo>>(upload.ListJson);
                // 需要添加一个已经使用不能删除的验证。后期添加
                foreach (UserInfo userInfo in list)
                {
                    masterService.UserInfoDelete(userInfo.UserId.ToString());
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
