using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreServices.Interfaces
{
    interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetUserOrderDetails(string email);
        OrderDetail GetOrderDetail(int id);
        void AddOrderDetail(OrderDetail OrderDetail);
        void UpdateOrderDetail(OrderDetail OrderDetail);
        void DeleteOrderDetail(int id);
        void DeleteOrderDetail(OrderDetail OrderDetail);
        void SearchOrderDetails(string searchString);
    }
}
