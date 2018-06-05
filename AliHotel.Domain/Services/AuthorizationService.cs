using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliHotel.Domain.Services
{
    /// <summary>
    /// Class for user authorization
    /// </summary>
    public class AuthorizationService: IAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthorizationService(IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> AuthorizationAsync(LoginModel loginModel)
        {
            if (loginModel.Email == null)
            {
                return null;
            }

            var users = await _userService.GetAsync();
            var resultUser = users.SingleOrDefault(x => x.Email == loginModel.Email);
            if (resultUser == null)
            {
                throw new ArgumentException("Incorrect password or e-mail");
            }
            var resultHash = _passwordHasher.HashPassword(resultUser, loginModel.Password);

            if(resultHash != resultUser.PasswordHash)
            {
                throw new ArgumentException("Incorrect password or e-mail");
            }
            return resultUser;

        }
    }
}
