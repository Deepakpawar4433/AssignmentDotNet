using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;

namespace AssignmentDotNet.Service.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<string> AddUser(UserDto userDto);
        Task<string> UpdateUser(int id, UserDto userDto);
        Task DeleteUser(int id);
    }
}
