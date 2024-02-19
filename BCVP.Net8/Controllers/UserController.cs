using BCVP.Net8.Common.HttpContextUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCVP.Net8.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize("Permission")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUser _user;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(ILogger<UserController> logger,
            IUser user,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _user = user;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/User
        [HttpGet]
        public string Get(int page = 1, string key = "")
        {
            long iD = _user.ID;
            _logger.LogInformation(key, page);
            return "OK!!!";
        }


        // GET: api/User/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            _logger.LogError("test wrong");
            return "value";
        }
    }
}