namespace AssignmentDotNet.Service.SalesReportService
{
    public interface ISalesReportService
    {
        Task<List<object>> GetMonthlySalesReport(DateTime fromDate, DateTime toDate);
        Task<List<object>> GetBrandWiseSalesReport(DateTime fromDate, DateTime toDate);
    }
}
