
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
