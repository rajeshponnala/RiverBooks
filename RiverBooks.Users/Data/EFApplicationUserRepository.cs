using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users.Data;

internal class EFApplicationUserRepository(UsersDbContext dbContext) : IApplicationUserRepository
{
  private readonly UsersDbContext _dbContext = dbContext;
  public Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
  {
    return _dbContext.ApplicationUsers.Include(i => i.cartItems).SingleAsync(user => user.Email == email);
  }

  public async Task SaveChangesAsync()
  {
    await _dbContext.SaveChangesAsync();
  }
}
