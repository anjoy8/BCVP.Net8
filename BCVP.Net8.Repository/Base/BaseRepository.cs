using BCVP.Net8.Common.Core;
using BCVP.Net8.Model;
using BCVP.Net8.Repository.UnitOfWorks;
using Newtonsoft.Json;
using SqlSugar;
using System.Reflection;

namespace BCVP.Net8.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly SqlSugarScope _dbBase;
        private readonly IUnitOfWorkManage _unitOfWorkManage;
        public ISqlSugarClient Db => _db;

        private ISqlSugarClient _db
        {
            get
            {
                ISqlSugarClient db = _dbBase;

                //修改使用 model备注字段作为切换数据库条件，使用sqlsugar TenantAttribute存放数据库ConnId
                //参考 https://www.donet5.com/Home/Doc?typeId=2246
                var tenantAttr = typeof(TEntity).GetCustomAttribute<TenantAttribute>();
                if (tenantAttr != null)
                {
                    //统一处理 configId 小写
                    db = _dbBase.GetConnectionScope(tenantAttr.configId.ToString().ToLower());
                    return db;
                }

                return db;
            }
        }


        public BaseRepository(IUnitOfWorkManage unitOfWorkManage)
        {
            _unitOfWorkManage = unitOfWorkManage;
            _dbBase = unitOfWorkManage.GetDbClient();
        }

        public async Task<List<TEntity>> Query()
        {
            await Console.Out.WriteLineAsync(Db.GetHashCode().ToString());
            return await _db.Queryable<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<long> Add(TEntity entity)
        {
            var insert = _db.Insertable(entity);
            return await insert.ExecuteReturnSnowflakeIdAsync();
        }
    }
}
