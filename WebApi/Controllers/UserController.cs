namespace WebApi.Controllers;
using Metika.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public UserController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpGet("CreateToken")]
    public async Task<IActionResult> CreateToken()
    {
        var token = await _jwtService.Generate(new List<Claim>());
        return Ok(token);
    }
    [Authorize]
    [HttpGet("Validate")]
    public async Task<IActionResult> Validate()
    {
        return Ok();
    }
}
