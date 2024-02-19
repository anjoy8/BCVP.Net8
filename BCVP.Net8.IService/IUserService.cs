using BCVP.Net8.Model;
using Blog.Core.Model.Models;
using System.Linq.Expressions;

namespace BCVP.Net8.IService
{
    public interface IUserService : IBaseServices<SysUserInfo, UserVo>
    {
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
        Task<List<RoleModulePermission>> RoleModuleMaps();
        Task<bool> TestTranPropagation();
    }
}
