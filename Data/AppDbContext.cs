using Microsoft.EntityFrameworkCore;
using ExampleCrud.Model;
using ExampeCrud.Models;

namespace ExampleCrud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options) { }

        public DbSet<Product> Products {get; set;}
        public DbSet<Users> Users {get; set;}
    }
}