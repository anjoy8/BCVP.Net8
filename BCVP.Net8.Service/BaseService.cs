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

        public BaseServices(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<TVo>> Query()
        {
            var baseRepo = new BaseRepository<TEntity>();
            var entities = await baseRepo.Query();
            var llout = _mapper.Map<List<TVo>>(entities);
            return llout;
        }
    }
}
