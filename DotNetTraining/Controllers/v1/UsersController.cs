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

    public UsersController(IServiceProvider services, IHttpContextAccessor httpContextAccessor)
        : base(services, httpContextAccessor)
    {
        _userService = services.GetService<UserService>()!;
    }

    // GET: api/users/getAllUsers
    [HttpGet("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Success(users);
    }

    // GET: api/users/{userId}
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        return Success(await _userService.GetUserById(userId));
    }

    // POST: api/users/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return CreatedSuccess(await _userService.CreateUser(dto));
    }

    // PUT: api/users/{userId}
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Success(await _userService.UpdateUser(userId, dto));
    }

    // DELETE: api/users/{userId}
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await _userService.DeleteUser(userId);
        return Success("Delete success");
    }
}
