using RestASPNET.API.Model;

namespace RestASPNET.API.Persist.Contracts
{
    public interface IBookPersist : IGeralPersist
    {
        List<Book> FindAll();
        Book FindByID(long id);
    }
}
