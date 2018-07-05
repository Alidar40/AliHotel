using AliHotel.Database;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Models;
using AliHotel.Domain.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
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
    public class AuthorizationService : IAuthorizationService
    {
        private readonly DatabaseContext _context;
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// AuthorizationService constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        /// <param name="passwordHasher"></param>
        /// <param name="memoryCache"></param>
        public AuthorizationService(DatabaseContext context, IUserService userService, IPasswordHasher<User> passwordHasher, IMemoryCache memoryCache)
        {
            _context = context;
            _userService = userService;
            _passwordHasher = passwordHasher;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Authorizes user
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
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

            if (resultHash != resultUser.PasswordHash)
            {
                throw new ArgumentException("Incorrect password or e-mail");
            }
            return resultUser;
        }

        /// <summary>
        /// Generates token for email confirmation
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<(Guid actionGuid, string code)> GenerateEmailConfirmationTokenAsync(User user)
        {
            var codeToSend = Randomizer.GetRandNumbers(5);
            var resultModel = new ConfirmEmailModel { UserId = user.Id, Code = codeToSend };
            var actionGuid = Guid.NewGuid();
            _memoryCache.Set(actionGuid, resultModel, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(3)));

            return (actionGuid, codeToSend);
        }

        /// <summary>
        /// Confirms email
        /// </summary>
        /// <param name="actionGuid"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<Guid> ConfirmEmailAsync(Guid actionGuid, string code)
        {
            var resultModel = _memoryCache.Get<ConfirmEmailModel>(actionGuid);
            if (resultModel == null)
            {
                throw new InvalidOperationException($"There is no operations with such an Id:{actionGuid}");
            }
            if (String.Compare(resultModel.Code, code, StringComparison.Ordinal) != 0)
            {
                throw new InvalidOperationException($"Confirmation code is incorrect.");
            }

            var resultActionGuid = Guid.NewGuid();

            _memoryCache.Set(resultActionGuid, resultModel.UserId,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            return await Task.FromResult(resultActionGuid);
        }

        /// <summary>
        /// Generates token for password reseting
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<(Guid actionGuid, string code)> GenerateResetPasswordToken(string email)
        {
            var codeToSend = Randomizer.GetRandNumbers(5);
            var resultModel = new ForgotPasswordModel { Email = email, Code = codeToSend };
            var actionGuid = Guid.NewGuid();
            _memoryCache.Set(actionGuid, resultModel, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(3)));

            return (actionGuid, codeToSend);
        }

        /// <summary>
        /// Confirms password reset link set to email and returns guid to password reset action
        /// </summary>
        /// <param name="actionGuid"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<Guid> ConfirmPasswordResetCode(Guid actionGuid, string code)
        {
            var resultModel = _memoryCache.Get<ForgotPasswordModel>(actionGuid);
            if (resultModel == null)
            {
                throw new InvalidOperationException($"There is no operations with such an Id:{actionGuid}");
            }
            if (String.Compare(resultModel.Code, code, StringComparison.Ordinal) != 0)
            {
                throw new InvalidOperationException($"Confirmation code is incorrect.");
            }

            var resultActionGuid = Guid.NewGuid();

            _memoryCache.Set(resultActionGuid, resultModel.Email,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            return await Task.FromResult(resultActionGuid);
        }

        /// <summary>
        /// Resets password
        /// </summary>
        /// <param name="actionGuid"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task ResetPassword(Guid actionGuid, string newPassword)
        {
            var userEmail = _memoryCache.Get<string>(actionGuid);
            if (userEmail == String.Empty)
            {
                throw new NullReferenceException($"There is no operations with such an Id:{actionGuid}");
            }
            await _userService.ResetPassword(userEmail, newPassword);
        }

        /// <summary>
        /// Deletes all users, who has unconfirmed emails
        /// </summary>
        /// <returns></returns>
        public async Task DeleteUsersWithUncofirmedEmails()
        {
            var usersToDelete = await _context.Users.Where(u => u.EmailConfirmed == false).ToListAsync();
            _context.Users.RemoveRange(usersToDelete);

            foreach (var user in usersToDelete)
            {
                if((user.RegistrationTime - DateTime.UtcNow).Seconds > 30)
                {
                    _context.Remove(user);
                }
            }

            _context.SaveChanges();
        }
    }
}
