using OnlineStoreDataAccessLayer.Repositories;
using OnlineStoreModels;
using OnlineStoreServices.Interfaces;
using System;
using System.Collections.Generic;

namespace OnlineStoreServices.Services
{
    public class CartService : ICartService
    {
        readonly Repositories repositories;

        public CartService(Repositories repositories)
        {
            this.repositories = repositories;
        }

        public IEnumerable<CartItem> GetUserCartItems(string email, string includedTable = "")
        {
            try
            {
                return repositories.CartRepository.GetUserCartItems(email, includedTable);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public CartItem GetCartItem(int id)
        {
            try
            {
                return repositories.CartRepository.GetCartItem(id);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public CartItem GetCartItem(string email, int productId)
        {
            try
            {
                return repositories.CartRepository.GetCartItem(email, productId);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public void AddCartItem(CartItem cartItem)
        {
            try
            {
                repositories.CartRepository.AddCartItem(cartItem);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            try
            {
                repositories.CartRepository.UpdateCartItem(cartItem);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void DeleteCartItem(int id)
        {
            repositories.CartRepository.DeleteCartItem(id);
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            try
            {
                repositories.CartRepository.DeleteCartItem(cartItem);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        /// <summary>
        /// Assigns cart items to email enterned by anonymous user logs in or after an account is created
        /// </summary>
        /// <param name="email">email to assign cart items to</param>
        /// <param name="anonymousGuid">anonymousGuid of user regarding item added to cart prior to user logging in</param>
        public void MigrateCart(string email, string anonymousGuid)
        {
            repositories.CartRepository.MigrateCart(email, anonymousGuid);
        }
    }
}
