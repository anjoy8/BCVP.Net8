using BCVP.Net8.Model;
using Newtonsoft.Json;

namespace BCVP.Net8.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<List<SysUserInfo>> Query()
        {
            await Task.CompletedTask;
            var data = "[{\"Id\": 18,\"Name\":\"laozhang\"}]";
            return JsonConvert.DeserializeObject<List<SysUserInfo>>(data) ?? new List<SysUserInfo>();
        }
    }
}
