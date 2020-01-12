using com.bentley.retailsupport.web.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace com.bentley.retailsupport.web.Controllers
{
    [AuthenAdminAttribute]
    public class BaseController : Controller
    {
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

        public void DownloadFile(string ossPath, string fileName)
        {
            byte[] fileContent = null;
            if (ossPath.StartsWith("Bentley"))
            {//oss
                HttpClient client = new HttpClient();
                string baseOss = WebConfigurationManager.AppSettings["ossPath"];
                Uri uri = new Uri(baseOss + ossPath);
                client.BaseAddress = uri;
                //添加请求的头文件
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-msdownload"));
                HttpResponseMessage message = client.GetAsync(uri).Result;
                //发送请求并接受返回的值
                fileContent = message.Content.ReadAsByteArrayAsync().Result;
            }
            else
            {
                string localFileDic = WebConfigurationManager.AppSettings["localFileDic"];
                string filePath = Server.MapPath("~") + localFileDic + ossPath;
                FileStream fs = new FileStream(filePath,FileMode.Open);
                if (fs == null) return;
                fileContent = new byte[(int)fs.Length];
                fs.Position = 0;
                fs.Read(fileContent, 0, (int)fs.Length);
                fs.Close();
            }

            Response.Clear();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            Response.AddHeader("content-type", "application/x-msdownload");
            Response.AddHeader("Content-Disposition", "attachment; ");
            Response.BinaryWrite(fileContent);
            Response.End();
        }
    }
}