using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

public class ApplicationUser : IdentityUser
{
  public string FullName { get; set; } = string.Empty;
  private readonly List<CartItem> _cartItems = [];
  public IReadOnlyCollection<CartItem> cartItems => _cartItems.AsReadOnly();

  public void AddItemToCart(CartItem item) {
    Guard.Against.Null(item); 
    var existingBook = _cartItems.FirstOrDefault(citem => citem.BookId == item.BookId);
    if (existingBook != null) {
      existingBook.updateQuantity(existingBook.Quantity + item.Quantity);
      existingBook.updateDescription(item.Description);
      existingBook.updateUnitPrice(item.UnitPrice);
      return;
    }
    _cartItems.Add(item);
  }

}

public class CartItem {
  public CartItem(Guid bookId,string description,int quantity, decimal unitPrice) {
       BookId = Guard.Against.Default(bookId);
       Description = Guard.Against.NullOrEmpty(description);
       Quantity = Guard.Against.Negative(quantity);
    UnitPrice = Guard.Against.Negative(unitPrice);
  }

  public Guid Id { get; set; } = Guid.NewGuid();
  public Guid  BookId { get; set; }
  public string Description { get; set; }
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }

  public void updateQuantity(int quantity) { 
     this.Quantity = Guard.Against.Negative(quantity);
  }

  internal void updateDescription(string description)
  {
    this.Description = Guard.Against.Null(description);
  }

  internal void updateUnitPrice(decimal unitPrice)
  {
    this.UnitPrice = Guard.Against.Negative(unitPrice);
  }
}
