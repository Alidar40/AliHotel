using AliHotel.Domain.Entities;
using AliHotel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AliHotel.Domain.Interfaces
{
    /// <summary>
    /// Interface for working with users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// List of users
        /// </summary>
        List<User> Users { get;  }

        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="userModel">User's model for registering</param>
        /// <returns></returns>
        Task<User> AddAsync(UserRegisterModel userModel);

        /// <summary>
        /// Confirms user's email
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ConfirmEmail(Guid userId);

        /// <summary>
        /// Change user by id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task EditAsync(Guid id, UserEditModel userModel);

        /// <summary>
        /// Deletes user by id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAsync();

        /// <summary>
        /// Returns user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> FindByIdAsync(Guid id);

        /// <summary>
        /// Changes user's password (in case he forgot it)
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        Task ResetPassword(string email, string newPassword);
    }
}
