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
    /// <summary>
    /// Controller for issuing views
    /// </summary>
    [Produces("application/json")]
    public class HomeController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        /// <summary>
        /// HomeController constructor
        /// </summary>
        /// <param name="signInManager"></param>
        public HomeController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Returns main view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                if(User.IsInRole(nameof(RolesOptions.Admin)))
                {
                    return RedirectToAction("Admin");
                }

                return View();
            }
            
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Returns view for unathorized users
        /// </summary>
        /// <returns></returns>
        [Route("/Login")]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        [Route("/Admin/{*catchall}")]
        public IActionResult Admin()
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole(nameof(RolesOptions.Admin)))
                {
                    return View("Index");
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }
    }
}