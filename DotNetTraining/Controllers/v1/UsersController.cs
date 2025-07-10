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
<<<<<<< HEAD
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
=======
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        return Success(users);
    }

    // GET: api/users/{userId}
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
<<<<<<< HEAD
        var user = await _userService.GetUserById(userId);
        if (user == null)
        {
            return NotFound(new { message = "Không tìm thấy người dùng" });
        }
        return Success(user);
    }

    // POST: api/users/create
    [HttpPost("createUser")]
=======
        return Success(await _userService.GetUserById(userId));
    }

    // POST: api/users/create
    [HttpPost("create")]
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
    public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

<<<<<<< HEAD
        var result = await _service.CreateUser(dto);
        return CreatedSuccess(result);
=======
        return CreatedSuccess(await _userService.CreateUser(dto));
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
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
<<<<<<< HEAD
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
=======
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        return Success("Delete success");
    }
}
