using BCVP.Net8.Common.Core;
using BCVP.Net8.Model.Tenants;
using SqlSugar;

namespace BCVP.Net8.Common.DB;

public class RepositorySetting
{
    /// <summary>
    /// 配置租户
    /// </summary>
    public static void SetTenantEntityFilter(SqlSugarScopeProvider db)
    {
        if (App.User is not { ID: > 0, TenantId: > 0 })
        {
            return;
        }

        //多租户 单表字段
        db.QueryFilter.AddTableFilter<ITenantEntity>(it => it.TenantId == App.User.TenantId || it.TenantId == 0);
    }
}