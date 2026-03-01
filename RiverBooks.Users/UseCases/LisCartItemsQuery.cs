using Ardalis.Result;
using MediatR;
using RiverBooks.Users.CartEndpoints;

namespace RiverBooks.Users.UseCases;

public record ListCartItemsQuery(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;

internal class ListCartItemsQueryHandler(IApplicationUserRepository userRepository) : IRequestHandler<ListCartItemsQuery, Result<List<CartItemDto>>>
{
  private readonly IApplicationUserRepository _userRepository = userRepository;

  public async Task<Result<List<CartItemDto>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
    if (user is null) {
      return Result.Unauthorized();  
    }
    return user.cartItems.Select(item => new CartItemDto(item.Id, item.BookId, item.Description, item.Quantity, item.UnitPrice))
                         .ToList();
  }
}

