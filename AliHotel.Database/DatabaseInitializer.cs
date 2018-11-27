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
                    Email = "admin@alihotel.com",
                    BirthDate = DateTime.Parse("01/01/1980"),
                    PhoneNumber = "adminNumber",
                    PasswordSalt = passwordSalt,
                    PasswordHash = resultHash,
                    RoleId = RolesOptions.Admin,
                };
                await context.Users.AddAsync(admin);
            }

            {
                var standartRoom = await context.RoomTypes.SingleOrDefaultAsync(x => x.Name == "Standart");
                var semiSuiteRoom = await context.RoomTypes.SingleOrDefaultAsync(x => x.Name == "Semi-suite");
                var suiteRoom = await context.RoomTypes.SingleOrDefaultAsync(x => x.Name == "Suite");

                if (standartRoom == null)
                {
                    standartRoom = new RoomType("Standart", 1000, 200);
                    await context.RoomTypes.AddAsync(standartRoom);
                }

                if (semiSuiteRoom == null)
                {
                    semiSuiteRoom = new RoomType("Semi-suite", 2000, 500);
                    await context.RoomTypes.AddAsync(semiSuiteRoom);
                }

                if (suiteRoom == null)
                {
                    suiteRoom = new RoomType("Suite", 3000, 1000);
                    await context.RoomTypes.AddAsync(suiteRoom);
                }

                var roomCount = await context.Rooms.CountAsync();

                if(roomCount == 0)
                {
                    for (int i = 1; i <= 35; i++)
                    {
                        Room room;
                        if (i < 20)
                        {
                            room = new Room(i, 4, standartRoom);
                        }
                        else if (i < 30)
                        {
                            room = new Room(i, 3, semiSuiteRoom);
                        }
                        else
                        {
                            room = new Room(i, 2, suiteRoom);
                        }

                        await context.Rooms.AddAsync(room);
                    }
                }
                
            }
            await context.SaveChangesAsync();
        }
    }
}
