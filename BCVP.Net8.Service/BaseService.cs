using AutoMapper;
using BCVP.Net8.IService;
using BCVP.Net8.Model;
using BCVP.Net8.Repository;
using SqlSugar;
using System.Linq.Expressions;
using System.Reflection;

namespace BCVP.Net8.Service
{
    public class BaseServices<TEntity, TVo> : IBaseServices<TEntity, TVo> where TEntity : class, new()
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity> _baseRepository;
        public ISqlSugarClient Db => _baseRepository.Db;

        public BaseServices(IMapper mapper, IBaseRepository<TEntity> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public async Task<List<TVo>> Query(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            var entities = await _baseRepository.Query(whereExpression);
            Console.WriteLine($"_baseRepository 实例HashCode ： {_baseRepository.GetHashCode()}");
            var llout = _mapper.Map<List<TVo>>(entities);
            return llout;
        }

        public async Task<List<TVo>> QueryWithCache(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            var entities = await _baseRepository.QueryWithCache(whereExpression);
            Console.WriteLine($"_baseRepository 实例HashCode ： {_baseRepository.GetHashCode()}");
            var llout = _mapper.Map<List<TVo>>(entities);
            return llout;
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<long> Add(TEntity entity)
        {
            return await _baseRepository.Add(entity);
        }

        public async Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null)
        {
            return await _baseRepository.QuerySplit(whereExpression, orderByFields);
        }

        public async Task<List<long>> AddSplit(TEntity entity)
        {
            return await _baseRepository.AddSplit(entity);
        }
    }
}
