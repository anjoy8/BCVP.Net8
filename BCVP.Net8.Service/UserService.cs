using AutoMapper;
using BCVP.Net8.Common;
using BCVP.Net8.IService;
using BCVP.Net8.Model;
using BCVP.Net8.Repository;
using Blog.Core.Model.Models;

namespace BCVP.Net8.Service
{
    public class UserService : BaseServices<SysUserInfo, UserVo>, IUserService
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IBaseRepository<UserRole> _userRoleRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IDepartmentServices departmentServices,
            IBaseRepository<Role> roleRepository,
            IBaseRepository<UserRole> userRoleRepository,
            IUserRepository userRepository, 
            IMapper mapper, IBaseRepository<SysUserInfo> baseRepository) : base(mapper, baseRepository)
        {
            _departmentServices = departmentServices;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }


        /// <summary>
        /// 测试使用同事务
        /// </summary>
        /// <returns></returns>
        [UseTran(Propagation = Propagation.Required)]
        public async Task<bool> TestTranPropagation()
        {
            await Console.Out.WriteLineAsync($"db context id : {base.Db.ContextID}");
            var sysUserInfos = await base.Query();

            TimeSpan timeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var id = timeSpan.TotalSeconds.ObjToLong();
            var insertSysUserInfo = await base.Add(new SysUserInfo()
            {
                Id = id,
                Name = $"user name {id}",
                Status = 0,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                CriticalModifyTime = DateTime.Now,
                LastErrorTime = DateTime.Now,
                ErrorCount = 0,
                Enable = true,
                TenantId = 0,
            });

            await _departmentServices.TestTranPropagation2();

            return true;
        }

        public async Task<string> GetUserRoleNameStr(string loginName, string loginPwd)
        {
            string roleName = "";
            var user = (await base.Query(a => a.LoginName == loginName && a.LoginPWD == loginPwd)).FirstOrDefault();
            var roleList = await _roleRepository.Query(a => a.IsDeleted == false);
            if (user != null)
            {
                var userRoles = await _userRoleRepository.Query(ur => ur.UserId == user.Id);
                if (userRoles.Count > 0)
                {
                    var arr = userRoles.Select(ur => ur.RoleId.ObjToString()).ToList();
                    var roles = roleList.Where(d => arr.Contains(d.Id.ObjToString()));

                    roleName = string.Join(',', roles.Select(r => r.Name).ToArray());
                }
            }
            return roleName;
        }

        public async Task<List<RoleModulePermission>> RoleModuleMaps()
        {
            return await _userRepository.RoleModuleMaps();
        }
    }
}
