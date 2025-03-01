using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;


public record BookDto(Guid Id, string Title, string Author);
internal interface IBookService
{
   IEnumerable<BookDto> ListBooks();
}

internal class BookService : IBookService
{
    public IEnumerable<BookDto> ListBooks()
    {
        return [
            new BookDto(Guid.NewGuid(), "The Hobbit", "J.R.R. Tolkien"),
            new BookDto(Guid.NewGuid(), "The Fellowship of the Ring", "J.R.R. Tolkien"),
            new BookDto(Guid.NewGuid(), "The Two Towers", "J.R.R. Tolkien"),
            new BookDto(Guid.NewGuid(), "The Return of the King", "J.R.R. Tolkien")
            ];
    }
}

public static class BookEndPoints
{
    public static string GetBooks = "/books";
    public static string GetBookById = "api/books/{id}";
    public static string CreateBook = "api/books";
    public static string UpdateBook = "api/books/{id}";
    public static string DeleteBook = "api/books/{id}";
    
    public static void MapBookEndPoints(this WebApplication app)
    {
        app.MapGet(GetBooks, (IBookService bookService) => bookService.ListBooks());


    } 

}

public static class BookExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services)
    {
        services.AddSingleton<IBookService, BookService>();
        return services;
    }
}

