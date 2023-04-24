using Metika.Identity.Entities;
using Metika.Identity.Model;
using Metika.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Metika.Identity.Abstraction
{
    public class BaseUserController<TUser,TRole> : ControllerBase where TUser:User , new() where TRole : Role,new()
    {
        private readonly IUserService<TUser,TRole> _userService;

        public BaseUserController(IUserService<TUser, TRole> userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public virtual async Task<IActionResult> Login([FromBody] LoginWithPasswordRequestDto model)
        {
            var token = await _userService.LoginWithPassword(model);
            if (token == null)
            {
                return Problem();
            }

            return Ok(token);

        }

        [HttpPost]
        [Route("register")]
        public virtual async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var token = await _userService.Register(request);
            if (token == null)
            {
                return Problem();
            }

            return Ok(token);
        }
    }
}
