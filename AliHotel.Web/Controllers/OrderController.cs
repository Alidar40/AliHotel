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

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Returns all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetOrders()
        {
            var result = await _orderService.GetAsync();
            return result.Select(x => x?.OrderView());
        }

        /// <summary>
        /// Add order to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> AddOrder([FromBody]OrderModel model)
        {
            return await _orderService.AddAsync(model);
        }

        /// <summary>
        /// Changes departure day
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderId"></param>
        /// <param name="newDepDate"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task EditDepartureDay([FromQuery] Guid userId, [FromQuery]Guid orderId, [FromBody]DateTime newDepDate)
        {
            var order = _orderService.FindByIdAsync(orderId);
            if(order.Result.UserId != userId)
            {
                throw new UnauthorizedAccessException("You have not permissions to edit this order");
            }
            await _orderService.EditDepartureDay(orderId, newDepDate);
        }
    }
}