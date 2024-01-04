using Microsoft.EntityFrameworkCore;
using RestASPNET.API.Context;
using RestASPNET.API.Model;
using RestASPNET.API.Persist.Contracts;

namespace RestASPNET.API.Persist
{
    public class PersonPersist : GeralPersist, IPersonPersist
    {
        private readonly MySqlContext _context;

        public PersonPersist(MySqlContext context) : base(context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Person.AsNoTracking()
                .ToList();
        }

        public Person FindByID(long id)
        {
            return _context.Person.AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
        }

    }
}
