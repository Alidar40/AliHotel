using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Models;
using AliHotel.Domain.Services;
using FluentScheduler;
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
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly AliIAuthorizationService _authorizatioService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly EmailService _emailService;

        /// <summary>
        /// AccountController constructor
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userService"></param>
        /// <param name="authorization"></param>
        /// <param name="userManager"></param>
        public AccountController(SignInManager<User> signInManager, IUserService userService, AliIAuthorizationService authorization, UserManager<User> userManager)
        {
            _userService = userService;
            _authorizatioService = authorization;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = new EmailService();
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<object> RegisterUser([FromBody] UserRegisterModel model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            var user = await _userService.AddAsync(model);
            var token = await _authorizatioService.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { actionGuid = token.actionGuid, code = token.code, userId = user.Id},
                protocol: HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(model.Email, "Confirm your account",
                $"Confirm registration by following this link: <a href='{callbackUrl}'>link</a>");
      
            return "Check your email for confirmation";
        }

        /// <summary>
        /// Confirms email
        /// </summary>
        /// <param name="actionGuid"></param>
        /// <param name="code"></param>
        /// /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("ConfirmEmail")]
        public async Task<object> ConfirmEmail(Guid actionGuid, string code, Guid userId)
        {
            if (actionGuid == null || code == null)
            {
                return BadRequest();//RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var result = await _authorizatioService.ConfirmEmailAsync(actionGuid, code);

            await _userService.ConfirmEmail(userId);
            
            return "Confirmed"; //Add redirection later
        }

        /// <summary>
        /// Login to system
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginModel model)
        {
            var resultUser = await _authorizatioService.AuthorizationAsync(model);
            if (resultUser != null)
            {
                await _signInManager.SignInAsync(resultUser, false);
                return Ok(resultUser.Name);
            }
            return BadRequest("Incorrect email or password");
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

        /// <summary>
        /// Sends email with link to password reset page
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("SendEmailToResetPassword")]
        public async Task<object> SendEmailToResetPassword([FromBody] string email)
        {
            if (email == null || email == String.Empty)// check that email is actually an email - ???
            {
                return BadRequest();
            }
            
            var token = await _authorizatioService.GenerateResetPasswordToken(email);
            var callbackUrl = Url.Action(
                "ConfirmPasswordResetCode",
                "Account",
                new { actionGuid = token.actionGuid, code = token.code },
                protocol: HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(email, "Reset password",
                $"To reset password follow this link: <a href='{callbackUrl}'>link</a>");

            return "Check your email for confirmation";
        }

        /// <summary>
        /// Returns Guid to password reset action
        /// </summary>
        /// <param name="actionGuid"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("ConfirmResetPasswordCode")]
        public async Task<object> ConfirmPasswordResetCode(Guid actionGuid, string code)
        {
            var newActionGuid = await _authorizatioService.ConfirmPasswordResetCode(actionGuid, code);
            //return RedirectToAction("ResetPassword", newActionGuid);
            return newActionGuid.ToString();
        }

        /// <summary>
        /// Resets password
        /// </summary>
        /// <param name="actionGuid"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPost("ResetPassword/{actionGuid}")]
        public async Task<object> ResetPassword(Guid actionGuid, [FromBody]string newPassword)
        {
            if (actionGuid == null)
            {
                return BadRequest();//RedirectToAction(nameof(HomeController.Index), "Home");
            }

            await _authorizatioService.ResetPassword(actionGuid, newPassword);

            return Ok("Password was reset successfully");
        }
    }
}