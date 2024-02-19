using BCVP.Net8.Model;
using Blog.Core.Model.Models;

namespace BCVP.Net8.Repository
{
    public interface IUserRepository
    {
        Task<List<SysUserInfo>> Query();
        Task<List<RoleModulePermission>> RoleModuleMaps();
    }
}
