using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using AliIAuthorizationService = AliHotel.Domain.Interfaces.IAuthorizationService;
namespace AliHotel.Web.Controllers
{
    /// <summary>
    /// Controller for registration, logging in/out
    /// </summary>
    [Produces("application/json")]
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly AliIAuthorizationService _authorization;
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager, IUserService userService, AliIAuthorizationService authorization)
        {
            _userService = userService;
            _authorization = authorization;
            _signInManager = signInManager;
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<object> RegisterUser([FromBody] UserRegisterModel model)
        {
            return await _userService.AddAsync(model);
        }

        /// <summary>
        /// Login to system
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginModel model)
        {
            var resultUser = await _authorization.AuthorizationAsync(model);
            if (resultUser != null)
            {
                await _signInManager.SignInAsync(resultUser, false);
                return "Authorized successfully";
            }
            return "Authorization failed!";
        }

        /// <summary>
        /// Logoff from system
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async Task<object> LogOff()
        {
            await _signInManager.SignOutAsync();
            return "Logged off successfully";
        }
    }
}