using BCVP.Net8.Model;
using SqlSugar;

namespace BCVP.Net8.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        ISqlSugarClient Db { get; }

        Task<long> Add(TEntity entity);
        Task<List<TEntity>> Query();
    }
}
