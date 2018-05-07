using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using AliHotel.Domain.Entities;

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
            foreach (var roleOpt in Enum.GetValues(typeof(RolesOptions)))
            {
                var roleName = Enum.GetName(typeof(RolesOptions), roleOpt);
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }
            
            //Initializing administrator
            var admin = new User("admin", "admin@alihotel.com", DateTime.Parse("01/01/1980"));
            var adminPassword = "admin";
            var result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, nameof(RolesOptions.Admin));
            }
        }
    }
}
