using Core.Utilities.Results;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReportService
    {
        IDataResult<ReportDto> GetDailyReports(DateTime date);
        IDataResult<ReportDto> GetWeeklyReports(DateTime date);
        IDataResult<ReportDto> GetMonthlyReports(DateTime date);
        IDataResult<ReportDto> GetYearReports(DateTime date);
        IDataResult<List<TopProductDto>> GetTopSellingProducts();
    }
}
