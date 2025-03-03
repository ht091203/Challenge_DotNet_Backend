using System.Text;
using Application.Settings;
using BPMaster.Services;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/users")]
[ApiController]
public class UsersController : BaseV1Controller<UserService, ApplicationSetting>
{
    private readonly UserService _userService;
    public UsersController(IServiceProvider services, IHttpContextAccessor httpContextAccessor): base(services, httpContextAccessor)
    {
        this._userService = services.GetService<UserService>()!;
    }

    [HttpGet("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
            var users = await _userService.GetAllUsers();
            return Ok(users);

    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
            var user = await _userService.GetUserById(userId);
            return Ok(user);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto newUser)
    {
        try
        {
            var createdUser = await _userService.CreateUser(newUser);
            if (createdUser == null)
            {
                return BadRequest("thêm mới sản phẩm thất bại.");
            }

            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }

    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateProduct(Guid userId, [FromBody] UpdateUserDto userdto)
    {
        var updatedUser = await _userService.UpdateUser(userId, userdto);

        return updatedUser is null
            ? NotFound($"người dùng với id {userId} không tìm thấy.")
            : Ok(updatedUser);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        try
        {
            var deleted = await _userService.DeleteUser(userId);
            if (!deleted)
            {
                return NotFound($"người dùng với id {userId} không tìm thấy.");
            }
            return Ok($"người dùng với id {userId} đã xóa thành công.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }







}


