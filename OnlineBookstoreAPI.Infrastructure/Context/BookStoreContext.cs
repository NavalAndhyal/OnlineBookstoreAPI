using Microsoft.EntityFrameworkCore;
using OnlineBookstoreAPI.Domain.Models;
using OnlineBookstoreAPI.Domain.Models.Books;
using System.Reflection;

namespace OnlineBookstoreAPI.Infrastructure.Context
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
