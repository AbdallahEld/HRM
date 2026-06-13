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
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                Token = Guid.NewGuid().ToString() // This is a placeholder. In a real application, you would generate a JWT or similar token here.
            };
            return Ok(returnedUser);
        }
    }
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserDTO
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
