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
using iText.Commons.Actions.Data;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DotNetTraining.Services
{
  
    [ScopedService]
    public class UserService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection): BaseService(services)
    {
        private readonly UserRepository _repo = new(connection);

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            try
            {
                var users = await _repo.GetAllUsers();
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách người dùng", ex);
            }

        }

        public async Task<UserDto?> GetUserById(Guid userId)
        {
            var existingUser = await _repo.GetUserById(userId);
            if (existingUser == null)
            {
                throw new Exception("user not exist");
            }
            // map entity to Dto
            var dto = _mapper.Map<UserDto>(existingUser);

            return dto;

        }

        public async Task<UserDto?> UpdateUser(Guid userId, UpdateUserDto userDto)
        {
            var existingUser = await _repo.GetUserById(userId);
            if (existingUser == null)
            {
                return null; // User không tồn tại
            }
            // Cập nhật thông tin từ DTO
            _mapper.Map(userDto, existingUser); 

            var updatedUser = await _repo.UpdateUser(existingUser);
            return _mapper.Map<UserDto>(updatedUser);
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
                    Password = newUser.Password,
                };
            
            // Gọi repository để lưu vào DB
            return await _repo.CreateUser(user);
        }

    }
}
