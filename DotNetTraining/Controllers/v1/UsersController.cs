using Application.Settings;
using BPMaster.Services;
using Common.Controllers;
using Common.Loggers.Interfaces;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Common.Loggers.Interfaces;

[Route("api/users")]
[ApiController]
//[Authorize(Roles = "user")]
[Authorize(Roles = "admin")]
//[Authorize]
public class UsersController : BaseV1Controller<UserService, ApplicationSetting>
{
    private readonly UserService _userService;
    private readonly ILogManager _logger;

    public UsersController(IServiceProvider services, IHttpContextAccessor httpContextAccessor)
        : base(services, httpContextAccessor)
    {
        _userService = services.GetService<UserService>()!;
        _logger = services.GetService<ILogManager>()!;
    }
    [HttpGet("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        _logger.Info("[GET] Get all users", "USER");
        try
        {
            var users = await _userService.GetAllUsers();
            _logger.Info($"Returned {users.Count} users", "USER");
            return Success(users);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to get all users", "USER");
            return StatusCode(500, new { message = "Đã xảy ra lỗi hệ thống", detail = ex.Message });
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        _logger.Info($"[GET] Get user by ID: {userId}", "USER");
        try
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                _logger.Warn($"User not found: {userId}", "USER");
                return NotFound(new { message = "Không tìm thấy người dùng" });
            }

            return Success(user);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to get user {userId}", "USER");
            return StatusCode(500, new { message = "Lỗi khi lấy người dùng", detail = ex.Message });
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
    {
        _logger.Info($"[POST] Create user: {dto.Email}", "USER");

        if (!ModelState.IsValid)
        {
            _logger.Warn("Invalid model state", "USER");
            return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
        }

        try
        {
            var result = await _service.CreateUserWithRole(dto);
            return CreatedSuccess(new
            {
                result.Id,
                result.Email,
                result.Name,
                result.RoleId
            });
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to create user {dto.Email}", "USER");
            return StatusCode(500, new { message = "Lỗi khi tạo người dùng", detail = ex.Message });
        }
    }


    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserDto dto)
    {
        _logger.Info($"[PUT] Update user: {userId}", "USER");

        if (!ModelState.IsValid)
        {
            _logger.Warn("Invalid update payload", "USER");
            return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
        }

        try
        {
            var user = await _userService.UpdateUser(userId, dto);
            if (user == null)
            {
                _logger.Warn($"Cannot find user to update: {userId}", "USER");
                return NotFound(new { message = "Không tìm thấy người dùng để cập nhật" });
            }

            _logger.Info($"Updated user: {userId}", "USER");
            return Success(user);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to update user {userId}", "USER");
            return StatusCode(500, new { message = "Lỗi khi cập nhật người dùng", detail = ex.Message });
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        _logger.Info($"[DELETE] Delete user: {userId}", "USER");

        try
        {
            var existingUser = await _userService.GetUserById(userId);
            if (existingUser == null)
            {
                _logger.Warn($"User not found for delete: {userId}", "USER");
                return NotFound(new { message = "Không tìm thấy người dùng để xoá" });
            }

            await _userService.DeleteUser(userId);
            _logger.Info($"Deleted user: {userId}", "USER");
            return Success("Xoá thành công");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to delete user {userId}", "USER");
            return StatusCode(500, new { message = "Lỗi khi xoá người dùng", detail = ex.Message });
        }
    }
}
