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
    public UsersController(
            IServiceProvider services,
            IHttpContextAccessor httpContextAccessor

            )
            : base(services, httpContextAccessor)
    {
        this._userService = services.GetService<UserService>()!;
    }

    [HttpGet("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {

            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        try
        {
            var user = await _userService.GetUserById(userId);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] User user)
    {
        try
        {
            var updatedUser = await _userService.UpdateUser(userId, user);
            if (updatedUser == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        try
        {
            var deleted = await _userService.DeleteUser(userId);
            if (!deleted)
            {
                return NotFound($"User with ID {userId} not found.");
            }
            return Ok($"User with ID {userId} has been deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto newUser)
    {
        try
        {
            var createdUser = await _userService.CreateUser(newUser);
            if (createdUser == null)
            {
                return BadRequest("Failed to create user.");
            }
            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }




}


