using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;

namespace RiverBooks.Users.UseCases;

public record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress): IRequest<Result>;

public class AddItemToCartHandler(IApplicationUserRepository userRepository, IMediator mediator) : IRequestHandler<AddItemToCartCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository = userRepository;
  private readonly IMediator _mediator = mediator;

  public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
    if (user is null) { 
       return Result.Unauthorized();
    }
    var result  = await _mediator.Send(new BookDetailsQuery(request.BookId), cancellationToken);
    if(result.Status == ResultStatus.NotFound) return Result.NotFound();
    var bookDetails = result.Value;
    string description = $"{bookDetails.Title} by {bookDetails.Author}";
    var newCartItem = new CartItem(request.BookId, description, request.Quantity, bookDetails.Price);
    user.AddItemToCart(newCartItem);
    await _userRepository.SaveChangesAsync();
    return Result.Success();
  }
}
