using RestASPNET.API.Data.DTO;

namespace RestASPNET.API.Services
{
    public interface ILoginService
    {
        TokenDTO ValidateCredentials(string userName, string password);


    }
}
