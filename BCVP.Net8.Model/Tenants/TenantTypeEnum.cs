using System.ComponentModel;

namespace BCVP.Net8.Model.Tenants;

/// <summary>
/// 租户隔离方案
/// </summary>
public enum TenantTypeEnum
{
    None = 0,

    /// <summary>
    /// Id隔离
    /// </summary>
    [Description("Id隔离")]
    Id = 1,

    /// <summary>
    /// 表隔离
    /// </summary>
    [Description("表隔离")]
    Tables = 3,
}