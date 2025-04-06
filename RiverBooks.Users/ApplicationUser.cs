using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

public class ApplicationUser : IdentityUser
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;

  public string Address { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
}
