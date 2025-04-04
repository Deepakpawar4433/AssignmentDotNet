using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;
using AssignmentDotNet.Repository;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Service.MobileService
{
    public class MobileService : IMobileService
    {
        private readonly IRepository<Mobile> _repository;
        private readonly AssignmentDbContext _context;
        public MobileService(IRepository<Mobile> mobileRepository, AssignmentDbContext context)
        {
            _repository = mobileRepository;
            _context = context;
        }
        public async Task<IEnumerable<Mobile>> GetAllMobiles()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Mobile> GetMobileById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddMobile(MobileDto mobileDto)
        {
            var mobile = new Mobile
            {
                Brand = mobileDto.Brand,
                Model = mobileDto.Model,
                Price = mobileDto.Price
            };
            await _repository.AddAsync(mobile);
        }
        //public async Task<string> UpdateMobile(MobileDto mobileDto)
        //{
        //    var mobile = await _repository.GetByIdAsync(mobileDto.Id);
        //    if (mobile == null)
        //    {
        //        return "Mobile not found.";
        //    }
        //    mobile.Brand = mobileDto.Brand;
        //    mobile.Model = mobileDto.Model;
        //    mobile.Price = mobileDto.Price;

        //    await _repository.UpdateAsync(mobile);
        //    return "Mobile updated successfully.";
        //}
        public async Task<string> UpdateMobileById(int id, MobileDto mobileDto)
        {
            var mobile = await _repository.GetByIdAsync(id);
            if (mobile == null)
            {
                return "Mobile not found.";
            }
            mobile.Brand = mobileDto.Brand;
            mobile.Model = mobileDto.Model;
            mobile.Price = mobileDto.Price;

            await _repository.UpdateAsync(mobile);
            return "Mobile updated successfully.";
        }
        public async Task DeleteMobile(int id)
        {
            await _repository.DeleteAsync(id);

        }
        public async Task<decimal> GetBestPrice(int mobileId)
        {
            var sales = await _context.Sales
                .Where(s => s.MobileId == mobileId)
                .ToListAsync();

            if (sales == null || sales.Count == 0)
            {
                return 0;
            }
            decimal avgPrice = sales.Average(s => s.TotalAmount / s.Quantity);

            decimal mostFrequentPrice = sales
                .GroupBy(s => s.TotalAmount / s.Quantity)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            decimal bestPrice = (avgPrice * 0.7m) + (mostFrequentPrice * 0.3m);

            return Math.Round(bestPrice, 2);
        }
    }
}