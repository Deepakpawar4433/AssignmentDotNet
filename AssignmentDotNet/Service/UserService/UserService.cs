using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;
using AssignmentDotNet.Repository;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly AssignmentDbContext _context;

        public UserService(IRepository<User> repository, AssignmentDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<string> AddUser(UserDto userDto)
        {
            var roleIdExists = await _context.UserRole.AnyAsync(u => u.Id == userDto.RoleId);
            if (!roleIdExists)
            {
                return "RoleId does not exist in the UserRole table.";
            }
            bool userExists = await _context.Users.AnyAsync(u => u.Email == userDto.Email || u.PhoneNumber == userDto.PhoneNumber);

            if (userExists)
            {
                return "Email or PhoneNumber already exists.";
            }
            var user = new User
            {
                Username = userDto.Username,
                PhoneNumber = userDto.PhoneNumber,
                Email = userDto.Email,
                Password = userDto.Password,
                RoleId = userDto.RoleId,
            };
            await _repository.AddAsync(user);
            return "User added successfully.";
        }

        public async Task<string> UpdateUser(int id, UserDto userDto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return "User not found.";
            }
            var roleExists = await _context.UserRole.AnyAsync(r => r.Id == userDto.RoleId);
            if (!roleExists)
            {
                return "RoleId does not exist in the UserRole table.";
            }

            bool duplicateExists = await _context.Users.AnyAsync(u => (u.Email == userDto.Email || u.PhoneNumber == userDto.PhoneNumber) && u.Id != id);

            if (duplicateExists)
            {
                return "Email or PhoneNumber already exists.";
            }
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Password = userDto.Password;
            user.RoleId = userDto.RoleId;

            await _repository.UpdateAsync(user);
            return "User updated successfully.";
        }

        public async Task DeleteUser(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
