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
using ASPCoreNetProject.Data;
using ASPCoreNetProject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ASPCoreNetProject.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace ASPCoreNetProject
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ASPCore")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "1079137313732-aj867h7r23qqeo98eme36go6gfqnlvc8.apps.googleusercontent.com";
                options.ClientSecret = "iAKkFf_HkclC4M8W-qn0FSr6";
            }).AddFacebook(options =>
            {
                options.ClientId = "259560725166772";
                options.AppSecret = "f553efb0190da41fb9e9df8e74ba0236";
            });
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient<IArtBase<Art>, ArtService>();
            services.AddTransient<IArtPhotoBase<ArtPhoto>, ArtPhotoService>();
            services.AddTransient<IRateBase<Rate>, RateService>();
            services.AddTransient<IFavouriteBase<Favourite>, FavouriteService>();
            services.AddTransient<IUserBase<ApplicationUser>, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> manager, RoleManager<IdentityRole> roleManager)
        {
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
            MyIdentityDataInitializer.SeedData(manager, roleManager);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
