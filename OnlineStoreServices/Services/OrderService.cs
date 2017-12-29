using OnlineStoreServices.Interfaces;
using OnlineStoreModels;
using System;
using System.Collections.Generic;
using OnlineStoreDataAccessLayer.Repositories;

namespace OnlineStoreServices.Services
{
    public class OrderService : IOrderService
    {
        readonly Repositories repositories;

        public OrderService(Repositories repositories)
        {
            this.repositories = repositories;
        }

        public IEnumerable<Order> GetUserOrders(string email)
        {
            try
            {
                return repositories.OrderRepository.GetUserOrders(email);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public Order GetOrder(int id)
        {
            try
            {
                return repositories.OrderRepository.GetOrder(id);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public void AddOrder(Order order)
        {
            try
            {
                repositories.OrderRepository.AddOrder(order);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                repositories.OrderRepository.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void DeleteOrder(int id)
        {
            repositories.OrderRepository.DeleteOrder(id);
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                repositories.OrderRepository.DeleteOrder(order);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }
    }
}
