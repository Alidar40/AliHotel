using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Models;
using AliHotel.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AliHotel.Web.Controllers.Admin
{
    /// <summary>
    /// Admin's tools for working with orders
    /// </summary>
    [Produces("application/json")]
    [Route("api/Admin/Orders")]
    [Authorize(Roles = nameof(RolesOptions.Admin))]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for OrderController
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="userManager"></param>
        /// <param name="userService"></param>
        public OrdersController(IOrderService orderService, UserManager<User> userManager, IUserService userService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _userService = userService;
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
            return Ok(result.Select(x => x?.OrderView()));
        }

        /// <summary>
        /// Pays off the order
        /// </summary>
        /// <returns></returns>
        [HttpGet("CloseOrder")]
        public async Task<object> CloseOrder(Guid orderId)
        {
            Order order = _orderService.Orders.FirstOrDefault(o => o.Id == orderId);
            if(order == null)
            {
                return NotFound("This order does not exist");
            }

            var bill = await _orderService.PayOrder(order.Id);

            return Ok(bill.ToString());
        }

        /// <summary>
        /// Fetches all data needed for admin
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCurrentData")]
        public async Task<object> GetCurrentData()
        {
            var _activeOrders = await _orderService.GetAsync();
            var activeOrders = _activeOrders.Where(x => x.IsClosed == false).Select(x => x?.OrderView());
            if (!activeOrders.Any())
            {
                return NotFound("There are no active orders");
            }

            var _renters = await _userService.GetAsync();
            var renters = _renters.Where(u => u.IsRenter == true).Select(x => x.UserView());
            if (!renters.Any())
            {
                return NotFound("There is no renters");
            }

            return Ok(new { name="admin", activeOrders, renters });
        }
    }
}