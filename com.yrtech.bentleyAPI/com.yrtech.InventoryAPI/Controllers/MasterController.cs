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
                masterService.ShopSave(shop);
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
        public APIResult EventTypeSearch(string eventTypeId, string eventTypeName, string eventTypeNameEn)
        {
            try
            {
                List<EventTypeDto> eventTypeList = masterService.EventTypeSearch(eventTypeId,eventTypeName,eventTypeNameEn);

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
                masterService.EventTypeSave(eventType);
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
                List<HiddenCode> hiddenCodeList = masterService.HiddenCodeSearch(hiddenCodeGroup, hiddenCode);

                return new APIResult() { Status = true, Body = CommonHelper.Encode(hiddenCodeList) };
            }
            catch (Exception ex)
            {
                return new APIResult() { Status = false, Body = ex.Message.ToString() };
            }
        }
        #endregion

    }
}
