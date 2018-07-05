using AliHotel.Database;
using AliHotel.Domain.Entities;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Services;
using AliHotel.Identity;
using AliHotel.Tests.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace AliHotel.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider Provider { get; private set; }

        [OneTimeSetUp]
        public void SetUpConfig()
        {
            var services = new ServiceCollection();

            //Database 
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("AliHotelDb"));

            services.AddMemoryCache();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();

            //Factories
            services.AddScoped<UserDataFactory>();

            Provider = services.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public void DownUpConfig()
        {
            Provider.GetService<DatabaseContext>().Database.EnsureDeleted();
        }
    }
}
