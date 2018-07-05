using AliHotel.Database;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Models;
using AliHotel.Domain.Services;
using AliHotel.Tests.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliHotel.Tests.Tests
{
    /// <summary>
    /// Class for testing UserService class
    /// </summary>
    [TestFixture]
    public class UserServiceTests
    {
        private List<User> _existingUsers;

        private IUserService _userService;

        private DatabaseContext _context;

        private IPasswordHasher<User> _passwordHasher;

        private IHttpContextAccessor _httpContextAccessor;

        [SetUp]
        public async Task Initialize()
        {
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            _existingUsers = await _context.Users.ToListAsync();

            _passwordHasher = TestInitializer.Provider.GetService<IPasswordHasher<User>>();
            _httpContextAccessor = TestInitializer.Provider.GetService<IHttpContextAccessor>();

            _userService = new UserService(_context, _passwordHasher, _httpContextAccessor);
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
        }
        
        /// <summary>
        /// Creates fake user
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task CreteUser_Success()
        {
            var user = new UserRegisterModel();
            {
                Name = "TestUser",
                Email = "Email",
                Password = "qwertqwert",
                PhoneNumber = "89208888888",
                BirthDate = DateTime.Parse("01.01.1970"),
                CreditCard = "1111 2222 3333 4444"
            };
            
            var resultUser = await _userService.AddAsync(user);
            var userToCompare = await _context.Users.SingleOrDefaultAsync(x => x.Email == user.Email);
            
            Assert.AreEqual(user.Email, userToCompare.Email);
            Assert.AreEqual(user.Name, userToCompare.Name);
            Assert.AreEqual(user.BirthDate, userToCompare.BirthDate);
            Assert.AreEqual(user.CreditCard, userToCompare.CreditCard);
            Assert.AreEqual(user.PhoneNumber, userToCompare.PhoneNumber);
        }
    }
}
