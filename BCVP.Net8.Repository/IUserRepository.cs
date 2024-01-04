using BCVP.Net8.Model;

namespace BCVP.Net8.Repository
{
    public interface IUserRepository
    {
        Task<List<SysUserInfo>> Query();
    }
}
