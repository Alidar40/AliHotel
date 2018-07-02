using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AliHotel.Domain.Services;
using AliHotel.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AliHotel.Web.Extensions;
using AliHotel.Domain.Models;
using AliHotel.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AliHotel.Web.Controllers
{
    /// <summary>
    /// Controller for orders
    /// </summary>
    [Produces("application/json")]
    [Route("Orders")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        public OrderController(IOrderService orderService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<object> GetAllOrders()
        {
            var result = await _orderService.GetAsync();
            return result.Select(x => x?.OrderView());
        }

        /// <summary>
        /// Returns all users orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("MyOrders")]
        public async Task<object> GetAllUsersOrders()
        {
            var result = await _orderService.GetAsync();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return result.Where(x => x.User == user).Select(x => x?.OrderView());
        }

        /// <summary>
        /// Returns users current order
        /// </summary>
        /// <returns></returns>
        [HttpGet("CurrentOrder")]
        public async Task<object> GetCurrentOrder()
        {
            var result = await _orderService.GetAsync();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (!result.Any(x => x.User == user && x.IsClosed == false))
            {
                return "You have not active order.";
            }
            return result.Where(x => x.User == user).Select(x => x?.OrderView());
        }

        /// <summary>
        /// Add order to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> AddOrder([FromBody]OrderModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user.IsRenter)
            {
                return "You can't have two orders simultaneously";
            }

            model.UserId = user.Id;
            bool result = await _orderService.AddAsync(model);
            if (result == true)
            {
                return "Your order have been added";
            }
            else
            {
                return "We have not found suitable room";
            }
        }

        /// <summary>
        /// Changes departure day
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newDepDate"></param>
        /// <returns></returns>
        [HttpPut("EditDepartureDay")]
        public async Task<object> EditDepartureDay([FromQuery]Guid orderId, [FromBody]DateTime newDepDate)
        {
            var r = Request;
            var order = _orderService.FindByIdAsync(orderId);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user.Id != order.Result.UserId)
            {
                throw new UnauthorizedAccessException("You have not permissions to edit this order");
            }

            if(order.Result.IsClosed)
            {
                throw new Exception("It is impossible to edit this order");
            }
            await _orderService.EditDepartureDay(orderId, newDepDate);
            return newDepDate;
        }

        /// <summary>
        /// Pays off the order
        /// </summary>
        /// <returns></returns>
        [HttpGet("PayOrder")]
        public async Task<string> PayOrder()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if(!user.IsRenter)
            {
                return "You have not active orders to close";
            }

            Order order = _orderService.Orders.First(o => o.IsClosed == false && o.UserId == user.Id);

            var bill = await _orderService.PayOrder(order.Id);

            return "Your bill: " + bill.ToString();
        }
    }
}