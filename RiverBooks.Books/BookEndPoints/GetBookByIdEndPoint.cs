using FastEndpoints;

namespace RiverBooks.Books.BookEndPoints;

public record GetBookByIdRequest(Guid Id);
internal class GetBookByIdEndPoint(IBookService bookService) : Endpoint<GetBookByIdRequest, BookDto>
{
  public override void Configure()
  {
    Get("/books/{id}");
    AllowAnonymous();

  }

  public async override Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
  {
    var book = await bookService.GetBookByIdAsync(req.Id);
    if (book == null)
    {
      await SendNotFoundAsync();
      return;
    }
    await SendAsync(book, cancellation: ct);
  }
}
