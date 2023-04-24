 namespace Metika.Security.Extensions;

using Metika.Security.Common.Settings;
using Metika.Security.Interfaces;
using Metika.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
public static class ConfigureJwtSettings
{
    /// <summary>
    /// adds security options from appsettings to services , generating public and private key into directories inside setting file
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddRsaJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var security = new SecurityOptions();
        // binding configuration from appsetting or secrets to security option variable
        configuration.Bind(nameof(SecurityOptions), security);
        // adding security option as singleton
        services.AddSingleton(security);
        AddRsaKeys(services, security);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var rsa = RSA.Create();
                string key = File.ReadAllText(security.PublicKeyFilePath);
                rsa.FromXmlString(key);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Auth Provider",
                    ValidAudience = "Auth Consumer",
                    IssuerSigningKey = new RsaSecurityKey(rsa)
                };
            });
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
    /// <summary>
    /// generating public and private keys for authentication into folders defined inside appsetting 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    private static IServiceCollection AddRsaKeys(IServiceCollection services, SecurityOptions options)
    {
        string keysFolder = Path.GetDirectoryName(options.PrivateKeyFilePath);
        if (!Directory.Exists(keysFolder)) Directory.CreateDirectory(keysFolder);
        var rsa = RSA.Create();
        string privateKeyXml = rsa.ToXmlString(true);
        string publicKeyXml = rsa.ToXmlString(false);
        using var privateFile = File.Create(options.PrivateKeyFilePath);
        using var publicFile = File.Create(options.PublicKeyFilePath);
        privateFile.Write(Encoding.UTF8.GetBytes(privateKeyXml));
        publicFile.Write(Encoding.UTF8.GetBytes(publicKeyXml));
        return services;
    }
}
