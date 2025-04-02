using AssignmentDotNet.Model;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Service.SalesReportService
{
    public class SalesReportService : ISalesReportService
    {
        private readonly AssignmentDbContext _dbContext;

        public SalesReportService(AssignmentDbContext context)
        {
            _dbContext = context;
        }
        //public async Task<List<object>> GetMonthlySalesReport(DateTime startDate, DateTime endDate)
        public async Task<List<object>> GetMonthlySalesReport(DateTime fromDate, DateTime toDate)
        {
            var salesData = await _dbContext.Sales
                .Where(s => s.SalesDate >= fromDate && s.SalesDate <= toDate)
                .ToListAsync();

            var report = salesData
                .GroupBy(s => s.SalesDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalSales = g.Sum(s => s.TotalAmount),
                    TotalQuantity = g.Sum(s => s.Quantity)
                })
                .ToList();

            return report.Cast<object>().ToList();
        }

        //

        public async Task<List<object>> GetBrandWiseSalesReport(DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.Sales
                .Where(s => s.SalesDate >= fromDate && s.SalesDate <= toDate)
                .Include(s => s.Mobile)
                .GroupBy(s => s.Mobile.Brand)
                .Select(g => new
                {
                    Brand = g.Key,
                    TotalSales = g.Sum(s => s.TotalAmount),
                    TotalQuantity = g.Sum(s => s.Quantity)
                })
                .ToListAsync<object>();
        }
        //
        //public async Task<>
    }
}
