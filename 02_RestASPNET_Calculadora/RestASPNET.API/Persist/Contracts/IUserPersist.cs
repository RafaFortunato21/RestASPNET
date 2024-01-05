using RestASPNET.API.Data.DTO;
using RestASPNET.API.Model;

namespace RestASPNET.API.Persist.Contracts;

public interface IUserPersist
{
    User ValidateCredentials(string userName, string password);
    void UpdateUser(User user);
    bool UserExists(User user);

}
