using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AliHotel.Database;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Models;

namespace AliHotel.Domain.Services
{
    /// <summary>
    /// Service for orders. Implements IOrderService
    /// </summary>
    public class OrderService: IOrderService
    {
        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List of orders
        /// </summary>
        public List<Order> Orders => _context.Orders
            .Include(x => x.User)
            .Include(x => x.Room)
            .ThenInclude(x => x.RoomType)
            .ToList();

        /// <summary>
        /// Add order to database
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(OrderModel orderModel)
        {
            if (orderModel == null)
            {
                throw new NullReferenceException("Reference to order is null");
            }

            var resultOrder = new Order(orderModel.UserId, orderModel.RoomId, orderModel.ArrivalDate, orderModel.DepartureDate);
            await _context.Orders.AddAsync(resultOrder);
            await _context.SaveChangesAsync();
            return resultOrder.Id;
        }

        /// <summary>
        /// Pay for order and close it
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<decimal> PayOrder(Guid orderId)
        {
            var resultList = await _context.Orders
                .Include(x => x.User)
                .Include(x => x.Room)
                .ToListAsync();
            var resultOrder = resultList.SingleOrDefault(x => x.Id == orderId);
            if (resultOrder == null)
            {
                throw new NullReferenceException($"reference to order is null");
            }

            decimal resultSum = (resultOrder.DepartureDate - resultOrder.ArrivalDate).Days * (resultOrder.Room.RoomType.Price + (resultOrder.Room.Capacity - 1) * resultOrder.Room.RoomType.PricePerMen);
            _context.Orders.ToList().SingleOrDefault(x => x.Id == orderId).Bill = resultSum;
            //resultOrder.IsClosed = true;
            _context.Orders.ToList().SingleOrDefault(x => x.Id == orderId).IsClosed = true;
            await _context.SaveChangesAsync();
            return resultSum;
        }

        /// <summary>
        /// Edit order's departure day
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderModel"></param>
        /// <param name="newDepDate"></param>
        /// <returns></returns>
        public async Task EditDepartureDay(Guid orderId, OrderModel orderModel, DateTime newDepDate)
        {
            var resultList = await _context.Orders.ToListAsync();
            var resultOrder = resultList.SingleOrDefault(x => x.Id == orderId);
            if (resultOrder == null)
            {
                throw new NullReferenceException($"reference to order is null");
            }

            if ((resultOrder.DepartureDate - newDepDate).Days < 0)
            {
                throw new NullReferenceException($"New departure date is incorrect");
            }

            _context.Orders.ToList().SingleOrDefault(x => x.Id == orderId).DepartureDate = newDepDate;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get list of all orders from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetAsync()
        {
            return await _context.Orders
                .Include(x => x.User)
                .Include(x => x.Room)
                .ToListAsync();
        }
    }
}
