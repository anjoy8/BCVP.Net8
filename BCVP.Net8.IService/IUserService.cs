using BCVP.Net8.Model;

namespace BCVP.Net8.IService
{
    public interface IUserService
    {
        Task<List<UserVo>> Query();
        Task<bool> TestTranPropagation();
    }
}
