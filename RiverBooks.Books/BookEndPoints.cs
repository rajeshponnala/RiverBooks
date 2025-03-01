using Microsoft.AspNetCore.Builder;

namespace RiverBooks.Books;

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

