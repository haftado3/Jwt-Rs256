using Metika.Identity.Entities;
using Metika.Identity.Exceptions;
using Metika.Identity.Model;
using Metika.Security.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Metika.Identity.Services
{
    public class UserService<TUser,TRole> : IUserService<TUser,TRole> where TUser : User ,new() where TRole : Role,new()
    {
        private readonly UserManager<TUser> _userManager;
        //private readonly RoleManager<TRole> _roleManager;
        private readonly IJwtService _jwtService;
        public UserService(UserManager<TUser> userManager,IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<string> Register(RegisterRequestDto request)
        {
            TUser? userExists = await _userManager.FindByNameAsync(request.Username);
            bool byEmail = request.Username.Contains("@");
            if (userExists != null)
                throw new UserExistException();

            var user = new TUser()
            {
                Email = byEmail?request.Username : "",
                UserName = request.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = !byEmail?request.Username: ""
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new RegisterUserException();

            return await GetToken(user);
        }
        public async Task<string> LoginWithPassword(LoginWithPasswordRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return await GetToken(user);
            }

            throw new UnauthorizedException();
        }

        private async Task<string> GetToken(TUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = await _jwtService.Generate(authClaims);

            return token;
        }

        public async Task<string> LoginWithVerification(LoginWithVerificationRequestDto request)
        {
            return "";
        }
        public async Task<bool> SendSmsVerificationCode(SmsVerificationRequestDto request)
        {
            return false;
        }
        public async Task<bool> SendEmailVerificationCode(EmailVerificationRequestDto request)
        {
            return false;
        }
    }
}
