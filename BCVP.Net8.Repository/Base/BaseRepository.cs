using BCVP.Net8.Common.Core;
using BCVP.Net8.Model;
using Newtonsoft.Json;
using SqlSugar;
using System.Reflection;

namespace BCVP.Net8.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly ISqlSugarClient _dbBase;
        public BaseRepository(ISqlSugarClient sqlSugarClient)
        {
            _dbBase = sqlSugarClient;
        }


        public ISqlSugarClient Db => _dbBase;

        public async Task<List<TEntity>> Query()
        {
            await Console.Out.WriteLineAsync(Db.GetHashCode().ToString());
            return await _dbBase.Queryable<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<long> Add(TEntity entity)
        {
            var insert = _dbBase.Insertable(entity);
            return await insert.ExecuteReturnSnowflakeIdAsync();
        }
    }
}
