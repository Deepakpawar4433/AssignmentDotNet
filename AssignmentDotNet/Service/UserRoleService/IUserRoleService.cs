using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;

namespace AssignmentDotNet.Service.UserRoleService
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllRoles();
        Task<UserRole> GetRoleById(int id);
        Task AddRole(UserRoleDto roleDto);
        Task<string> UpdateRoleById(int id, UserRoleDto roleDto);
        Task DeleteRole(int id);
    }
}
