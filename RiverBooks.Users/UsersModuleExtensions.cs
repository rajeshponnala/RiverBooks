using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.Users;
public static class UsersModuleExtensions
{
  public static IServiceCollection AddUserModuleServices(
    this IServiceCollection services,
    ConfigurationManager config, ILogger logger)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");
    services.AddDbContext<UsersDbContext>(options =>
    {
      options.UseSqlServer(connectionString);
    });
    services.AddIdentityCore<ApplicationUser>(options => { })
            .AddEntityFrameworkStores<UsersDbContext>();
    logger.Information("{Module} module services are registered", "Users"); 
    return services;
  }
}
