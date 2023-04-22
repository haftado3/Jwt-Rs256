namespace Metika.Security.Services;

using Metika.Security.Common.Settings;
using Metika.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

public class JwtService : IJwtService
{
    private readonly SecurityOptions _options;

    public JwtService(SecurityOptions options)
    {
        _options = options;
    }

    public async Task<string> Generate(List<Claim> claims)
    {
        var rsa = RSA.Create();
        string key = await File.ReadAllTextAsync(_options.PrivateKeyFilePath);
        rsa.FromXmlString(key);
        var credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
        var jwt = new JwtSecurityToken(
            new JwtHeader(credentials),
            new JwtPayload(
                "Auth Provider",
                "Auth Consumer",
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(3)
            )
        );
        string token = new JwtSecurityTokenHandler()
        .WriteToken(jwt);
        return token;
    }
}