using Microsoft.EntityFrameworkCore;
using RestASPNET.API.Model;

namespace RestASPNET.API.Context;

public class MySqlContext : DbContext
{
    public MySqlContext()
    {
        
    }

    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

    public DbSet<Person> Person { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<User> User { get; set; }




}
