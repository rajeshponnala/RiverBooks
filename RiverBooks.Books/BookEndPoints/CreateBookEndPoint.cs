using FastEndpoints;

namespace RiverBooks.Books.BookEndPoints;

internal class CreateBookRequest
{
  public Guid? Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Author { get; set; } = string.Empty;
  public decimal Price { get; set; }
}
internal class CreateBookEndPoint(IBookService bookService) : Endpoint<CreateBookRequest, BookDto>
{
  public override void Configure()
  {
    Post("/books");
    AllowAnonymous();
  }
  public async override Task HandleAsync(CreateBookRequest req, CancellationToken ct)
  {
    var book = new BookDto(
      req.Id ?? Guid.NewGuid(),
      req.Title,
      req.Author,
      req.Price);
    await bookService.CreateBookAsync(book);
    await SendCreatedAtAsync<GetBookByIdEndPoint>(
      new { book.Id },
      book,
      cancellation: ct);
  }
}
