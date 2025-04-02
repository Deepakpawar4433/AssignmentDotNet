using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;
using AssignmentDotNet.Repository;

namespace AssignmentDotNet.Service.MobileService
{
    public class MobileService : IMobileService
    {
        private readonly IRepository<Mobile> _repository;
        public MobileService(IRepository<Mobile> mobileRepository)
        {
            _repository = mobileRepository;
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
        public async Task<string> UpdateMobile(MobileDto mobileDto)
        {
            var mobile = await _repository.GetByIdAsync(mobileDto.Id);
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
    }
}