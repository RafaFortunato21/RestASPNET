using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RestASPNET.API.Configurations;
using RestASPNET.API.Data.DTO;
using RestASPNET.API.Infraestructure;
using RestASPNET.API.Persist.Contracts;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace RestASPNET.API.Services.Implementations
{
    public class LoginServiceImplementation : ILoginService
    {

        private readonly IUserPersist _userPersist;
        private TokenConfiguration _configuration;
        private readonly ITokenInfrastructure _tokenInfrastructure;

        public LoginServiceImplementation(IUserPersist userPersist, TokenConfiguration configuration, ITokenInfrastructure tokenInfrastructure)
        {
            _userPersist = userPersist;
            _configuration = configuration;
            _tokenInfrastructure = tokenInfrastructure;
        }


        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public TokenDTO ValidateCredentials(string userName, string password)
        {
            var user = _userPersist.ValidateCredentials(userName, password);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, userName)
            };

            var accessToken = _tokenInfrastructure.GenerateAccessToken(claims);
            var refreshToken = _tokenInfrastructure.GenerateRefreshToken();

            user.RefresToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _userPersist.UpdateUser(user);
            
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenDTO
            (
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );

        }
    }

}
