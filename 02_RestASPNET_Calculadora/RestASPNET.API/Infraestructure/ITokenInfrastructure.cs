using System.Security.Claims;

namespace RestASPNET.API.Infraestructure
{
    public interface ITokenInfrastructure
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
