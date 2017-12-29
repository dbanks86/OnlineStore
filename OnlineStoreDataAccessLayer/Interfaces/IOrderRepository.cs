using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreDataAccessLayer.Interfaces
{
    interface IOrderRepository
    {
        IEnumerable<Order> GetUserOrders(string email);
        Order GetOrder(int id);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        void DeleteOrder(Order order);
    }
}
