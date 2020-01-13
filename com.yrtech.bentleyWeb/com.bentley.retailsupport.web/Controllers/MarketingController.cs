using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.yrtech.InventoryAPI.DTO;
using System.Web.Configuration;
using System.Net.Http;
using com.bentley.retailsupport.web.Common;
using Infragistics.Documents.Excel;
using System.IO;

namespace com.bentley.retailsupport.web.Controllers
{
    public class MarketingController : BaseController
    {
        //
        // GET: /Marketing/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult Before3Weeks(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult Before3Days(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult TheDays(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult After2Days(string Id, string ShopName, string ActionName)
        {
            ViewBag.Id = Id;
            ViewBag.ShopName = ShopName;
            ViewBag.ActionName = ActionName;
            return View();
        }
        public ActionResult After7Days(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult After1Months(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult After3Months(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        #region MarketAction
        // 导出所有线索报告
        public void MarketActionAllLeadsReportExport(string year)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://" + WebConfigurationManager.AppSettings["APIHost"]);
            client.BaseAddress = uri;
            //添加请求的头文件
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //发送请求并接受返回的值
            string leadsReportApi = string.Format("bentley/api/MarketAction/MarketActionAfter2LeadsReportSearch?year={0}", year);
            HttpResponseMessage message = client.GetAsync(leadsReportApi).Result;
            string json = message.Content.ReadAsStringAsync().Result;
            APIResult result = CommonHelper.DecodeString<APIResult>(json);
            List<MarketActionAfter2LeadsReportDto> list = new List<MarketActionAfter2LeadsReportDto>();
            if (result.Status)
            {
                list = CommonHelper.DecodeString<List<MarketActionAfter2LeadsReportDto>>(result.Body);
            }
            Workbook book = Workbook.Load(Server.MapPath("~") + @"Content\Excel\" + "LeadsReportAll.xlsx", false);
            //填充数据
            Worksheet sheet = book.Worksheets[0];
            int rowIndex = 1;

            foreach (MarketActionAfter2LeadsReportDto item in list)
            {
                //经销商名称
                sheet.GetCell("A" + (rowIndex + 2)).Value = item.ShopName;
                //活动名称
                sheet.GetCell("B" + (rowIndex + 2)).Value = item.ActionName;
                //客户姓名
                sheet.GetCell("C" + (rowIndex + 2)).Value = item.CustomerName;
                //联系方式
                sheet.GetCell("D" + (rowIndex + 2)).Value = item.TelNO;
                //BPNO
                sheet.GetCell("E" + (rowIndex + 2)).Value = item.BPNO;
                //是否车主
                sheet.GetCell("F" + (rowIndex + 2)).Value = item.OwnerCheckName;
                // 是否试驾
                sheet.GetCell("G" + (rowIndex + 2)).Value = item.TestDriverCheckName;
                // 是否线索
                sheet.GetCell("H" + (rowIndex + 21)).Value = item.LeadsCheckName;
                //感兴趣车型
                sheet.GetCell("I" + (rowIndex + 2)).Value = item.InterestedModelName;
                //是否成交
                sheet.GetCell("J" + (rowIndex + 2)).Value = item.DealCheckName;
                // 成交车型
                sheet.GetCell("K" + (rowIndex + 2)).Value = item.DealModelName;
                rowIndex++;
            }

            //保存excel文件
            string fileName = "线索报告" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            string dirPath = Server.MapPath("~") + @"\Temp\";
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath + fileName;
            book.Save(filePath);
            DownloadExcel(fileName, filePath, true);
        }
        #region 2 days after leads report
        //导出线索报告
        public void MarketActionAfter2LeadsReportExport(string marketActionId)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://" + WebConfigurationManager.AppSettings["APIHost"]);
            client.BaseAddress = uri;
            //添加请求的头文件
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //发送请求并接受返回的值
            string leadsReportApi = string.Format("bentley/api/MarketAction/MarketActionAfter2LeadsReportSearch?marketActionId={0}", marketActionId);
            HttpResponseMessage message = client.GetAsync(leadsReportApi).Result;
            string json = message.Content.ReadAsStringAsync().Result;
            APIResult result = CommonHelper.DecodeString<APIResult>(json);
            List<MarketActionAfter2LeadsReportDto> list = new List<MarketActionAfter2LeadsReportDto>();
            if (result.Status)
            {
                list = CommonHelper.DecodeString<List<MarketActionAfter2LeadsReportDto>>(result.Body);
            }
            
            Workbook book = Workbook.Load(Server.MapPath("~") + @"Content\Excel\" + "LeadsReport.xlsx", false);
            //填充数据
            Worksheet sheet = book.Worksheets[0];
            int rowIndex = 1;

            foreach (MarketActionAfter2LeadsReportDto item in list)
            {
                //客户姓名
                sheet.GetCell("D" + (rowIndex + 1)).Value = item.CustomerName;
                //BPNO
                sheet.GetCell("E" + (rowIndex + 1)).Value = item.BPNO;
                //是否车主
                sheet.GetCell("F" + (rowIndex + 1)).Value = item.OwnerCheckName;
                // 是否试驾
                sheet.GetCell("G" + (rowIndex + 1)).Value = item.TestDriverCheckName;
                // 是否线索
                sheet.GetCell("H" + (rowIndex + 1)).Value = item.LeadsCheckName;
                //感兴趣车型
                sheet.GetCell("I" + (rowIndex + 1)).Value = item.InterestedModel;
                //是否成交
                sheet.GetCell("J" + (rowIndex + 1)).Value = item.DealCheckName;
                // 成交车型
                sheet.GetCell("K" + (rowIndex + 1)).Value = item.DealModel;
                rowIndex++;
            }

            //保存excel文件
            string fileName = "线索报告" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            string dirPath = Server.MapPath("~") + @"\Temp\";
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath + fileName;
            book.Save(filePath);
            DownloadExcel(fileName, filePath, true);
        }
        #endregion
        #region MarketAction Export

        
        public void MarketActionExport(string actionName, string year, string month, string marketActionStatusCode, string shopId, string eventTypeId, bool? expenseAccountChk, string userId, string roleTypeCode)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://" + WebConfigurationManager.AppSettings["APIHost"]);
            client.BaseAddress = uri;
            //添加请求的头文件
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //发送请求并接受返回的值
            string marketActionApi = string.Format(@"bentley/api/MarketAction/MarketActionExportSearch?actionName={0},year={1},month={2},marketActionStatusCode={3},shopId={4},eventTypeId={5},expenseAccountChk={6},userId={7},roleTypeCode={8}",
                actionName, year, month, marketActionStatusCode, shopId, eventTypeId, expenseAccountChk, userId, roleTypeCode);
            HttpResponseMessage message = client.GetAsync(marketActionApi).Result;
            string json = message.Content.ReadAsStringAsync().Result;
            APIResult result = CommonHelper.DecodeString<APIResult>(json);
            List<MarketActionExportDto> list = new List<MarketActionExportDto>();
            if (result.Status)
            {
                list = CommonHelper.DecodeString<List<MarketActionExportDto>>(result.Body);
            }
            Workbook book = Workbook.Load(Server.MapPath("~") + @"Content\Excel\" + "MarketAction.xlsx", false);
            //填充数据
            Worksheet sheet = book.Worksheets[0];
            int rowIndex = 3;

            foreach (MarketActionExportDto item in list)
            {
                //ID
                sheet.GetCell("A" + (rowIndex + 1)).Value = item.MarketActionId;
                //经销商名称
                sheet.GetCell("B" + (rowIndex + 1)).Value = item.ShopName;
                //活动状态
                sheet.GetCell("C" + (rowIndex + 1)).Value = item.MarketActionStatusName;
                // 活动名称
                sheet.GetCell("D" + (rowIndex + 1)).Value = item.ActionName;
                // 活动类型
                sheet.GetCell("E" + (rowIndex + 1)).Value = item.EventTypeName;
                //开始日期
                sheet.GetCell("F" + (rowIndex + 1)).Value = item.StartDate;
                //结束日期
                sheet.GetCell("G" + (rowIndex + 1)).Value = item.EndDate;
                // 主推车型
                sheet.GetCell("H" + (rowIndex + 1)).Value = item.MarketActionTargetModelName;
                if (item.MarketActionBefore21 != null)
                {
                    // 活动预算
                    sheet.GetCell("I" + (rowIndex + 1)).Value = item.MarketActionBefore21.Budget;
                    // 预计参加（车主)
                    sheet.GetCell("J" + (rowIndex + 1)).Value = item.MarketActionBefore21.TargetParticipationPCCount;
                    // 预计参加（潜客）
                    sheet.GetCell("K" + (rowIndex + 1)).Value = item.MarketActionBefore21.TargetParticipationPCCount;
                    // 预期线索（车主）
                    sheet.GetCell("L" + (rowIndex + 1)).Value = item.MarketActionBefore21.TargetLeadsOwnerCount;
                    // 预期线索（潜客）
                    sheet.GetCell("M" + (rowIndex + 1)).Value = item.MarketActionBefore21.TargetLeadsPCCount;
                    // 预计试驾（车主)
                    sheet.GetCell("N" + (rowIndex + 1)).Value = item.MarketActionBefore21.TargetTestDriveOwnerCount;
                    // 预计试驾（潜客）
                    sheet.GetCell("O" + (rowIndex + 1)).Value = item.MarketActionBefore21.TargetTestDrivePCCount;
                    // 预计订单（车主）
                    sheet.GetCell("P" + (rowIndex + 1)).Value = item.MarketActionBefore21.TargetOrdersOwnerCount;
                    // 预计订单（潜客）
                    sheet.GetCell("Q" + (rowIndex + 1)).Value = item.MarketActionBefore21.TargetOrdersPCCount;
                }
                
                // 实际花费
                sheet.GetCell("R" + (rowIndex + 1)).Value = item.ActualExpenseSum;
                if (item.MarketActionAfter7 != null)
                {
                    // 到场人数（车主）
                    sheet.GetCell("S" + (rowIndex + 1)).Value = item.MarketActionAfter7.AttendenceOwnerCount;
                    // 预计试驾（潜客）
                    sheet.GetCell("T" + (rowIndex + 1)).Value = item.MarketActionAfter7.AttendencePCCount;
                }
                if (item.LeadsCount != null)
                {
                    // 线索数量（车主）
                    sheet.GetCell("U" + (rowIndex + 1)).Value = item.LeadsCount.LeadOwnerCount;
                    // 线索数量（潜客）
                    sheet.GetCell("V" + (rowIndex + 1)).Value = item.LeadsCount.LeadPCCount;
                    // 试驾人数（车主）
                    sheet.GetCell("W" + (rowIndex + 1)).Value = item.LeadsCount.TestDriverOwnerCount;
                    // 试驾人数（潜客）
                    sheet.GetCell("X" + (rowIndex + 1)).Value = item.LeadsCount.TestDriverPCCount;
                    // 实际订单（车主）
                    sheet.GetCell("Y" + (rowIndex + 1)).Value = item.LeadsCount.ActualOrderOwnerCount;
                    // 实际订单（潜客）
                    sheet.GetCell("Y" + (rowIndex + 1)).Value = item.LeadsCount.ActualOrderPCCount;
                }
                rowIndex++;
            }

            //保存excel文件
            string fileName = "市场活动" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            string dirPath = Server.MapPath("~") + @"\Temp\";
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath + fileName;
            book.Save(filePath);
            DownloadExcel(fileName, filePath, true);
        }
        #endregion
        #endregion
    }
}