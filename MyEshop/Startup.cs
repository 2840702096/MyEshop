using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyEshop.Core.Services.Interfaces;
using MyEshop.Core.Services;
using MyEShop.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop
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

            #region Context

            services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ShopConnectionString"));
            });

            #endregion

            #region IOC

            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<ISliderServices, SliderServices>();
            services.AddTransient<IBannerervices, BannerServices>();
            services.AddTransient<IWeblogServices, WeblogServices>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IDiscountServices, DiscountServices>();
            services.AddTransient<IAnswerOfRepetitiveQuestions, AnswerOfRepetitiveQuestions>();
            services.AddTransient<IRulesAndOthersServices, RulesAndOthersServices>();
            services.AddTransient<IOrderServices, OrderServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IClientServices, ClientServices>();
            services.AddTransient<ISingleProduct, SingleProduct>();

            #endregion

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Areas",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
