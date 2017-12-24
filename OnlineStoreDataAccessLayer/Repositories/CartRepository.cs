using OnlineStoreDataAccessLayer.Interfaces;
using OnlineStoreModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreDataAccessLayer.Repositories
{
    public class CartRepository : ICartRepository
    {   
        readonly OnlineStoreEntities dbContext;

        public CartRepository(OnlineStoreEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<CartItem> GetUserCartItems(string email, string includedTable = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(includedTable))
                {
                    return dbContext.CartItems.Where(cartItem => cartItem.Email == email).ToList();
                }
                else
                {
                    return dbContext.CartItems.Include(includedTable).Where(cartItem => cartItem.Email == email).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
                return null;
            }
        }

        public CartItem GetCartItem(int id)
        {
            try
            {
                return dbContext.CartItems.SingleOrDefault(cartItem => cartItem.CartItemID == id);
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
                return null;
            }
        }

        public CartItem GetCartItem(string email, int productId)
        {
            try
            {
                return dbContext.CartItems.SingleOrDefault(cartItem => cartItem.Email == email && cartItem.ProductID == productId);
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
                return null;
            }
        }

        public void AddCartItem(CartItem cartItem)
        {
            try
            {
                dbContext.CartItems.Add(cartItem);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            try
            {
                dbContext.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public void DeleteCartItem(int id)
        {
            var cartItem = GetCartItem(id);
            this.DeleteCartItem(cartItem);
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            try
            {
                dbContext.CartItems.Remove(cartItem);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        /// <summary>
        /// Assigns cart items to email enterned by anonymous user logs in or after an account is created
        /// </summary>
        /// <param name="email">email to assign cart items to</param>
        /// <param name="anonymousGuid">anonymousGuid of user regarding item added to cart prior to user logging in</param>
        public void MigrateCart(string email, string anonymousGuid)
        {
            //cart items of anonymous user and user logging in
            var cartItemsOfAnonymousUserAndUserLoggingIn = dbContext.CartItems.Where(ci => ci.Email == anonymousGuid || ci.Email == email).ToList();

            //cart items of anonymous user and user logging in filtered by anonymous user
            var anonymousCartItems = cartItemsOfAnonymousUserAndUserLoggingIn.Where(ci => ci.Email == anonymousGuid);

            //cart items of anonymous user and user logging in filtered by user logging in
            var userLoggingInCartItems = cartItemsOfAnonymousUserAndUserLoggingIn.Where(ci => ci.Email == email);

            //compare cart items of anonymous user to cart items of user logging in
            bool matchFound;
            foreach (CartItem anonymousCartItem in anonymousCartItems)
            {
                matchFound = false;

                foreach (CartItem userLoggingInCartItem in userLoggingInCartItems)
                {
                    //check if cart item product of anonymous user equals cart item product of user logging in
                    if (anonymousCartItem.ProductID == userLoggingInCartItem.ProductID)
                    {
                        //if productIDs are equals, increase cart item quantity of user logging in by cart item quantity of anonymous user
                        userLoggingInCartItem.Quantity += anonymousCartItem.Quantity;

                        //remove anonymous cart item once cart item quantity of user logging in is updated
                        dbContext.CartItems.Remove(anonymousCartItem);
                        matchFound = true;

                        //break out of nested foreach to prevent unnecessary iterations in nested foreach
                        break;
                    }
                }

                if (matchFound)
                {
                    continue;
                }
                else
                {
                    //if anonymous cart item does not exists in cart items of user logging in, update email of anonymous cart item to email of user logging in
                    anonymousCartItem.Email = email;
                }
            }
            dbContext.SaveChanges();
        }
    }
}
