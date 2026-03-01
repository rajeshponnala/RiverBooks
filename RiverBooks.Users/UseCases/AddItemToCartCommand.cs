using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases;

public record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress): IRequest<Result>;

public class AddItemToCartHandler(IApplicationUserRepository userRepository) : IRequestHandler<AddItemToCartCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository = userRepository;

  public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
    if (user is null) { 
       return Result.Unauthorized();
    }
    var newCartItem = new CartItem(request.BookId, "Description", request.Quantity, 1.00m);
    user.AddItemToCart(newCartItem);
    await _userRepository.SaveChangesAsync();
    return Result.Success();
  }
}
