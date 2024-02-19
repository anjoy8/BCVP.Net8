
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BCVP.Net8.Common;
using BCVP.Net8.Extensions;
using BCVP.Net8.Extensions.ServiceExtensions;
using BCVP.Net8.Common.Option.Core;
using BCVP.Net8.IService;
using BCVP.Net8.Repository;
using BCVP.Net8.Service;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BCVP.Net8.Common.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using BCVP.Net8.Common.HttpContextUser;

namespace BCVP.Net8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule<AutofacModuleRegister>();
                    builder.RegisterModule<AutofacPropertityModuleReg>();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    hostingContext.Configuration.ConfigureApplication();
                })
                ;

            builder.ConfigureApplication();
            // Add services to the container.

            builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();

            // 配置
            builder.Services.AddSingleton(new AppSettings(builder.Configuration));
            builder.Services.AddAllOptionRegister();

            // 缓存
            builder.Services.AddCacheSetup();

            // ORM
            builder.Services.AddSqlsugarSetup();
            // JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "Blog.Core",
                        ValidAudience = "wr",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs"))
                    };
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireClaim("iss", "Blog.Core").Build());
                options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("SuperAdmin", "System"));

                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement()));
            });
            builder.Services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
            builder.Services.AddSingleton(new PermissionRequirement());


            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IUser, AspNetUser>();


            var app = builder.Build();
            app.ConfigureApplication();
            app.UseApplicationSetup();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
