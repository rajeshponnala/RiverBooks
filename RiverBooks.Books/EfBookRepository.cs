using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal class EfBookRepository(BookDbContext dbContext) : IBookRepository
{
  public Task AddAsync(Book book)
  {
    
    dbContext.Books.Add(book);
    
    return Task.CompletedTask;
  }
  public Task DeleteAsync(Book book)
  {
    dbContext.Books.Remove(book);
    return Task.CompletedTask;
  }
  public async Task<Book?> GetByIdAsync(Guid id)
  {
    return await dbContext!.Books.FindAsync(id);
  }

  public async Task<List<Book>> ListAsync()
  {
     return await dbContext.Books.ToListAsync();
  }

  public async Task SaveChangesAsync()
  {
    await dbContext.SaveChangesAsync();
  }

  public Task UpdateAsync(Book book)
  {
    dbContext.Books.Update(book);
    return Task.CompletedTask;
  }
}
