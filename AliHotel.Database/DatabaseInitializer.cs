using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using AliHotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AliHotel.Database
{
    /// <summary>
    /// Static extension class
    /// </summary>
    public static class DatabaseInitializer
    {
        public static async Task Initialize(this DatabaseContext context, IServiceProvider services)
        {
            var userManager = services.GetService<UserManager<User>>();
            var roleManager = services.GetService<RoleManager<IdentityRole>>();

            //Initializing roles
            foreach (RolesOptions role in Enum.GetValues(typeof(RolesOptions)))
            {
                var roleForAdd = await context.Roles.SingleOrDefaultAsync(x => x.Id == role);
                if (roleForAdd == null)
                {
                    roleForAdd = new Role(role, Enum.GetName(typeof(RolesOptions), role));
                    await context.Roles.AddAsync(roleForAdd);
                }
            }

            //Initializing administrator
            var admin = await context.Users.SingleOrDefaultAsync(x =>
                    x.Name == "admin" && x.RoleId == RolesOptions.Admin);

            if (admin == null)
            {
                var hashProvider = services.GetService<IPasswordHasher<User>>();
                var passwordSalt = "adminsalt";
                var password = "admin";
                var resultHash = hashProvider.HashPassword(null, password + passwordSalt);
                admin = new User
                {
                    Name = "admin",
                    Email = "admin",
                    BirthDate = DateTime.Parse("01/01/1980"),
                    PhoneNumber = "adminNumber",
                    PasswordSalt = passwordSalt,
                    PasswordHash = resultHash,
                    RoleId = RolesOptions.Admin,
                };
                await context.Users.AddAsync(admin);
            }
            await context.SaveChangesAsync();
        }
    }
}
