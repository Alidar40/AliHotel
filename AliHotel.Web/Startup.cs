using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AliHotel.Database;
using AliHotel.Domain;
using AliHotel.Domain.Entities;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AliHotel.Identity;
using React.AspNet;

namespace AliHotel.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IServiceProvider ServiceProvider;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();

            //Add database context
            services.AddDbContext<DatabaseContext>(x =>
                x.UseNpgsql(Configuration["ConnectionStrings:ConnectionToDb"],
                assemb => assemb.MigrationsAssembly("AliHotel.Web")));

            //Services for user authentication and password validation
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();


            //Add authentication
            services.AddIdentity<User, Role>(options =>
                        {
                            options.User.RequireUniqueEmail = true;
                        })
                .AddRoleStore<RoleStore>()
                .AddUserStore<IdentityStore>()
                .AddPasswordValidator<Md5PasswordValidator>()
                //.AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            //Configure authentication
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;
                
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            //Add swagger for documenting API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AliHotel API", Version = "v1" });

                c.IncludeXmlComments(System.AppDomain.CurrentDomain.BaseDirectory + @"AliHotel.Web.xml");
            });

            services.AddDomainServices();

            ServiceProvider = services.BuildServiceProvider();
            return ServiceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AliHotel API V1");
            });

            app.UseStaticFiles();
            app.UseReact(config => { });
            app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
            ServiceProvider.GetService<DatabaseContext>().Initialize(ServiceProvider).Wait();

            /*app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });*/
        }
    }
}
