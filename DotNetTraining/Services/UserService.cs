using Application.Settings;
using System.Text;
using AutoMapper;
using Common.Application.CustomAttributes;
using Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;
using Newtonsoft.Json;
using System.Data;

namespace DotNetTraining.Services
{
  
    [ScopedService]
    public class UserService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection): BaseService(services)
    {
        private readonly UserRepository _repo = new(connection);

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _repo.GetAllUsers();
            return users;
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            return await _repo.GetUserById(userId);
        }

        public async Task<User?> UpdateUser(Guid userId, User user)
        {
            var existingUser = await _repo.GetUserById(userId);
            if (existingUser == null)
            {
                return null; // User không tồn tại
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password; // Cần mã hóa password nếu cần

            return await _repo.UpdateUser(existingUser);
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var existingUser = await _repo.GetUserById(userId);
            if (existingUser == null)
            {
                return false; // User không tồn tại
            }

            return await _repo.DeleteUser(userId);
        }

        public async Task<User?> CreateUser(CreateUserDto newUser)
        {
            // Kiểm tra email đã tồn tại chưa
            var existingUser = await _repo.GetUserByEmail(newUser.Email);
            if (existingUser != null)
            {
                return null; // Email đã tồn tại
            }

            // Tạo đối tượng User
            var user = new User
            {
                Id = Guid.NewGuid(), // Tạo ID mới
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password
            };

            // Gọi repository để lưu vào DB
            return await _repo.CreateUser(user);
        }
    }
}
