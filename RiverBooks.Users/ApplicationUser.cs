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
    var existingBook = _cartItems.FirstOrDefault(citem => citem.Id == item.Id);
    if (existingBook != null) {
      existingBook.updateQuantity(existingBook.Quantity + item.Quantity);
      // TODO: what if other details have been changed
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

  }

  public Guid Id { get; set; } = Guid.NewGuid();
  public Guid  BookId { get; set; }
  public string Description { get; set; }
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }

  public void updateQuantity(int quantity) { 
     this.Quantity = Guard.Against.Negative(quantity);
  }  
}
