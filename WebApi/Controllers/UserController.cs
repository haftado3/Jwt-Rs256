using Metika.Identity.Entities;

namespace WebApi.Controllers;

using Metika.Identity.Abstraction;
using Metika.Identity.Services;
using Metika.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseUserController<User,Role>
{
    private readonly IUserService<User, Role> _userService;

    public UserController(IUserService<User, Role> userService) : base(userService)
    {
        _userService = userService;
    }
    [Authorize]
    [HttpGet("Validate")]
    public async Task<IActionResult> Validate()
    {
        return Ok();
    }
}
