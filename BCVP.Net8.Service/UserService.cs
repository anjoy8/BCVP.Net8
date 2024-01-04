using AutoMapper;
using BCVP.Net8.Common;
using BCVP.Net8.IService;
using BCVP.Net8.Model;
using BCVP.Net8.Repository;

namespace BCVP.Net8.Service
{
    public class UserService : BaseServices<SysUserInfo, UserVo>, IUserService
    {
        private readonly IDepartmentServices _departmentServices;

        public UserService(IDepartmentServices departmentServices, IMapper mapper, IBaseRepository<SysUserInfo> baseRepository) : base(mapper, baseRepository)
        {
            _departmentServices = departmentServices;
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
    }
}
