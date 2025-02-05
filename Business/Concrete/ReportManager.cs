using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReportManager : IReportService
    {
        private readonly IReportDal _reportDal;

        public ReportManager(IReportDal reportDal)
        {
            _reportDal = reportDal;
        }



        [ValidationAspect(typeof(ReportValidator))]
        public IDataResult<ReportDto> GetDailyReports(DateTime date)
        {
            return new SuccessDataResult<ReportDto>(_reportDal.GetDailyReports(date), "Gunluk qazanc getirildi");
        }

        [ValidationAspect(typeof(ReportValidator))]
        public IDataResult<ReportDto> GetMonthlyReports(DateTime date)
        {
            return new SuccessDataResult<ReportDto>(_reportDal.GetMonthlyReports(date), "Ayliq qazanc getirildi");
        }

        public IDataResult<List<TopProductDto>> GetTopSellingProducts()
        {
            return new SuccessDataResult<List<TopProductDto>>(_reportDal.GetTopSellingProducts(), "En cox satilan mehsullar getirildi");
        }

        [ValidationAspect(typeof(ReportValidator))]
        public IDataResult<ReportDto> GetWeeklyReports(DateTime date)
        {
            return new SuccessDataResult<ReportDto>(_reportDal.GetWeeklyReports(date), "Heftelik qazanc getirildi");
        }

        [ValidationAspect(typeof(ReportValidator))]
        public IDataResult<ReportDto> GetYearReports(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
