using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReportDal : EfRepositoryBase<Report, ModelDbContext>, IReportDal
    {
        public ReportDto GetDailyReports()
        {
            using var context = new ModelDbContext();
            var date = DateTime.Now.Date;

        }

        public ReportDto GetMonthlyReports()
        {
            throw new NotImplementedException();
        }

        public List<TopProductDto> GetTopSellingProducts()
        {
            using var context = new ModelDbContext();
            var products = context.Orders
                         .Include(o => o.Cart)
                         .ThenInclude(c => c.CartItems)
                         .ThenInclude(ci => ci.Product)
                         .SelectMany(o => o.Cart.CartItems)
                         .GroupBy(ci => ci.ProductId)
                         .Select(g => new TopProductDto
                         {
                             Name = g.First().Product.Name,
                             SaleCount = g.Sum(ci => ci.Quantity)
                         })
                         .OrderByDescending(p => p.SaleCount)
                         .ToList();

            return products;
        }

        public ReportDto GetWeeklyReports()
        {
            throw new NotImplementedException();
        }

        public ReportDto GetYearReports()
        {
            throw new NotImplementedException();
        }
    }
}
