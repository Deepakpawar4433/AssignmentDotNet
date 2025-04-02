using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;

namespace AssignmentDotNet.Service.SalesService
{
    public interface ISalesService
    {
        Task<IEnumerable<Sales>> GetAllSales();
        Task<Sales> GetSalesById(int id);
        Task<string> AddSales(SalesDto salesDto);
        Task<string> UpdateSales(SalesDto salesDto);
        Task DeleteSales(int id);
    }
}
