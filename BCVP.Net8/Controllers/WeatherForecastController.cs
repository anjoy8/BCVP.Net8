using AutoMapper;
using BCVP.Net8.Common;
using BCVP.Net8.IService;
using BCVP.Net8.Model;
using BCVP.Net8.Service;
using Microsoft.AspNetCore.Mvc;

namespace BCVP.Net8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public IBaseServices<Role, RoleVo> _roleServiceObj { get; set; }

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBaseServices<Role, RoleVo> _roleService;
        private readonly IServiceScopeFactory _scopeFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBaseServices<Role, RoleVo> roleService, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _roleService = roleService;
            _scopeFactory = scopeFactory;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<object> Get()
        {
            Console.WriteLine("api request begin...");

            //var userService = new UserService();
            //var userList = await userService.Query();
            //return userList;


            //var roleService = new BaseServices<Role, RoleVo>(_mapper);
            //var roleList = await roleService.Query();


            //var roleList = await _roleService.Query();
            //Console.WriteLine(_roleService.GetHashCode());
            //var roleList2 = await _roleService.Query();
            //Console.WriteLine(_roleService.GetHashCode());


            //using var scope = _scopeFactory.CreateScope();
            //var _dataStatisticService = scope.ServiceProvider.GetRequiredService<IBaseServices<Role, RoleVo>>();
            //var roleList1 = await _dataStatisticService.Query();
            //var _dataStatisticService2 = scope.ServiceProvider.GetRequiredService<IBaseServices<Role, RoleVo>>();
            //var roleList21 = await _dataStatisticService2.Query();


            var roleList = await _roleServiceObj.Query();
            var redisEnable = AppSettings.app(new string[] { "Redis", "Enable" });
            var redisConnectionString = AppSettings.GetValue("Redis:ConnectionString");
            Console.WriteLine($"Enable: {redisEnable} ,  ConnectionString: {redisConnectionString}");


            Console.WriteLine("api request end...");
            return roleList;
        }
    }
}
