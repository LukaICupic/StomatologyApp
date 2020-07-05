using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StomatologyApp.Interfaces;
using StomatologyApp.Models;

namespace StomatologyApp
{
    public class Startup
    {
        private IConfiguration _config;


        public Startup (IConfiguration config)
        {
            _config = config;

        }

        async Task CreateRole(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string role = "Admin";

            bool existingRole = await RoleManager.RoleExistsAsync(role);

            if (!existingRole)
            {
                await RoleManager.CreateAsync(new IdentityRole(role));
            }

            var adminUser = await UserManager.FindByEmailAsync("admin@gmail.com");

            if (adminUser == null)
            {
                var superAdmin = new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };

                string adminPass = "123456A";

                var createsuperAdmin = await UserManager.CreateAsync(superAdmin, adminPass);

                if (createsuperAdmin.Succeeded)
                {
                    await UserManager.AddToRoleAsync(superAdmin, "Admin");
                }
            }
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("SSMSConnectionString")));


            services.AddIdentity<IdentityUser, IdentityRole>(o => {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDentalProcedureRepository, DentalProcedureRepo>();
            services.AddScoped<IWorkDayRepository, WorkDaysRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }

        

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default","/{controller=Home}/{action=Index}/{Id?}");
            });

            CreateRole(serviceProvider).Wait();
        }


    }
}
