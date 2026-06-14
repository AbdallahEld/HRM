using HR.Application.Account.DTOs;
using HR.Application.Common.Interfaces;
using HR.Domain.Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            return Ok(model);
        }
        [HttpPost("login")]
        public async Task <IActionResult> Login(LoginDTO model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User == null)
            {
                return Unauthorized("Invalid email or password");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password");
            }
            var returnedUser = new UserDTO
            {
                DisplayName = User.UserName,
                Email = User.Email,
                Token = await _tokenService.GenerateTokenAsync(User),
            };
            return Ok(returnedUser);
        }
    }
}
