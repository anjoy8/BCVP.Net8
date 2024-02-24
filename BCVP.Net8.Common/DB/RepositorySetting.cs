using BCVP.Net8.Common.Core;
using BCVP.Net8.Model;
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

        //多租户 多表
        db.SetTenantTable(App.User.TenantId.ToString());
    }

    private static readonly Lazy<IEnumerable<Type>> AllEntitys = new(() =>
    {
        return typeof(RootEntityTkey<>).Assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(it => it.FullName != null && it.FullName.StartsWith("BCVP.Net8.Model"));
    });
    public static IEnumerable<Type> Entitys => AllEntitys.Value;
}