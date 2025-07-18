using Application.Settings;
using Common.Controllers;
using Common.Loggers.Interfaces;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : BaseV1Controller<UserService, ApplicationSetting>
{
    private readonly UserService _userService;
    private readonly ILogManager _logger;

    public AuthController(IServiceProvider services, IHttpContextAccessor httpContextAccessor)
        : base(services, httpContextAccessor)
    {
        _userService = services.GetService<UserService>()!;
        _logger = services.GetService<ILogManager>()!;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        _logger.Info($"[POST] Login attempt: {request.Email}", "AUTH");

        if (!ModelState.IsValid)
        {
            _logger.Warn("Invalid login payload", "AUTH");
            return BadRequest(new { message = "Dữ liệu đăng nhập không hợp lệ", errors = ModelState });
        }

        try
        {
            var result = await _userService.LoginAsync(request);

            if (result == null)
            {
                _logger.Warn($"Login failed for: {request.Email}", "AUTH");
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng" });
            }

            _logger.Info($"Login success for: {request.Email}", "AUTH");
            return Success(result);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Login error for: {request.Email}", "AUTH");
            return StatusCode(500, new { message = "Đăng nhập thất bại", detail = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        _logger.Info($"[POST] Register attempt: {request.Email}", "AUTH");

        if (!ModelState.IsValid)
        {
            _logger.Warn("Invalid register payload", "AUTH");
            return BadRequest(new { message = "Dữ liệu đăng ký không hợp lệ", errors = ModelState });
        }

        try
        {
            var user = await _userService.RegisterAsync(request);
            _logger.Info($"Register success: {request.Email}", "AUTH");
            return CreatedSuccess(user);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Register error for: {request.Email}", "AUTH");
            return StatusCode(500, new { message = "Đăng ký thất bại", detail = ex.Message });
        }
    }
}
