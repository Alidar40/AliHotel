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

        /// <summary>
        /// Generates token for password reseting
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<(Guid actionGuid, string code)> GenerateResetPasswordToken(string email);

        /// <summary>
        /// Confirms password reset link set to email and returns guid to password reset action
        /// </summary>
        /// <param name="actionGuid"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<Guid> ConfirmPasswordResetCode(Guid actionGuid, string code);

        /// <summary>
        /// Resets password
        /// </summary>
        /// <param name="actionGuid"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task ResetPassword(Guid actionGuid, string newPassword);

        /// <summary>
        /// Deletes all users, who has unconfirmed emails
        /// </summary>
        /// <returns></returns>
        Task DeleteUsersWithUncofirmedEmails();
    }
}
