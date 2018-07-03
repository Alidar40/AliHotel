using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Models;

namespace AliHotel.Domain.Interfaces
{
    /// <summary>
    /// Interface for working with orders
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// List of orders
        /// </summary>
        List<Order> Orders { get; }

        /// <summary>
        /// Returns most appropriate room for order
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        Task<Room> FindRoom(OrderModel orderModel);

        /// <summary>
        /// Add order to database
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        Task<Order> AddAsync(OrderModel orderModel);

        /// <summary>
        /// Close order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<decimal> PayOrder(Guid orderId);

        /// <summary>
        /// Edit order's departure day
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newDepDate">New departure day</param>
        /// <returns></returns>
        Task EditDepartureDay(Guid orderId, DateTime newDepDate);

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        Task<List<Order>> GetAsync();

        /// <summary>
        /// Returns order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Order> FindByIdAsync(Guid id);
    }
}
