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
        IDataResult<ReportDto> GetDailyReports();
        IDataResult<ReportDto> GetWeeklyReports();
        IDataResult<ReportDto> GetMonthlyReports();
        IDataResult<ReportDto> GetYearReports();
        IDataResult<ProductDto> GetTopSellingProducts();
    }
}
