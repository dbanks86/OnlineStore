using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreDataAccessLayer.Interfaces
{
    interface ICartRepository
    {
        IEnumerable<CartItem> GetUserCartItems(string email, string includedTable = "");
        CartItem GetCartItem(int id);
        CartItem GetCartItem(string email, int productId);
        void AddCartItem(CartItem cartItem);
        void UpdateCartItem(CartItem cartItem);
        void DeleteCartItem(int id);
        void DeleteCartItem(CartItem cartItem);
        void MigrateCart(string email, string anonymousGuid);
    }
}
