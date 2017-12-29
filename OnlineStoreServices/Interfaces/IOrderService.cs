using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreServices.Interfaces
{
    interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string email);
        Order GetOrder(int id);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        void DeleteOrder(Order order);
    }
}
