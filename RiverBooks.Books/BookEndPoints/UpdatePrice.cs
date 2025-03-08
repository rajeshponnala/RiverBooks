using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RiverBooks.Books.BookEndPoints;

public record UpdateBookPriceRequest(Guid Id, decimal NewPrice);

public class UpdateBookPriceValidator : Validator<UpdateBookPriceRequest>
{
  public UpdateBookPriceValidator()
  {
    RuleFor(x => x.Id)
      .NotNull()
      .NotEqual(Guid.Empty)
      .WithMessage("A book id is required");
    RuleFor(x => x.NewPrice)
      .GreaterThanOrEqualTo(0)
      .WithMessage("Book prices may not be negative.");
  }
}
internal class UpdatePrice(IBookService bookService) : Endpoint<UpdateBookPriceRequest, BookDto>
{
  public override void Configure()
  {
    Post("/books/{Id}/pricehistory");
    AllowAnonymous();
  }
  public async override Task HandleAsync(UpdateBookPriceRequest req, CancellationToken ct)
  {
    // TODO: Implement Not Found
    await bookService.UpdateBookPriceAsAsync(req.Id, req.NewPrice);
    var updatedBook = await bookService.GetBookByIdAsync(req.Id);
    await SendAsync(updatedBook, cancellation: ct);
  }
}
