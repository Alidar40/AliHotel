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
    [Route("Admin/Orders")]
    [Authorize(Roles = nameof(RolesOptions.Admin))]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        public OrdersController(IOrderService orderService, UserManager<User> userManager)
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
    }
}