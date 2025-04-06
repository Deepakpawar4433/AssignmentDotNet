using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;
using AssignmentDotNet.Repository;

namespace AssignmentDotNet.Service.UserRoleService
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IRepository<UserRole> _repository;
        private readonly AssignmentDbContext _context;

        public UserRoleService(IRepository<UserRole> repository, AssignmentDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetAllRoles()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<UserRole> GetRoleById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddRole(UserRoleDto roleDto)
        {
            var role = new UserRole
            {
                Name = roleDto.Name
            };
            await _repository.AddAsync(role);
        }

        public async Task<string> UpdateRoleById(int id, UserRoleDto roleDto)
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null)
            {
                return "Role not found.";
            }
            role.Name = roleDto.Name;

            await _repository.UpdateAsync(role);
            return "User role updated successfully.";
        }

        public async Task DeleteRole(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
