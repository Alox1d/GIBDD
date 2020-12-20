using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

//namespace BLL.Services
//{
//    public class ReportsService : IReportService
//    {
//        private IDbRepos db;
//        public ReportsService(IDbRepos repos)
//        {
//            db = repos;
//        }
//        //private class SPResult
//        //{
//        //    [StringLength(50)]
//        //    public string ArticleUKRF { get; set; }

//        //    public DateTime? DateOfOffense { get; set; }

//        //    public double? SumOfPenalty { get; set; }

//        //    [StringLength(50)]
//        //    public string FIO_PenaltyWrite { get; set; }
//        //}

//        //public static List<ArticleSum> ExecuteSP(string date1, string date2)
//        //{
//        //    DateTime first;
//        //    DateTime.TryParse(date1, out first);

//        //    DateTime second;
//        //    DateTime.TryParse(date2, out second);

//        //    SqlParameter param1 = new SqlParameter("@first_date", date1);
//        //    SqlParameter param2 = new SqlParameter("@second_date", date2);

//        //    GIBDDContext db = new GIBDDContext();
//        //    var result = db.Database.SqlQuery<SPResult>("Narusheniya @first_date,@second_date", new object[] { param1, param2 }).ToList();
//        //    var data = result.GroupBy(i => new { i.ArticleUKRF })
//        //        .Select(i => new ArticleSum
//        //        {
//        //            Article = i.Key.ArticleUKRF,
//        //            SumOfPenalty = i.Sum(j => j.SumOfPenalty)
//        //        }).ToList();
//        //    return data;
//        //}
//        public List<ArticleSum> MakeReport(string date1, string date2)
//        {
//            return db.Report.ExecuteSP(date1, date2);
//        }
//    }
//}
