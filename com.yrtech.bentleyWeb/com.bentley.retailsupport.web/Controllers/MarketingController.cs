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
        public ActionResult After2Days(string Id)
        {
            ViewBag.Id = Id;
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
                sheet.GetCell("I" + (rowIndex + 2)).Value = item.InterestedModel;
                //是否成交
                sheet.GetCell("J" + (rowIndex + 2)).Value = item.DealCheckName;
                // 成交车型
                sheet.GetCell("K" + (rowIndex + 2)).Value = item.DealModel;
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
        #endregion
    }
}