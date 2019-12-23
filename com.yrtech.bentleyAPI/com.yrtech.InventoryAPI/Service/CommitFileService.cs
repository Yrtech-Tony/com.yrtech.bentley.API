﻿using com.yrtech.InventoryAPI.DTO;
using System;
using com.yrtech.bentley.DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace com.yrtech.InventoryAPI.Service
{
    public class CommitFileService
    {
        Bentley db = new Bentley();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<CommitFile> CommitFileSearch(string year)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Year", year) };
            Type t = typeof(CommitFile);
            string sql = @"SELECT FileId,FileName,UpperFileId FROM CommitFile
                            WHERE Year = @Year";
            return db.Database.SqlQuery(t, sql, para).Cast<CommitFile>().ToList();
        }
        public List<ShopCommitFileRecord> ShopCommitFileRecordSearch(string shopId, string fileId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ShopId", shopId),
                                                    new SqlParameter("@File", fileId) };
            Type t = typeof(ShopCommitFileRecord);
            string sql = @"SELECT B.ShopId,A.FileId,A.FileName,B.InDateTime,B.ModifyDateTime 
                            FROM CommitFile A INNER JOIN ShopCommitFileRecord B ON A.FileId = B.FileId
                            WHERE B.ShopId = @ShopId AND A.FileId = @FileId";
            return db.Database.SqlQuery(t, sql, para).Cast<ShopCommitFileRecord>().ToList();
        }
        public void ShopCommitFileRecordSave(ShopCommitFileRecord shopCommitFileRecord)
        {
            ShopCommitFileRecord findOne = db.ShopCommitFileRecord.Where(x => (x.ShopId == shopCommitFileRecord.ShopId && x.FileId == shopCommitFileRecord.FileId)).FirstOrDefault();
            if (findOne == null)
            {
                shopCommitFileRecord.SeqNO = 1;
            }
            else
            {
                ShopCommitFileRecord findOneMax = db.ShopCommitFileRecord.Where(x => (x.ShopId == shopCommitFileRecord.ShopId && x.FileId == shopCommitFileRecord.FileId)).OrderByDescending(x => x.SeqNO).FirstOrDefault();
                shopCommitFileRecord.SeqNO = findOneMax.SeqNO + 1;
            }
            shopCommitFileRecord.InDateTime = DateTime.Now;
            shopCommitFileRecord.ModifyDateTime = DateTime.Now;
            db.ShopCommitFileRecord.Add(shopCommitFileRecord);
            db.SaveChanges();
        }
        public void ShopCommitFileRecordDelete(string shopId,string fileId,string seqNO)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ShopId", shopId),
                                                    new SqlParameter("@FileId", fileId),
                                                    new SqlParameter("@SeqNO", seqNO)};
            Type t = typeof(ShopCommitFileRecord);
            string sql = @"DELETE ShopCommitFileRecord WHERE ShopId =@ShopId AND FileId = @FileId AND SeqNO = @SeqNO";
             db.Database.ExecuteSqlCommand(sql, para);
        }
        public List<ShopCommitFileRecordStatusDto> ShopCommitFileRecordStatusSearch(string year)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Year", year) };
            Type t = typeof(ShopCommitFileRecordStatusDto);
            string sql = @"SELECT B.ShopId,A.FileId,Count(*) AS FileCount
                            FROM CommitFile A INNER JOIN ShopCommitFileRecord B ON A.FileId = B.FileId
                            WHERE A.Year = @Year
                            GROUP BY B.ShopId,A.FileId";
            return db.Database.SqlQuery(t, sql, para).Cast<ShopCommitFileRecordStatusDto>().ToList();
        }
    }
}