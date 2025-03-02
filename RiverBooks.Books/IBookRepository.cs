namespace RiverBooks.Books;

internal interface IBookRepository: IReadOnlyBookRepository
{
  Task AddAsync(Book book);
  Task UpdateAsync(Book book);
  Task DeleteAsync(Guid id);

  Task SaveChangesAsync();
}
