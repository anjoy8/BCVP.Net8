using BCVP.Net8.Model;
using BCVP.Net8.Repository.UnitOfWorks;
using Blog.Core.Model.Models;
using Newtonsoft.Json;
using SqlSugar;

namespace BCVP.Net8.Repository
{
    public class UserRepository : BaseRepository<SysUserInfo>, IUserRepository
    {
        public UserRepository(IUnitOfWorkManage unitOfWorkManage) : base(unitOfWorkManage)
        {
        }

        public async Task<List<SysUserInfo>> Query()
        {
            await Task.CompletedTask;
            var data = "[{\"Id\": 18,\"Name\":\"laozhang\"}]";
            return JsonConvert.DeserializeObject<List<SysUserInfo>>(data) ?? new List<SysUserInfo>();
        }

        public async Task<List<RoleModulePermission>> RoleModuleMaps()
        {
            return await QueryMuch<RoleModulePermission, Modules, Role, RoleModulePermission>(
                (rmp, m, r) => new object[] {
                    JoinType.Left, rmp.ModuleId == m.Id,
                    JoinType.Left,  rmp.RoleId == r.Id
                },

                (rmp, m, r) => new RoleModulePermission()
                {
                    Role = r,
                    Module = m,
                    IsDeleted = rmp.IsDeleted
                },

                (rmp, m, r) => rmp.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false
                );
        }

    }
}
