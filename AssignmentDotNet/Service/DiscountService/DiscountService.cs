using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;
using AssignmentDotNet.Repository;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Service.DiscountService
{
    public class DiscountService : IDiscountService
    {
        private readonly IRepository<Discount> _repository;
        private readonly AssignmentDbContext _context;

        public DiscountService(IRepository<Discount> discountRepository, AssignmentDbContext dbContext)
        {
            _repository = discountRepository;
            _context = dbContext;
        }

        public async Task<IEnumerable<Discount>> GetAllDiscounts()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Discount> GetDiscountById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<string> AddDiscount(DiscountDto discountDto)
        {
            // Mobile Id checkent that is exist in the Mobile table or not
            var mobileExists = await _context.Mobile.AnyAsync(m => m.Id == discountDto.MobileId);
            if (!mobileExists)
            {
                return "MobileId does not exist in the Mobile table.";
            }
            var discount = new Discount
            {
                Id = discountDto.Id,
                MobileId = discountDto.MobileId,
                DiscountedAmount = discountDto.DiscountAmont,
                ValidUntil = discountDto.ValidUntil
            };
            await _repository.AddAsync(discount);
            return "Discount added successfully.";
        }

        public async Task<string> UpdateDiscount(int id, DiscountDto discountDto)
        {
            var discount = await _repository.GetByIdAsync(id);
            if (discount == null)
            {
                return "Discount not found.";
            }

            var mobileExists = await _context.Mobile.AnyAsync(m => m.Id == discountDto.MobileId);
            if (!mobileExists)
            {
                return "MobileId does not exist in the Mobile table.";
            }
            discount.MobileId = discountDto.MobileId;
            discount.DiscountedAmount = discountDto.DiscountAmont;
            discount.ValidUntil = discountDto.ValidUntil;

            await _repository.UpdateAsync(discount);
            return "Discount updated successfully.";
        }

        public async Task DeleteDiscount(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
