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
        //public async Task<List<object>> GetProfitLossReport(DateTime fromDate, DateTime toDate)
        //{

        //}
        public async Task<object> GetProfitLossReport(DateTime currentFromDate, DateTime currentToDate, DateTime previousFromDate, DateTime previousToDate)
        {

            var currentSalesTotal = await _dbContext.Sales
                .Where(s => s.SalesDate >= currentFromDate && s.SalesDate <= currentToDate)
                .SumAsync(s => s.TotalAmount);


            var previousSalesTotal = await _dbContext.Sales
                .Where(s => s.SalesDate >= previousFromDate && s.SalesDate <= previousToDate)
                .SumAsync(s => s.TotalAmount);


            var profitOrLoss = currentSalesTotal - previousSalesTotal;


            return new
            {
                CurrentSales = currentSalesTotal,
                PreviousSales = previousSalesTotal,
                ProfitOrLoss = profitOrLoss
            };
        }

    }
}
