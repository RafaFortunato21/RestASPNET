using RestASPNET.API.Model;

namespace RestASPNET.API.Persist.Contracts
{
    public interface IPersonPersist : IGeralPersist
    {
        Person FindByID(long id);

        List<Person> FindAll();
    }
}
