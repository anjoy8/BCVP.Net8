using AutoMapper;
using BCVP.Net8.IService;
using BCVP.Net8.Model;
using BCVP.Net8.Repository;
using System.Reflection;

namespace BCVP.Net8.Service
{
    public class BaseServices<TEntity, TVo> : IBaseServices<TEntity, TVo> where TEntity : class, new()
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseServices(IMapper mapper, IBaseRepository<TEntity> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public async Task<List<TVo>> Query()
        {
            var entities = await _baseRepository.Query();
            Console.WriteLine($"_baseRepository 实例HashCode ： {_baseRepository.GetHashCode()}");
            var llout = _mapper.Map<List<TVo>>(entities);
            return llout;
        }
    }
}
