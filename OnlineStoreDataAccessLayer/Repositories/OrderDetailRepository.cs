using OnlineStoreDataAccessLayer.Interfaces;
using OnlineStoreModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreDataAccessLayer.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        readonly OnlineStoreEntities dbContext;

        public OrderDetailRepository(OnlineStoreEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<OrderDetail> GetUserOrderDetails(string email)
        {
            try
            {
                return (from od in dbContext.OrderDetails
                        join o in dbContext.Orders on od.OrderID equals o.OrderID
                        where o.Email == email
                        select od).ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
                return null;
            }
        }

        public OrderDetail GetOrderDetail(int id)
        {
            try
            {
                return dbContext.OrderDetails.SingleOrDefault(OrderDetail => OrderDetail.OrderDetailID == id);
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
                return null;
            }
        }

        public void AddOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                dbContext.OrderDetails.Add(OrderDetail);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public void UpdateOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                dbContext.Entry(OrderDetail).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public void DeleteOrderDetail(int id)
        {
            var OrderDetail = GetOrderDetail(id);
            this.DeleteOrderDetail(OrderDetail);
        }

        public void DeleteOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                dbContext.OrderDetails.Remove(OrderDetail);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public IEnumerable<SearchOrderDetails_Result> SearchOrderDetails(string email, string searchString)
        {
            return dbContext.SearchOrderDetails(email, searchString).ToList();
        }
    }
}
