using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;

namespace AssignmentDotNet.Service.DiscountService
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetAllDiscounts();
        Task<Discount> GetDiscountById(int id);
        Task<string> AddDiscount(DiscountDto discountDto);
        Task<string> UpdateDiscount(int id, DiscountDto discountDto);
        Task DeleteDiscount(int id);
    }
}
