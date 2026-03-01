using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Users;

public class UsersDbContext : IdentityDbContext<IdentityUser>
{
  public UsersDbContext(DbContextOptions<UsersDbContext> options)
      : base(options)
  {
  }

  public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema("Users");
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(builder);
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder
       .Properties<decimal>()
       .HavePrecision(18,6);
  }
}

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
  public void Configure(EntityTypeBuilder<CartItem> builder)
  {
    builder.Property(item => item.Id).ValueGeneratedNever();
  }
}
