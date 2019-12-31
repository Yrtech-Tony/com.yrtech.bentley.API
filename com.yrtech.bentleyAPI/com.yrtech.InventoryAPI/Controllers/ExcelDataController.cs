using com.yrtech.InventoryAPI.Common;
using com.yrtech.InventoryAPI.DTO;
using com.yrtech.InventoryAPI.Service;
using Infragistics.Documents.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace com.yrtech.InventoryAPI.Controllers
{
    public class ExcelDataController : Controller
    {
        MarketActionService mardetActionService = new MarketActionService();
        //MasterService masterService = new MasterService();
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
        public void DownloadExcel(string excelName, string filePath, bool isDeleteAfterDownload = false)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            if (stream == null) return;
            if (string.IsNullOrEmpty(excelName))
            {
                excelName = "excel" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            }
            byte[] bytes = new byte[(int)stream.Length];
            stream.Position = 0;
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            Response.Clear();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            Response.AddHeader("content-type", "application/x-msdownload");
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(excelName, Encoding.GetEncoding("UTF-8")));
            Response.BinaryWrite(bytes);
            Response.End();
            if (isDeleteAfterDownload)
            {
                System.IO.File.Delete(filePath);
            }
        }
        public void DownLoadAnswerImportExcel()
        {
            string fileName = "easyPhotoImport";

            string dirPath = Server.MapPath("~") + @"\Content\Excel\";
            string dirPath_Copy = Server.MapPath("~") + @"\Temp\";
            System.IO.File.Copy(dirPath + fileName + ".xls", dirPath_Copy + fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            DirectoryInfo dir = new DirectoryInfo(dirPath_Copy);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath_Copy + dirPath_Copy + fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            DownloadExcel(fileName+".xls", filePath, true);
        }

        #region MarketAction
        // 导出所有线索报告
        public void MarketActionAllLeadsReportExport(string year)
        {
            List<MarketActionAfter2LeadsReportDto> list = mardetActionService.MarketActionAfter2LeadsReportSearch("",year);
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
            List<MarketActionAfter2LeadsReportDto> list = mardetActionService.MarketActionAfter2LeadsReportSearch(marketActionId,"");
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