using OnlineStoreDataAccessLayer.Interfaces;
using OnlineStoreModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreDataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly OnlineStoreEntities dbContext;

        public OrderRepository(OnlineStoreEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Order> GetUserOrders(string email)
        {
            try
            {
                return dbContext.Orders.Where(order => order.Email == email).ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
                return null;
            }
        }

        public Order GetOrder(int id)
        {
            try
            {
                return dbContext.Orders.SingleOrDefault(order => order.OrderID == id);
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
                return null;
            }
        }

        public void AddOrder(Order order)
        {
            try
            {
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                dbContext.Entry(order).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrder(id);
            this.DeleteOrder(order);
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                dbContext.Orders.Remove(order);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }
    }
}
