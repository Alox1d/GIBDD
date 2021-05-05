using DAL;
using DAL.Entities;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL_ASP_5
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
            //services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ГИБДД", Version = "v1" });
            });
            //services.AddDbContext<GIBDDContext>(opt => opt.UseInMemoryDatabase("GIBDD"));
            services.AddDbContext<GIBDDContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            services.AddControllers().AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             });
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<GIBDDContext>();
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.WithOrigins("https://localhost:44314")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
                });
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            CreateUserRoles(services.BuildServiceProvider()).Wait();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "GIBDD"; 
                options.LoginPath = "/"; 
                options.AccessDeniedPath = "/"; 
                options.LogoutPath = "/";
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GIBDDContext context)
        {
            //app.UseCors(opt => opt.WithOrigins("https://localhost:3000")
            //.AllowAnyHeader()
            //.AllowAnyMethod());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PL_ASP_5 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            //ContextInizializer.SeedData(context); // Загрузка данных

            app.UseCors("EnableCORS");

        }
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager =
 serviceProvider.GetRequiredService<UserManager<User>>();

            // Создание ролей сотрудника ДПС и оператора аналитического отдела 
            if (await roleManager.FindByNameAsync("inspector") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("inspector"));
            }
            if (await roleManager.FindByNameAsync("operator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("operator"));
            }

            // Создание сотрудника ДПС 
            string insEmail = "inspector@mail.com";
            string insPass = "Qwe123!";
            if (await userManager.FindByNameAsync(insEmail) == null)
            {
                User inspector = new User { Email = insEmail, UserName = insEmail };
                IdentityResult result = await userManager.CreateAsync(inspector, insPass);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(inspector, "inspector");
                }
            }

            // Создание оператора 
            string opEmail = "oper@mail.com";
            string opPassword = "Qwe123!";
            if (await userManager.FindByNameAsync(opEmail) == null)
            {
                User user = new User { Email = opEmail, UserName = opEmail };
                IdentityResult result = await userManager.CreateAsync(user, opPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
        }

    }
}
