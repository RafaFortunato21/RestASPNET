using RestASPNET.API.Context;
using RestASPNET.API.Persist.Contracts;
using RestASPNET.API.Services;

namespace RestASPNET.API.Persist
{
    public class GeralPersist : IGeralPersist
    {
        private readonly MySqlContext _context;

        public GeralPersist(MySqlContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}
