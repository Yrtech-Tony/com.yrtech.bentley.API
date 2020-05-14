﻿using com.yrtech.bentley.DAL;
using com.yrtech.InventoryAPI.Common;
using com.yrtech.InventoryAPI.DTO;
using Infragistics.Documents.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace com.yrtech.InventoryAPI.Service
{
    public class ExcelDataService
    {
        string basePath = HostingEnvironment.MapPath(@"~/");
        MarketActionService marketActionService = new MarketActionService();
        AccountService accountService = new AccountService();
        DMFService dmfService = new DMFService();
        MasterService masterService = new MasterService();

        // 导出所有线索报告
        public string MarketActionAllLeadsReportExport(string year, string userId, string roleTypeCode)
        {
            List<MarketActionAfter2LeadsReportDto> listTemp = marketActionService.MarketActionAfter2LeadsReportSearch("", year);
            List<MarketActionAfter2LeadsReportDto> list = new List<MarketActionAfter2LeadsReportDto>();
            List<Shop> roleTypeShopList = accountService.GetShopByRole(userId, roleTypeCode);

            foreach (MarketActionAfter2LeadsReportDto leadsReport in listTemp)
            {
                foreach (Shop shop in roleTypeShopList)
                {
                    if (leadsReport.ShopId == shop.ShopId)
                    {
                        list.Add(leadsReport);
                    }
                }
            }

            Workbook book = Workbook.Load(basePath + @"Content\Excel\" + "LeadsReportAll.xlsx", false);
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
                sheet.GetCell("H" + (rowIndex + 2)).Value = item.LeadsCheckName;
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
            string dirPath = basePath + @"\Temp\";
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath + fileName;
            book.Save(filePath);

            return filePath;
        }

        //导出线索报告
        public string MarketActionAfter2LeadsReportExport(string marketActionId)
        {
            List<MarketActionAfter2LeadsReportDto> list = marketActionService.MarketActionAfter2LeadsReportSearch(marketActionId, "");
            Workbook book = Workbook.Load(basePath + @"Content\Excel\" + "LeadsReport.xlsx", false);
            //填充数据
            Worksheet sheet = book.Worksheets[0];
            int rowIndex = 2;

            foreach (MarketActionAfter2LeadsReportDto item in list)
            {
                //客户姓名
                sheet.GetCell("A" + (rowIndex + 1)).Value = item.CustomerName;
                //BPNO
                sheet.GetCell("B" + (rowIndex + 1)).Value = item.BPNO;
                //是否车主
                sheet.GetCell("C" + (rowIndex + 1)).Value = item.OwnerCheckName;
                // 是否试驾
                sheet.GetCell("D" + (rowIndex + 1)).Value = item.TestDriverCheckName;
                // 是否线索
                sheet.GetCell("E" + (rowIndex + 1)).Value = item.LeadsCheckName;
                //感兴趣车型
                sheet.GetCell("F" + (rowIndex + 1)).Value = item.InterestedModelName;
                //是否成交
                sheet.GetCell("G" + (rowIndex + 1)).Value = item.DealCheckName;
                // 成交车型
                sheet.GetCell("H" + (rowIndex + 1)).Value = item.DealModelName;
                rowIndex++;
            }

            //保存excel文件
            string fileName = "线索报告" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            string dirPath = basePath + @"\Temp\";
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath + fileName;
            book.Save(filePath); 


            return filePath;
        }

        // MarketAction Export
        public string MarketActionExport(string actionName, string year, string month, string marketActionStatusCode, string shopId, string eventTypeId, bool? expenseAccountChk, string userId, string roleTypeCode)
        {
            List<MarketActionExportDto> list = new List<MarketActionExportDto>();
            List<MarketActionDto> marketActionList = new List<MarketActionDto>();

            List<MarketActionDto> marketActionListTemp = marketActionService.MarketActionSearch(actionName, year, month, marketActionStatusCode, shopId, eventTypeId, expenseAccountChk);
            List<Shop> roleTypeShopList = accountService.GetShopByRole(userId, roleTypeCode);
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
            foreach (MarketActionDto marketActiondto in marketActionList)
            {
                MarketActionExportDto exportDto = new MarketActionExportDto();
                exportDto.ActionCode = marketActiondto.ActionCode;
                exportDto.ActionName = marketActiondto.ActionName;
                exportDto.ActionPlace = marketActiondto.ActionPlace;
                exportDto.EndDate = marketActiondto.EndDate;
                exportDto.EventTypeId = marketActiondto.EventTypeId;
                exportDto.EventTypeName = marketActiondto.EventTypeName;
                exportDto.EventTypeNameEn = marketActiondto.EventTypeNameEn;
                exportDto.ExpenseAccount = marketActiondto.ExpenseAccount;
                exportDto.MarketActionId = marketActiondto.MarketActionId;
                exportDto.MarketActionStatusCode = marketActiondto.MarketActionStatusCode;
                exportDto.MarketActionStatusName = marketActiondto.MarketActionStatusName;
                exportDto.MarketActionStatusNameEn = marketActiondto.MarketActionStatusNameEn;
                exportDto.MarketActionTargetModelCode = marketActiondto.MarketActionTargetModelCode;
                exportDto.MarketActionTargetModelName = marketActiondto.MarketActionTargetModelName;
                exportDto.MarketActionTargetModelNameEn = marketActiondto.MarketActionTargetModelNameEn;
                exportDto.ShopCode = marketActiondto.ShopCode;
                exportDto.ShopId = marketActiondto.ShopId;
                exportDto.ShopName = marketActiondto.ShopName;
                exportDto.ShopNameEn = marketActiondto.ShopNameEn;
                exportDto.StartDate = marketActiondto.StartDate;
                List<MarketActionBefore21> before21 = marketActionService.MarketActionBefore21Search(marketActiondto.MarketActionId.ToString());
                if (before21 != null && before21.Count > 0)
                {
                    exportDto.MarketActionBefore21 = before21[0];
                }
                decimal? actualExpenseSum = 0;
                List<MarketActionAfter7ActualExpenseDto> expenseList = marketActionService.MarketActionAfter7ActualExpenseSearch(marketActiondto.MarketActionId.ToString());
                foreach (MarketActionAfter7ActualExpenseDto expenseDto in expenseList)
                {
                    actualExpenseSum += expenseDto.Total;
                }
                exportDto.ActualExpenseSum = actualExpenseSum;
                List<MarketActionAfter7> after7 = marketActionService.MarketActionAfter7Search(marketActiondto.MarketActionId.ToString());
                if (after7 != null && after7.Count > 0)
                {
                    exportDto.MarketActionAfter7 = after7[0];
                }
                List<MarketActionLeadsCountDto> leadsCount = marketActionService.MarketActionLeadsCountSearch(marketActiondto.MarketActionId.ToString());
                if (leadsCount != null && leadsCount.Count > 0)
                {
                    exportDto.LeadsCount = leadsCount[0];
                }
                list.Add(exportDto);

            }
            Workbook book = Workbook.Load(basePath + @"Content\Excel\" + "MarketAction.xlsx", false);
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
                    sheet.GetCell("Z" + (rowIndex + 1)).Value = item.LeadsCount.ActualOrderPCCount;
                }
                rowIndex++;
            }

            //保存excel文件
            string fileName = "市场活动" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            string dirPath = basePath+ @"\Temp\";
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath + fileName;
            book.Save(filePath);


            return filePath;

        }

        // ExpenseAccount Export
        public string ExpenseAccountExport(string shopId,string userId,string roleTypeCode)
        {
            List<ExpenseAccountDto> listTemp = dmfService.ExpenseAccountSearch("",shopId,"","");
            List<ExpenseAccountDto> list = new List<ExpenseAccountDto>();
            List<Shop> roleTypeShopList = accountService.GetShopByRole(userId, roleTypeCode);
            foreach (ExpenseAccountDto expenseAccount in listTemp)
            {
                foreach (Shop shop in roleTypeShopList)
                {
                    if (expenseAccount.ShopId == shop.ShopId)
                    {
                        expenseAccount.ExpenseAmt = TokenHelper.DecryptDES(expenseAccount.ExpenseAmt);
                        list.Add(expenseAccount);
                    }
                }
            }
            Workbook book = Workbook.Load(basePath + @"Content\Excel\" + "ExpenseAccount.xlsx", false);
            //填充数据
            Worksheet sheet = book.Worksheets[0];
            int rowIndex = 2;

            foreach (ExpenseAccountDto item in list)
            {
                //经销商名称
                sheet.GetCell("A" + (rowIndex + 1)).Value = item.ShopName;
                //项目
                sheet.GetCell("B" + (rowIndex + 1)).Value = item.DMFItemName;
                //活动名称
                sheet.GetCell("C" + (rowIndex + 1)).Value = item.ActionName;
                // 费用金额
                if (string.IsNullOrEmpty(item.ExpenseAmt))
                {
                    sheet.GetCell("D" + (rowIndex + 1)).Value = "";
                }
                else
                {
                    sheet.GetCell("D" + (rowIndex + 1)).Value = Convert.ToDecimal(item.ExpenseAmt);
                }
                // 申请状态
                sheet.GetCell("E" + (rowIndex + 1)).Value = item.ApplyStatus;
                //申请说明
                sheet.GetCell("F" + (rowIndex + 1)).Value = item.ApprovalReason;
                //批复结果
                sheet.GetCell("G" + (rowIndex + 1)).Value = item.ReplyStatus;
                // 批复说明
                sheet.GetCell("H" + (rowIndex + 1)).Value = item.ReplyReason;
                rowIndex++;
            }

            //保存excel文件
            string fileName = "费用报销" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            string dirPath = basePath + @"\Temp\";
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath + fileName;
            book.Save(filePath);


            return filePath;
        }
        // DMFDetail Export
        public string DMFDetailExport(string shopId,string dmfItemName,string userId,string roleTypeCode)
        {
            List<DMFDetailDto> listTemp = dmfService.DMFDetailSearch("",shopId,"", dmfItemName);
            List<DMFDetailDto> list = new List<DMFDetailDto>();
            List<Shop> roleTypeShopList = accountService.GetShopByRole(userId, roleTypeCode);

            foreach (DMFDetailDto dmfDetail in listTemp)
            {
                foreach (Shop shop in roleTypeShopList)
                {
                    if (dmfDetail.ShopId == shop.ShopId)
                    {
                        list.Add(dmfDetail);
                    }
                }
            }
            Workbook book = Workbook.Load(basePath + @"Content\Excel\" + "DMFDetail.xlsx", false);
            //填充数据
            Worksheet sheet = book.Worksheets[0];
            int rowIndex = 2;

            foreach (DMFDetailDto item in list)
            {
                //经销商
                sheet.GetCell("A" + (rowIndex + 1)).Value = item.ShopName;
                //项目
                sheet.GetCell("B" + (rowIndex + 1)).Value = item.DMFItemName;
                //预算花费
                sheet.GetCell("C" + (rowIndex + 1)).Value = item.Budget;
                // 实际花费
                sheet.GetCell("D" + (rowIndex + 1)).Value = item.AcutalAmt;
                // 备注
                sheet.GetCell("E" + (rowIndex + 1)).Value = item.Remark;
                rowIndex++;
            }

            //保存excel文件
            string fileName = "预算与费用" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            string dirPath = basePath + @"\Temp\";
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath + fileName;
            book.Save(filePath);

            return filePath;
        }

        // UserInfo Export
        public string UserInfoExport()
        {
            List<UserInfoDto> list = masterService.UserInfoSearch("","","","","","");
            Workbook book = Workbook.Load(basePath + @"Content\Excel\" + "UserInfo.xlsx", false);
            //填充数据
            Worksheet sheet = book.Worksheets[0];
            int rowIndex = 1;

            foreach (UserInfoDto item in list)
            {
                //账号
                sheet.GetCell("A" + (rowIndex + 1)).Value = item.AccountId;
                //账号名称
                sheet.GetCell("B" + (rowIndex + 1)).Value = item.AccountName;
                //账号名称中文
                sheet.GetCell("C" + (rowIndex + 1)).Value = item.AccountNameEn;
                // 邮箱
                sheet.GetCell("D" + (rowIndex + 1)).Value = item.Email;
                // 权限
                sheet.GetCell("E" + (rowIndex + 1)).Value = item.RoleTypeName;
                rowIndex++;
            }

            //保存excel文件
            string fileName = "用户信息" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            string dirPath = basePath + @"\Temp\";
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string filePath = dirPath + fileName;
            book.Save(filePath);

            return filePath;
        }

        // 导入线索报告
        public List<MarketActionAfter2LeadsReportDto> LeadsReportImport(string ossPath)
        {
            // 从OSS下载文件
            string downLoadFilePath = basePath + @"Content\Excel\ExcelImport\"+ DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            OSSClientHelper.GetObject(ossPath, downLoadFilePath);
            Workbook book = Workbook.Load(downLoadFilePath, false);
            Worksheet sheet = book.Worksheets[0];
            List<MarketActionAfter2LeadsReportDto> list = new List<MarketActionAfter2LeadsReportDto>();
            for (int i = 0; i < 10000; i++)
            {
                string customerName = sheet.GetCell("A" + (i + 3)).Value==null?"":sheet.GetCell("A" + (i + 3)).Value.ToString();
                if (string.IsNullOrEmpty(customerName)) break;
                MarketActionAfter2LeadsReportDto report = new MarketActionAfter2LeadsReportDto();
                report.BPNO = sheet.GetCell("B" + (i + 3)).Value==null?"":sheet.GetCell("B" + (i + 3)).Value.ToString();
                report.CustomerName = customerName;
                report.DealCheckName = sheet.GetCell("G" + (i + 3)).Value==null?"":sheet.GetCell("G" + (i + 3)).Value.ToString();
                report.DealModelName = sheet.GetCell("H" + (i + 3)).Value==null?"":sheet.GetCell("H" + (i + 3)).Value.ToString();
                report.InterestedModelName = sheet.GetCell("F" + (i + 3)).Value==null?"":sheet.GetCell("F" + (i + 3)).Value.ToString();
                report.LeadsCheckName = sheet.GetCell("E" + (i + 3)).Value==null?"":sheet.GetCell("E" + (i + 3)).Value.ToString();
                report.OwnerCheckName = sheet.GetCell("C" + (i + 3)).Value==null?"":sheet.GetCell("C" + (i + 3)).Value.ToString();
                report.TestDriverCheckName = sheet.GetCell("D" + (i + 3)).Value==null?"":sheet.GetCell("D" + (i + 3)).Value.ToString();
                list.Add(report);
            }
            return list;
            
        }

        // 导入市场基金详情
        public List<DMFDetailDto> DMFDetailImport(string ossPath)
        {
            // 从OSS下载文件
            string downLoadFilePath = basePath + @"Content\Excel\ExcelImport\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            OSSClientHelper.GetObject(ossPath, downLoadFilePath);
            Workbook book = Workbook.Load(downLoadFilePath, false);
            Worksheet sheet = book.Worksheets[0];
            List<DMFDetailDto> list = new List<DMFDetailDto>();
            for (int i = 0; i < 10000; i++)
            {
                string shopName = sheet.GetCell("A" + (i + 3)).Value == null ? "" : sheet.GetCell("A" + (i + 3)).Value.ToString();
                if (string.IsNullOrEmpty(shopName)) break;
                DMFDetailDto dmfDetail = new DMFDetailDto();
                dmfDetail.ShopName = shopName;
                dmfDetail.DMFItemName = sheet.GetCell("B" + (i + 3)).Value == null ? "" : sheet.GetCell("B" + (i + 3)).Value.ToString();
                dmfDetail.Budget = sheet.GetCell("C" + (i + 3)).Value == null ? "" : sheet.GetCell("C" + (i + 3)).Value.ToString();
                dmfDetail.AcutalAmt = sheet.GetCell("D" + (i + 3)).Value == null ? "" : sheet.GetCell("D" + (i + 3)).Value.ToString();
                dmfDetail.Remark = sheet.GetCell("E" + (i + 3)).Value == null ? "" : sheet.GetCell("E" + (i + 3)).Value.ToString();
                list.Add(dmfDetail);
            }
            return list;

        }

        // 导入月批售概况
        public List<MonthSaleDto> MonthSaleImport(string ossPath)
        {
            // 从OSS下载文件
            string downLoadFilePath = basePath + @"Content\Excel\ExcelImport\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            OSSClientHelper.GetObject(ossPath, downLoadFilePath);
            Workbook book = Workbook.Load(downLoadFilePath, false);
            Worksheet sheet = book.Worksheets[0];
            List<MonthSaleDto> list = new List<MonthSaleDto>();
            for (int i = 0; i < 10000; i++)
            {
                string shopName = sheet.GetCell("A" + (i + 3)).Value == null ? "" : sheet.GetCell("A" + (i + 3)).Value.ToString();
                if (string.IsNullOrEmpty(shopName)) break;
                MonthSaleDto monthSale = new MonthSaleDto();
                monthSale.ShopName = shopName;
                monthSale.YearMonth = sheet.GetCell("B" + (i + 3)).Value == null ? "" : sheet.GetCell("B" + (i + 3)).Value.ToString();
                monthSale.ActualSaleCount = sheet.GetCell("C" + (i + 3)).Value == null ? "" : sheet.GetCell("C" + (i + 3)).Value.ToString();
                monthSale.ActualSaleAmt = sheet.GetCell("D" + (i + 3)).Value == null ? "" : sheet.GetCell("D" + (i + 3)).Value.ToString();
                list.Add(monthSale);
            }
            return list;

        }
    }
}