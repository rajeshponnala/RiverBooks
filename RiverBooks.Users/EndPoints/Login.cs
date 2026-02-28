using Microsoft.AspNetCore.Identity;
using FastEndpoints;
using FastEndpoints.Security;

namespace RiverBooks.Users.EndPoints;

internal class Login(UserManager<ApplicationUser> userManager) : Endpoint<UserLoginRequest>
{
  public override void Configure()
  {
    Post("/users/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UserLoginRequest req, CancellationToken ct)
  {
    var user = await userManager.FindByEmailAsync(req.Email);
    if(user is null)
    {
       await SendUnauthorizedAsync(ct);
       return;
    }
    var loginSuccessful = await userManager.CheckPasswordAsync(user, req.Password);
    if (!loginSuccessful)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }
    var jwtSecret = Config["Auth:JwtSecret"]!;
    var token =  JwtBearer.CreateToken(options =>
    {
      options.SigningKey = jwtSecret;
      options.User["EmailAddress"] = user.Email!;
    });
    await SendAsync(token, cancellation: ct);
    return;
  }
}
