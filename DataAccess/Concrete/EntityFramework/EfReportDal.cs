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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReportDal : EfRepositoryBase<Report, ModelDbContext>, IReportDal
    {
        public ReportDto GetDailyReports(DateTime date)
        {
            using var context = new ModelDbContext();
            var orders = context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.CartItems)
            .ThenInclude(ci=>ci.Product)
            .Where(o => o.OrderDate.Date == date.Date)
            .ToList();

            return CalculateReport(orders, date);
        }

        public ReportDto GetMonthlyReports(DateTime date)
        {
            var startOfMonth = new DateTime(date.Year, date.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            using var context = new ModelDbContext();
            var orders = context.Orders
                .Include(o => o.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .Where(o => o.OrderDate.Date >= startOfMonth && o.OrderDate.Date <= endOfMonth)
                .ToList();

            return CalculateReport(orders, startOfMonth, endOfMonth);
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

        public ReportDto GetWeeklyReports(DateTime date)
        {
            var startOfWeek = date.Date.AddDays(-(int)date.DayOfWeek); // Bazar ertəsi
            var endOfWeek = startOfWeek.AddDays(6); // Bazar günü
            using var context = new ModelDbContext();
            var orders = context.Orders
                .Include(o => o.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .Where(o => o.OrderDate.Date >= startOfWeek && o.OrderDate.Date <= endOfWeek)
                .ToList();

            return CalculateReport(orders, startOfWeek, endOfWeek);
        }

        public ReportDto GetYearReports(DateTime date)
        {
            throw new NotImplementedException();
        }

        private ReportDto CalculateReport(List<Order> orders, DateTime startDate, DateTime? endDate = null)
        {
            var totalSales = orders.Sum(o => o.TotalAmount);
            var totalOrders = orders.Count;
            var totalCost = orders.Sum(o => o.Cart.CartItems.Sum(ci => ci.Product.CostPrice * ci.Quantity));
            var profit = totalSales - totalCost;

            return new ReportDto
            {
                Date = startDate,
                EndDate = endDate,
                TotalSales = totalSales,
                TotalOrders = totalOrders,
                Profit = profit
            };
        }
    }
}
