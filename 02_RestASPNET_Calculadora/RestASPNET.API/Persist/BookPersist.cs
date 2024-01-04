using Microsoft.EntityFrameworkCore;
using RestASPNET.API.Context;
using RestASPNET.API.Model;
using RestASPNET.API.Persist.Contracts;

namespace RestASPNET.API.Persist
{
    public class BookPersist : GeralPersist, IBookPersist
    {
        private readonly MySqlContext _context;

        public BookPersist(MySqlContext context) : base(context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Book.AsNoTracking()
                .ToList();
        }

        public Book FindByID(long id)
        {
            return _context.Book.AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
        }

    }
}
