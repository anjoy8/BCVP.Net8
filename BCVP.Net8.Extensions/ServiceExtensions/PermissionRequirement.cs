using Microsoft.AspNetCore.Authorization;

namespace BCVP.Net8.Extensions.ServiceExtensions
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public List<PermissionItem> Permissions { get; set; } = new List<PermissionItem>();
    }
}
