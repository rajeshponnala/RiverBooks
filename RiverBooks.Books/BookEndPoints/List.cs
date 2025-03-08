using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace RiverBooks.Books.BookEndPoints;

public class ListBooksResponse
{
  public IEnumerable<BookDto> Books { get; set; } = [];
}

internal class List(IBookService bookService) : EndpointWithoutRequest<ListBooksResponse>
{

  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Get("/books");
    AllowAnonymous();
  }
  public override async Task HandleAsync(CancellationToken ct = default)
  {
    var books = await _bookService.ListBooksAsync();
    await SendAsync(new ListBooksResponse { Books = books }, cancellation: ct);
  }

}
