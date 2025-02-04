using Core.DataAccess;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IReportDal : IRepositoryBase<Report>
    {
        ReportDto GetDailyReports();
        ReportDto GetWeeklyReports();
        ReportDto GetMonthlyReports();
        ReportDto GetYearReports();
        List<TopProductDto> GetTopSellingProducts();
    }
}
