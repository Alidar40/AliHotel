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

        public List<Room> Rooms => _context.Rooms
            .Include(x => x.RoomType)
            .ToList();

        /// <summary>
        /// Returns most appropriate room for order
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public async Task<Room> FindRoom(OrderModel orderModel)
        {
            if (orderModel == null)
            {
                throw new NullReferenceException("Reference to order is null");
            }

            Room appropriateRoom = Rooms.FirstOrDefault(r => r.IsOccupied == false &&
                        r.RoomType.Name == orderModel.RoomTypeName && 
                        r.Capacity >= orderModel.PeopleCount);

            return appropriateRoom;
        }

        /// <summary>
        /// Add order to database
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(OrderModel orderModel)
        {
            Room room = await FindRoom(orderModel);

            if(room == null)
            {
                return false;
            }

            //var resultOrder = new Order(orderModel.UserId, orderModel.RoomId, orderModel.ArrivalDate, orderModel.DepartureDate);
            var resultOrder = new Order(orderModel.UserId, room.Id, orderModel.ArrivalDate, orderModel.DepartureDate, orderModel.PeopleCount);
            await _context.Orders.AddAsync(resultOrder);

            var user = await _context.Users.SingleAsync(u => u.Id == orderModel.UserId);
            user.IsRenter = true;

            var roomToChange = await _context.Rooms.FirstAsync(r => r.Id == room.Id);
            roomToChange.IsOccupied = true;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Pay for order and close it
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<decimal> PayOrder(Guid orderId)
        {
            var resultOrder = Orders.SingleOrDefault(x => x.Id == orderId);
            if (resultOrder == null)
            {
                throw new NullReferenceException($"reference to order is null");
            }

            decimal resultSum = (resultOrder.DepartureDate - resultOrder.ArrivalDate).Days * (resultOrder.Room.RoomType.Price + (resultOrder.PeopleCount - 1) * resultOrder.Room.RoomType.PricePerMen);
            Orders.SingleOrDefault(x => x.Id == orderId).Bill = resultSum;
            Orders.SingleOrDefault(x => x.Id == orderId).IsClosed = true;

            var user = await _context.Users.SingleAsync(u => u.Id == resultOrder.UserId);
            user.IsRenter = false;

            var roomToChange = await _context.Rooms.FirstAsync(r => r.Id == resultOrder.RoomId);
            roomToChange.IsOccupied = false;

            await _context.SaveChangesAsync();
            return resultSum;
        }

        /// <summary>
        /// Edit order's departure day
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newDepDate"></param>
        /// <returns></returns>
        public async Task EditDepartureDay(Guid orderId, DateTime newDepDate)
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

        /// <summary>
        /// Finds order by Id
        /// </summary>
        /// <param name="id">Order's id</param>
        /// <returns></returns>
        public async Task<Order> FindByIdAsync(Guid id)
        {
            var resultOrder = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            if (resultOrder == null)
            {
                throw new NullReferenceException($"There is no order with such id: {id} !");
            }
            return resultOrder;
        }
    }
}
