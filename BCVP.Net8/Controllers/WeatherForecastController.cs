using AutoMapper;
using BCVP.Net8.Common;
using BCVP.Net8.Common.Caches;
using BCVP.Net8.Common.Core;
using BCVP.Net8.Common.Option;
using BCVP.Net8.IService;
using BCVP.Net8.Model;
using BCVP.Net8.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BCVP.Net8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "SuperAdmin")]
    //[Authorize(Policy = "Client")]
    [Authorize("Permission")]
    public class WeatherForecastController : ControllerBase
    {
        public IBaseServices<Role, RoleVo>? _roleServiceObj { get; set; }

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBaseServices<Role, RoleVo> _roleService;
        private readonly IBaseServices<AuditSqlLog, AuditSqlLogVo> _auditSqllogService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ICaching _caching;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<RedisOptions> _redisOptions;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IBaseServices<Role, RoleVo> roleService,
            IBaseServices<AuditSqlLog, AuditSqlLogVo> auditSqllogService,
            IServiceScopeFactory scopeFactory,
            ICaching caching,
            IHttpContextAccessor httpContextAccessor,
            IOptions<RedisOptions> options)
        {
            _logger = logger;
            _roleService = roleService;
            _auditSqllogService = auditSqllogService;
            _scopeFactory = scopeFactory;
            _caching = caching;
            _httpContextAccessor = httpContextAccessor;
            _redisOptions = options;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<object> Get()
        {
            Console.WriteLine("api request begin...");
            var httpContext = _httpContextAccessor.HttpContext?.User.Claims.ToList();
            foreach (var item in httpContext)
            {
                await Console.Out.WriteLineAsync($"{item.Type} : {item.Value}");
            }
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
            //var redisEnable = AppSettings.app(new string[] { "Redis", "Enable" });
            //var redisConnectionString = AppSettings.GetValue("Redis:ConnectionString");
            //Console.WriteLine($"Enable: {redisEnable} ,  ConnectionString: {redisConnectionString}");


            //var redisOptions = _redisOptions.Value;
            //Console.WriteLine(JsonConvert.SerializeObject(redisOptions));

            //var roleServiceObjNew = App.GetService<IBaseServices<Role, RoleVo>>(false);
            //var roleList = await roleServiceObjNew.Query();
            //var redisOptions = App.GetOptions<RedisOptions>();

            //var cacheKey = "cache-key";
            //List<string> cacheKeys = await _caching.GetAllCacheKeysAsync();
            //await Console.Out.WriteLineAsync("全部keys -->" + JsonConvert.SerializeObject(cacheKeys));

            //await Console.Out.WriteLineAsync("添加一个缓存");
            //await _caching.SetStringAsync(cacheKey, "hi laozhang");
            //await Console.Out.WriteLineAsync("全部keys -->" + JsonConvert.SerializeObject(await _caching.GetAllCacheKeysAsync()));
            //await Console.Out.WriteLineAsync("当前key内容-->" + JsonConvert.SerializeObject(await _caching.GetStringAsync(cacheKey)));

            //await Console.Out.WriteLineAsync("删除key");
            //await _caching.RemoveAsync(cacheKey);
            //await Console.Out.WriteLineAsync("全部keys -->" + JsonConvert.SerializeObject(await _caching.GetAllCacheKeysAsync()));

            TimeSpan timeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var id = timeSpan.TotalSeconds.ObjToLong();
            await _auditSqllogService.AddSplit(new AuditSqlLog()
            {
                Id = id,
                DateTime = Convert.ToDateTime("2023-12-23"),
            });

            var rltList = await _auditSqllogService.QuerySplit(d => d.DateTime <= Convert.ToDateTime("2023-12-24"));

            Console.WriteLine("api request end...");
            return rltList;
        }
    }
}
