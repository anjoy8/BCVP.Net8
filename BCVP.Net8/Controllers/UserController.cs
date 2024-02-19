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

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        // GET: api/User
        [HttpGet]
        public string Get(int page = 1, string key = "")
        {
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