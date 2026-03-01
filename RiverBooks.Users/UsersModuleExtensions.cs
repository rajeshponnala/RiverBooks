using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Users.Data;
using Serilog;

namespace RiverBooks.Users;
public static class UsersModuleExtensions
{
  public static IServiceCollection AddUserModuleServices(
    this IServiceCollection services,
    ConfigurationManager config, ILogger logger, List<System.Reflection.Assembly> mediatRAssemblies)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");
    services.AddDbContext<UsersDbContext>(options =>
    {
      options.UseSqlServer(connectionString);
    });
    services.AddIdentityCore<ApplicationUser>(options => { })
            .AddEntityFrameworkStores<UsersDbContext>();

    mediatRAssemblies.Add(typeof(UsersModuleExtensions).Assembly);
    services.AddScoped<IApplicationUserRepository, EFApplicationUserRepository>();

    logger.Information("{Module} module services are registered", "Users"); 
    return services;
  }
}
