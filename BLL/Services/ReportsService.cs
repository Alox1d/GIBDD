using Bll.Models.ReportsModel;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportsService
    {
        public ReportsService()
        {
        }

        public List<ArticleSum> MakeReport(List<VehicleOffense> lists, DateTime startDate, DateTime endDate)
        {

            var res = lists
                .Where(i => i.CarDriver != null  &&
            i.CarDriver.Offenses.All(p => p.Date >= startDate) &&
            i.CarDriver.Offenses.All(p => p.Date <= endDate) && i.ArticleOffense != null)
                .GroupBy(i => new { i.ArticleOffense.Number })
            .Select(i => new ArticleSum
            {
                Number = i.Key.Number,
                SumOfPenalty = i.Sum(j => j.Penalty)
            }).ToList();
            return res;
        }


    }
}
