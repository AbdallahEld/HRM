using HR.Application.Common.Interfaces;
using HR.Application.Features.Account.Commands.RegisterEmployee;
using HR.Application.Features.Account.DTOs;
using HR.Domain.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMediator _mediator;
        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            IMediator mediator)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
        }

        [HttpPost("registerEployee")]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<IActionResult> Register([FromBody] RegisterEmployeeCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Created(string.Empty, new { Message = "Employee registered successfully." });
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
