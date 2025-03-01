using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace RiverBooks.Books;

internal class ListBooksEndpoint(IBookService bookService) : EndpointWithoutRequest<ListBooksResponse>
{

    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct = default)
    {
        var books = _bookService.ListBooks();
        await SendAsync(new ListBooksResponse { Books = books }, cancellation: ct);
    }

}