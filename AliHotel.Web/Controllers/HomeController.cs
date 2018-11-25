using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AliIAuthorizationService = AliHotel.Domain.Interfaces.IAuthorizationService;

namespace AliHotel.Web.Controllers
{
    [Produces("application/json")]
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        public HomeController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            
            return RedirectToAction("Login");
        }

        [Route("/Login")]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}