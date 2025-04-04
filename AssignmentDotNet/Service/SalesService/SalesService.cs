using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;
using AssignmentDotNet.Repository;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Service.SalesService
{
    public class SalesService : ISalesService
    {
        private readonly IRepository<Sales> _repository;
        private readonly AssignmentDbContext _context;

        public SalesService(IRepository<Sales> salesRepository, AssignmentDbContext dbContext)
        {
            _repository = salesRepository;
            _context = dbContext;
        }

        public async Task<IEnumerable<Sales>> GetAllSales()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Sales> GetSalesById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<string> AddSales(SalesDto salesDto)
        {
            // Checking MobileId exist or not
            var mobileExists = await _context.Mobile.AnyAsync(m => m.Id == salesDto.MobileId);
            if (!mobileExists)
            {
                return "MobileId does not exist in the Mobile table.";
            }
            // Checking DiscountId exist or not
            if (salesDto.DiscountId.HasValue)
            {
                var discountExists = await _context.Discount.AnyAsync(d => d.Id == salesDto.DiscountId.Value);
                if (!discountExists)
                {
                    return "DiscountId does not exist in the Discount table.";
                }
            }
            var sales = new Sales
            {
                MobileId = salesDto.MobileId,
                Quantity = salesDto.Quantity,
                TotalAmount = salesDto.TotalAmount,
                SalesDate = salesDto.SalesDate,
                DiscountId = salesDto.DiscountId
            };
            await _repository.AddAsync(sales);
            return "Sales record added successfully.";
        }

        public async Task<string> UpdateSales(SalesDto salesDto)
        {
            var existingSale = await _repository.GetByIdAsync(salesDto.Id);
            if (existingSale == null)
            {
                return "Sales record not found.";
            }

            existingSale.MobileId = salesDto.MobileId;
            existingSale.Quantity = salesDto.Quantity;
            existingSale.TotalAmount = salesDto.TotalAmount;
            existingSale.SalesDate = salesDto.SalesDate;
            existingSale.DiscountId = salesDto.DiscountId;

            await _repository.UpdateAsync(existingSale);
            return "Sales record updated successfully.";
        }

        public async Task DeleteSales(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
