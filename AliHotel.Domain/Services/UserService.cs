using AliHotel.Database;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Models;
using AliHotel.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliHotel.Domain.Services
{
    /// <summary>
    /// Implementation of IUserService interface
    /// </summary>
    public class UserService: IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<User> Users => _context.Users
            .Include(x => x.Role)
            .Include(x => x.Orders)
            .ToList();

        public UserService(DatabaseContext context, IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<User> AddAsync(UserRegisterModel userModel)
        {
            if (userModel == null)
            {
                throw new NullReferenceException("userModel == null");
            }

            var checkOnEmail = await _context.Users.AnyAsync(x => x.Email == userModel.Email);
            if (checkOnEmail)
            {
                throw new ArgumentException($"User with such an email: {userModel.Email} already exists.");
            }

            var passwordSalt = Randomizer.GetRandString(10);

            var passwordHash = _passwordHasher.HashPassword(null, userModel.Password + passwordSalt);

            var resultUser = new User(userModel.Name, userModel.Email, userModel.BirthDate, userModel.PhoneNumber, passwordSalt, passwordHash, RolesOptions.User);

            await _context.Users.AddAsync(resultUser);
            await _context.SaveChangesAsync();

            return resultUser;
        }

        /// <summary>
        /// Confirms user's email
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task ConfirmEmail(Guid userId)
        {
            if(userId == null)
            {
                throw new ArgumentException($"User's id is incorrect");
            }

            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (resultUser == null)
            {
                throw new NullReferenceException($"There is no user with such id: {userId} !");
            }

            resultUser.EmailConfirmed = true;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit user by id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task EditAsync(Guid id, UserEditModel userModel)
        {
            if (userModel == null)
            {
                throw new NullReferenceException("userModel == null");
            }

            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (resultUser == null)
            {
                throw new NullReferenceException($"There is no user with such id: {id} !");
            }
            var checkOnEmail = await _context.Users.AnyAsync(x => x.Email == userModel.Email);
            if (checkOnEmail)
            {
                throw new ArgumentException($"User with such an email: {userModel.Email} already exists.");
            }

            resultUser.Email = userModel.Email;
            resultUser.Name = userModel.Name;
            resultUser.PhoneNumber = userModel.PhoneNumber;
            resultUser.CreditCard = userModel.CreditCard;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">Id of the user being deleted</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (resultUser == null)
            {
                throw new NullReferenceException($"There is no user with such id: {id} !");
            }
            _context.Users.Remove(resultUser);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAsync()
        {
            return await _context.Users
                .Include(x => x.Role)
                .Include(x => x.Orders)
                .ToListAsync();
        }

        /// <summary>
        /// Finds user by Id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns></returns>
        public async Task<User> FindByIdAsync(Guid id)
        {
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (resultUser == null)
            {
                throw new NullReferenceException($"There is no user with such id: {id} !");
            }
            return resultUser;
        }

        /// <summary>
        /// Returns whether user is logged
        /// </summary>
        /// <returns></returns>
        public bool IsUserLoggedIn()
        {
            var context = _httpContextAccessor.HttpContext;
            return context.User.Identities.Any(x => x.IsAuthenticated);
        }
    }
}
