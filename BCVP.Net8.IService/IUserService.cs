using BCVP.Net8.Model;

namespace BCVP.Net8.IService
{
    public interface IBaseServices<TEntity, TVo> where TEntity : class
    {
        Task<List<TVo>> Query();
    }
}
