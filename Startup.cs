using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using RentACarWEBIzptina.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarWEBIzptina
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RentACarDBContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //  .AddEntityFrameworkStores<RentACarDBContext>();
            // services.AddControllersWithViews();
            services.AddIdentity<User, IdentityRole>(options =>
             {
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 3;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequiredUniqueChars = 0;

             }
            )
                 .AddEntityFrameworkStores<RentACarDBContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            CreateUserRoles(serviceProvider).Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        private async System.Threading.Tasks.Task CreateUserRoles(IServiceProvider serviceProvider)
        {

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "Admin", "User", "Employee" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleCheck = await RoleManager.RoleExistsAsync(roleName);
                if (!roleCheck)
                {
                    //create the roles and seed them to the database 
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //Admin user check and creation 
            var user = new User();
            user.UserName = "admin";
            user.Id = Guid.NewGuid().ToString();
            user.FirstName = "Admin";
            user.LastName = "Admin";
            user.EGN = "1234567890";
            user.PhoneNumber = "0888888888";
            user.Email = "admin@admin.admin";
            user.IsAdmin = true;
            string userPWD = "password";
            var _user = await UserManager.FindByNameAsync(user.UserName);
            if (_user == null)
            {
                IdentityResult chkUser = await UserManager.CreateAsync(user, userPWD);
                if (chkUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
