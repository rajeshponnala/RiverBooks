using System;
using System.Collections.Generic;
using System.Text;


namespace RiverBooks.Books.Contracts;

public record BookDetailsResponse(Guid BookId, string Title, string Author, decimal Price);

