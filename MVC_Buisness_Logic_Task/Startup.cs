using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_Buisness_Logic_Task.Abstarctions.Repositories;
using MVC_Buisness_Logic_Task.DAL;
using MVC_Buisness_Logic_Task.Models;
using MVC_Buisness_Logic_Task.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Buisness_Logic_Task
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
            services.AddControllersWithViews();
            services.AddIdentity<AppUser, IdentityRole>(
                IdentityOptions =>
                {
                    IdentityOptions.Password.RequiredLength = 8;
                    IdentityOptions.Password.RequireNonAlphanumeric = true;
                    IdentityOptions.Password.RequireUppercase = true;
                    IdentityOptions.Lockout.MaxFailedAccessAttempts = 5;
                    IdentityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                    IdentityOptions.Lockout.AllowedForNewUsers = true;
                    IdentityOptions.User.RequireUniqueEmail = true;
                    IdentityOptions.SignIn.RequireConfirmedEmail = false;
                    IdentityOptions.SignIn.RequireConfirmedPhoneNumber = false;
                }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("Default"));
            });
            services.AddScoped<IBaseRepository<Vehicle>, BaseRepository<Vehicle>>();
            services.AddScoped<IBaseRepository<AppUser>, BaseRepository<AppUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
