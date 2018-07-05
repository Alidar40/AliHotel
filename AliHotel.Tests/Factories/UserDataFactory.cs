using AliHotel.Database;
using AliHotel.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliHotel.Tests.Factories
{
    /// <summary>
    /// Fulls database with fake users
    /// </summary>
    public class UserDataFactory
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserDataFactory()
        {
            _context = TestInitializer.Provider.GetService<DatabaseContext>();
            _passwordHasher = TestInitializer.Provider.GetService<IPasswordHasher<User>>();
        }

        /// <summary>
        /// Adds fake user in database
        /// </summary>
        /// <returns></returns>
        public async Task CreateUsers()
        {
            //При создание пароля, сначала пароль, потом соль
            var userList = new List<User>
            {
                new User("admin", "admin", DateTime.Parse("01.01.1970"), "1111 2222 3333 4444","88005553555", "qwert", "admin", RolesOptions.Admin),
                new User("testUser2", "testEmail2", DateTime.Parse("01.01.1970"), "1111 2222 3333 4444","88005553555", "asd","user" , RolesOptions.User),
            };
            await _context.Users.AddRangeAsync(userList);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes fake users
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task Dispose()
        {
            var users = await _context.Users.Where(u => u.EmailConfirmed == false).ToListAsync();
            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync();
        }
    }
}
