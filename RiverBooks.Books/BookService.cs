
namespace RiverBooks.Books;

internal class BookService : IBookService
{
  private readonly IBookRepository _bookRepository;

  public BookService(IBookRepository bookRepository)
  {
    _bookRepository = bookRepository;
  }

  public async Task CreateBookAsync(BookDto newBook)
  {
    var book = new Book(newBook.Id,newBook.Title, newBook.Author, newBook.Price);
    await _bookRepository.AddAsync(book);
    await _bookRepository.SaveChangesAsync();
  }

  public async Task DeleteBookAsync(Guid id)
  {
    var bookToDelete = await _bookRepository.GetByIdAsync(id);
    if (bookToDelete != null) {
      await _bookRepository.DeleteAsync(id);
      await _bookRepository.SaveChangesAsync();
    }
  }

  public async Task<BookDto> GetBookByIdAsync(Guid id)
  {
    var book = await _bookRepository.GetByIdAsync(id);
    // TODO: Handle not found case later
    
    return new BookDto(book!.Id,book.Title, book.Author,book.Price);
  }

  public async Task<List<BookDto>> ListBooksAsync()
    {
        var books = (await _bookRepository.ListAsync())
      .Select(book => new BookDto(book.Id, book.Title, book.Author, book.Price));
       return books.ToList();
  }

  public async Task UpdateBookPriceAsAsync(Guid bookId, decimal newPrice)
  {
    // Validate Price

    var book = await _bookRepository.GetByIdAsync(bookId);

    // handle not found case

    book!.UpdatePrice(newPrice);
    await _bookRepository.SaveChangesAsync();
  }
}

