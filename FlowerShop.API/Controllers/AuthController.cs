using FlowerShop.Core.DTOs.AuthDTOs;
using FlowerShop.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        public AuthController(IAuthService authService,IValidator<RegisterDto> registerValidator, IValidator<LoginDto> loginValidator)
        {
            _authService = authService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var validationResult = await _registerValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Messages = errors });
            }

            var errorMessage = await _authService.RegisterAsync(dto);

            if (errorMessage != null)
                return BadRequest(new { Message = errorMessage });

            return Ok(new { Message = "Đăng ký thành công!" });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var validationResult = await _loginValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Messages = errors });
            }
            var response = await _authService.LoginAsync(dto);
            if (response ==  null)
            {
                return Unauthorized(new { Message = "Sai tên đăng nhập hoặc mật khẩu!" });
            }
            return Ok(response);
        }
    }
}
