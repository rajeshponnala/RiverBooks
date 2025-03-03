using FastEndpoints;

namespace RiverBooks.Books.BookEndPoints;

public record UpdateBookPriceRequest(Guid Id, decimal NewPrice);
internal class UpdateBookPriceEndPoint(IBookService bookService) : Endpoint<UpdateBookPriceRequest, BookDto>
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
