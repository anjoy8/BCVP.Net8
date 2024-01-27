using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCVP.Net8.Extensions.ServiceExtensions
{
    public class PermissionRequirement : AuthorizationHandler<PermissionRequirement>, IAuthorizationRequirement
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            await Task.CompletedTask;
            context.Succeed(requirement);
            return;
        }
    }
}
