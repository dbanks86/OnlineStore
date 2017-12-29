using OnlineStoreDataAccessLayer.Repositories;
using OnlineStoreModels;
using OnlineStoreServices.Interfaces;
using System;
using System.Collections.Generic;

namespace OnlineStoreServices.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        readonly Repositories repositories;

        public Repositories Repositories
        {
            get
            {
                return repositories;
            }
        }

        public OrderDetailService(Repositories repositories)
        {
            this.repositories = repositories;
        }

        public IEnumerable<OrderDetail> GetUserOrderDetails(string email)
        {
            try
            {
                return Repositories.OrderDetailRepository.GetUserOrderDetails(email);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public OrderDetail GetOrderDetail(int id)
        {
            try
            {
                return Repositories.OrderDetailRepository.GetOrderDetail(id);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public void AddOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                Repositories.OrderDetailRepository.AddOrderDetail(OrderDetail);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void UpdateOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                Repositories.OrderDetailRepository.UpdateOrderDetail(OrderDetail);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void DeleteOrderDetail(int id)
        {
            Repositories.OrderDetailRepository.DeleteOrderDetail(id);
        }

        public void DeleteOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                Repositories.OrderDetailRepository.DeleteOrderDetail(OrderDetail);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void SearchOrderDetails(string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
