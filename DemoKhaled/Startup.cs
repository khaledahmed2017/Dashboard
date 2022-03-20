using DemoKhaled.BL.interfaces;
using DemoKhaled.BL.Mapper;
using DemoKhaled.BL.Repository;
using DemoKhaled.DAL.Context;
using DemoKhaled.DAL.Entities;
using DemoKhaled.DAL.Extensions;
using DemoKhaled.Language;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DemoKhaled
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
            //this line must be exist automatically for mvc
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)//indecate that i will work with text to translate it
                .AddDataAnnotationsLocalization(options =>//as well as dataAnnotation
                    {
                        options.DataAnnotationLocalizerProvider = (type, factory) =>
                            factory.Create(typeof(SharedResource));
                    }).AddNewtonsoftJson(opt => {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });


            //conection String
            services.AddDbContextPool<Democontext>(opts =>
            opts.UseSqlServer(Configuration.GetConnectionString("DemoConnection")));//Configuration has a method for connection
            // DemoConnection is the conection string in appSettings
            //auto mapper
            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
               options =>
               {
                   options.LoginPath = new PathString("/Account/Login");
                   options.AccessDeniedPath = new PathString("/Account/Login");
               });
            services.AddIdentity<UserApplication, IdentityRole>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<Democontext>()
          .AddTokenProvider<DataProtectorTokenProvider<UserApplication>>(TokenOptions.DefaultProvider);

            //dependency injection
            services.AddScoped<Idepartment, deptRepo>();
            services.AddScoped<IEmployeeRep,EmployeeRep>();
            services.AddScoped<ICountryRep,CountryRepo>();
            services.AddScoped<ICityRep,CityRepo>();
            services.AddScoped<IDistinctRep,DistinctRepo>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { 
             var supportedCultures = new[] {// this step just to insall the languges which i will use
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };

            app.UseRequestLocalization(new RequestLocalizationOptions//2- make stand alone end piont 
            {
                DefaultRequestCulture = new RequestCulture("en-US"),//Defaultla
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
