using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreDataAccessLayer.Interfaces
{
    interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetUserOrderDetails(string email);
        OrderDetail GetOrderDetail(int id);
        void AddOrderDetail(OrderDetail OrderDetail);
        void UpdateOrderDetail(OrderDetail OrderDetail);
        void DeleteOrderDetail(int id);
        void DeleteOrderDetail(OrderDetail OrderDetail);
        IEnumerable<SearchOrderDetails_Result> SearchOrderDetails(string email, string searchString);
    }
}
 