using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;
public class BookDbContext : DbContext
{
  public BookDbContext(DbContextOptions options) : base(options)
  {
  }

  internal DbSet<Book> Books { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema("books");
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18,6);
  }

}
