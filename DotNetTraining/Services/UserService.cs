using Application.Settings;
using System.Text;
using AutoMapper;
using Common.Application.CustomAttributes;
using Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Models;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;
using Newtonsoft.Json;
using System.Data;
using iText.Commons.Actions.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using iText.Forms.Fields.Merging;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.AspNetCore.Identity;

namespace DotNetTraining.Services
{

    [ScopedService]
    public class UserService : BaseService
    {
        private readonly UserRepository _repo;
        private readonly ApplicationSetting _setting;
        private readonly ITokenService _tokenService;

        public UserService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection, ITokenService tokenService)
            : base(services)
        {
            _repo = new UserRepository(connection);
            _setting = setting;
            _tokenService = tokenService;
        }
        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = await _repo.GetAllUsers();

            var result = _mapper.Map<List<UserModel>>(users);

            return result;
        }

        public async Task<UserModel?> GetUserById(Guid userId)
        {
            var existingUser = await _repo.GetUserById(userId);
            if (existingUser == null)
            {
                throw new Exception("user not exist");
            }
            var dto = _mapper.Map<UserModel>(existingUser);

            return dto;
        }

        public async Task<User?> UpdateUser(Guid userId, UserDto userDto)
        {
            var existingUser = await _repo.GetUserById(userId);
            if (existingUser == null)
            {
                throw new Exception(" id not found");
            }

            var user = _mapper.Map(userDto, existingUser);

            var updatedUser = await _repo.UpdateUser(user);

            return user;
        }
        public async Task DeleteUser(Guid userId)
        {
            var existingUser = await _repo.GetUserById(userId);

            if (existingUser == null)
            {
                throw new Exception("user not exist"); 
            }

            await _repo.DeleteUser(existingUser);
        }

        public async Task<User?> CreateUser(UserDto newUser)
        {

            var existingUser = await _repo.GetUserByEmail(newUser.Email);
            if (existingUser != null)
            {
                throw new Exception("email đã tồn tại"); 
            }
            var user = _mapper.Map<User>(newUser);
            user.Id = Guid.NewGuid();

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, newUser.Password);

            return await _repo.Create(user);
        }

        public async Task<object> RegisterAsync(UserRegisterRequest request)
        {
            var existingUser = await _repo.GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                throw new Exception("Email đã tồn tại");
            }

            var defaultRole = await _repo.GetRoleByNameAsync("user");
            if (defaultRole == null)
            {
                throw new Exception("Role mặc định 'user' không tồn tại");
            }

            var user = _mapper.Map<User>(request);
            user.Id = Guid.NewGuid();
            user.RoleId = defaultRole.Id; 

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, request.Password);

            await _repo.Create(user); 

            var userRole = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = defaultRole.Id
            };
            await _repo.AssignRoleToUserAsync(userRole);

            return new
            {
                Message = "Đăng ký thành công",
                user.Id,
                user.Email
            };
        }
        public async Task<object> LoginAsync(UserLoginRequest request)
        {
            var user = await _repo.GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng");
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng");
            }

            var roles = await _repo.GetUserRoles(user.Id);

            var token = _tokenService.GenerateToken(user.Id.ToString(), user.Email, roles);

            return new
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                Roles = roles
            };
        }
        public async Task<User?> CreateUserWithRole(UserDto dto)
        {
            // Kiểm tra email trùng
            var existingUser = await _repo.GetUserByEmail(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email đã tồn tại");
            }

            // Kiểm tra RoleId có tồn tại không
            if (dto.RoleId == null)
            {
                throw new Exception("Vui lòng cung cấp RoleId");
            }

            var roleExists = await _repo.GetRoleByIdAsync(dto.RoleId.Value);
            if (roleExists == null)
            {
                throw new Exception("RoleId không tồn tại trong hệ thống");
            }

            // Tạo user
            var user = _mapper.Map<User>(dto);
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            await _repo.Create(user);

            var userRole = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = dto.RoleId.Value
            };
            await _repo.AssignRoleToUserAsync(userRole);

            return user;
        }
    }
}
