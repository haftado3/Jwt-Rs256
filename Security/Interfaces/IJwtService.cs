namespace Metika.Security.Interfaces;
using System.Security.Claims;
public interface IJwtService
{
    public Task<string> Generate(List<Claim> claims);
}
