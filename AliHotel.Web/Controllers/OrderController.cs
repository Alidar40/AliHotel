using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AliHotel.Domain.Services;
using AliHotel.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AliHotel.Web.Controllers
{
    [Route("orders")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}