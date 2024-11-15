using LearnLogic.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LearnLogic.Infra.CrossCutting.Extensions;
using LearnLogic.Domain.Interfaces.Application;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LearnLogic.Domain.DTO;

namespace LearnLogic.Services.Controllers
{
    [ApiController]
    public class AuthController : BaseCQRSController
    {
        private readonly IAuthService _authService;
        private readonly IUserApplication _userApplication;

        public AuthController(IAuthService authService, IUserApplication userApplication)
        {
            _userApplication = userApplication;
            _authService = authService;
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserViewModel userViewModel)
        {
            var registrationResult = await _authService.RegisterUser(userViewModel);
            if (!registrationResult.IsValid)
            {
                return CustomResponse(registrationResult);
            }

            var user = _userApplication.GetUserByEmail(userViewModel.Email.ToLower());
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            ClearUserPassword(user);
            var token = _authService.GenerateToken(user);

            return Ok(new { token, user });
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserViewModel userViewModel)
        {
            var registrationResult = await _authService.UpdateUser(userViewModel);
            if (!registrationResult.IsValid)
            {
                return CustomResponse(registrationResult);
            }

            return Ok();
        }

        [HttpPost("login"), AllowAnonymous]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            var user = _userApplication.GetUserByEmail(loginViewModel.Email);
            if (user == null || !loginViewModel.Password.GenerateHash().Equals(user.Password))
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            ClearUserPassword(user);
            var token = _authService.GenerateToken(user);

            return Ok(new { token, user });
        }

        private static void ClearUserPassword(UserDTO user)
        {
            user.Password = string.Empty;
            user.ConfirmPassword = string.Empty;
        }
    }
}
