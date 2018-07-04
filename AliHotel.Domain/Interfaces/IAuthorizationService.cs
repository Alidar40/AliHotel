using AliHotel.Domain.Entities;
using AliHotel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AliHotel.Domain.Interfaces
{
    /// <summary>
    /// Interface for authorization
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Returns user if he authorized, else returns null
        /// </summary>
        /// <param name="loginModel">User's login model</param>
        /// <returns></returns>
        Task<User> AuthorizationAsync(LoginModel loginModel);

        /// <summary>
        /// Generates token for email confirmation
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<(Guid actionGuid, string code)> GenerateEmailConfirmationTokenAsync(User user);

        /// <summary>
        /// Confirms email via link sent to user
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<Guid> ConfirmEmailAsync(Guid actionId, string code);
    }
}
