using RestASPNET.API.Model;

namespace RestASPNET.API.Services;

public interface IBookService
{
    
    List<Book> FindAll();
    Book FindByID(long id);

    Book Create(Book book);
    Book Update(Book book);
    void Delete(long id);

}