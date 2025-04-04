using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;

namespace AssignmentDotNet.Service.MobileService
{
    public interface IMobileService
    {
        Task<IEnumerable<Mobile>> GetAllMobiles();
        Task<Mobile> GetMobileById(int id);
        Task AddMobile(MobileDto mobileDto);
        //Task<string> UpdateMobile(MobileDto mobileDto);
        Task<string> UpdateMobileById(int id, MobileDto mobileDto);
        Task DeleteMobile(int id);
        Task<decimal> GetBestPrice(int mobileId);
    }
}
