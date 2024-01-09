using BCVP.Net8.Model;
using SqlSugar;

namespace BCVP.Net8.IService
{
    public interface IBaseServices<TEntity, TVo> where TEntity : class
    {
        ISqlSugarClient Db { get; }

        Task<long> Add(TEntity entity);
        Task<List<long>> AddSplit(TEntity entity);
        Task<List<TVo>> Query();
        Task<List<TEntity>> QuerySplit(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);
    }
}
