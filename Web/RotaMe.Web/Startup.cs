using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RotaMe.Data;
using RotaMe.Data.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using RotaMe.Services.EmailSender;
using RotaMe.Web.Common;
using RotaMe.Services.Mapping;
using RotaMe.Services;
using RotaMe.Services.Contracts;
using RotaMe.Sevices.Models;
using System.Reflection;
using RotaMe.Web.ViewModels.Administration.Users;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RotaMe.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RotaMeDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //options => options.SignIn.RequireConfirmedAccount = true in constructor
            services.AddIdentity<RotaMeUser, IdentityRole>()
                .AddEntityFrameworkStores<RotaMeDbContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();


            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            Account cloudinaryCredentials = new Account(
                this.Configuration["Cloudinary:CloudName"],
                this.Configuration["Cloudinary:ApiKey"],
                this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);

            //Password settings for easy work
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options => options.LoginPath = "/identity/account/login");

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IProjectsService, ProjectsService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();

            services.AddSingleton<IEmailSender, SendGridEmailSender>();
            services.Configure<SendGridOptions>(Configuration.GetSection("EmailSettings"));
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ListUserServiceModel).GetTypeInfo().Assembly,
                typeof(UsersListViewModel).GetTypeInfo().Assembly);
            

            app.SeedDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "areas",
                     template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    //endpoints.MapControllerRoute(
            //    //    name: "default",
            //    //    pattern: "{controller=Home}/{action=Index}/{id?}");
            //    //endpoints.MapRazorPages();

            //    endpoints.MapControllers();
            //    endpoints.MapAreaControllerRoute(
            //        "admin",
            //        "admin",
            //        "Admin/{controller=Home}/{action=Index}/{id?}");
            //    endpoints.MapControllerRoute(
            //        "default", "{controller=Home}/{action=Index}/{id?}");
            //    endpoints.MapRazorPages();
            //});
        }
    }
}
