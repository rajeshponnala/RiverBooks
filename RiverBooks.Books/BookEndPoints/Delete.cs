using FastEndpoints;

namespace RiverBooks.Books.BookEndPoints;

public record DeleteBookRequest(Guid Id);
internal class Delete(IBookService bookService) : Endpoint<DeleteBookRequest>
{
  public override void Configure()
  {
    Delete("/books/{id}");
    AllowAnonymous();
  }
  public async override Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
  {
    // TODO: Implement Not Found
    await bookService.DeleteBookAsync(req.Id);
    await SendNoContentAsync(ct);
  }
}
