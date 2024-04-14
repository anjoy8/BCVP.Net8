using BCVP.Net8.Extensions.ServiceExtensions;
using BCVP.Net8.IService;
using BCVP.Net8.Model;
using BCVP.Net8.Model.Vo;
using Blog.Core.Model.Models;
using BCVP.Net8.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Serilog;


namespace BCVP.Net8.Controllers
{
    /// <summary>
    /// 登录管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBaseServices<UserRole, UserRoleVo> _userRoleService;
        readonly PermissionRequirement _requirement;
        private readonly ILogger<LoginController> _logger;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="sysUserInfoServices"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="roleServices"></param>
        /// <param name="requirement"></param>
        /// <param name="roleModulePermissionServices"></param>
        /// <param name="logger"></param>
        public LoginController(
            IUserService userService,
            IBaseServices<UserRole, UserRoleVo> userRoleService,
            PermissionRequirement requirement,
            ILogger<LoginController> logger)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _requirement = requirement;
            _logger = logger;
        }

        [HttpGet]
        public async Task<MessageModel<TokenInfoViewModel>> GetJwtToken3(string name = "", string pass = "")
        {
            string jwtStr = string.Empty;

            pass = MD5Encrypt32(pass);

            Log.Information($"自定义日志 -- {name}-{pass}");
            _logger.LogInformation($"自定义日志 -- {name}-{pass}");

            var user = await _userService.Query(d => d.LoginName == name && d.LoginPWD == pass && d.IsDeleted == false);
            if (user.Count > 0)
            {
                var userRoles = await _userService.GetUserRoleNameStr(name, pass);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(JwtRegisteredClaimNames.Jti, user.FirstOrDefault().Id.ToString()),
                    new Claim("TenantId", user.FirstOrDefault().TenantId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.DateToTimeStamp()),
                    new Claim(ClaimTypes.Expiration,
                        DateTime.Now.AddSeconds(3600).ToString())
                };
                claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                return Success(token, "获取成功");
            }
            else
            {
                return Failed<TokenInfoViewModel>("认证失败");
            }
        }

        private MessageModel<T> Success<T>(T data, string msg = "成功")
        {
            return new MessageModel<T>()
            {
                success = true,
                msg = msg,
                response = data,
            };
        }
        private MessageModel<T> Failed<T>(string msg = "失败", int status = 500)
        {
            return new MessageModel<T>()
            {
                success = false,
                status = status,
                msg = msg,
                response = default,
            };
        }
        private string MD5Encrypt32(string password = "")
        {
            string pwd = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password))
                {
                    MD5 md5 = MD5.Create(); //实例化一个md5对像
                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                    foreach (var item in s)
                    {
                        // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                        pwd = string.Concat(pwd, item.ToString("X2"));
                    }
                }
            }
            catch
            {
                throw new Exception($"错误的 password 字符串:【{password}】");
            }
            return pwd;
        }
    }

}