using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BCVP.Net8.Model;
using BCVP.Net8.Model.Tenants;
using Blog.Core.Model.Models;
using SqlSugar;

namespace BCVP.Net8.Common.DB;

public static class TenantUtil
{
    public static List<Type> GetTenantEntityTypes(TenantTypeEnum? tenantType = null)
    {
        return RepositorySetting.Entitys
            .Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass)
            .Where(s => IsTenantEntity(s, tenantType))
            .ToList();
    }

    public static bool IsTenantEntity(this Type u, TenantTypeEnum? tenantType = null)
    {
        var mta = u.GetCustomAttribute<MultiTenantAttribute>();
        if (mta is null)
        {
            return false;
        }

        if (tenantType != null)
        {
            if (mta.TenantType != tenantType)
            {
                return false;
            }
        }

        return true;
    }

    public static string GetTenantTableName(this Type type, ISqlSugarClient db, string id)
    {
        var entityInfo = db.EntityMaintenance.GetEntityInfo(type);
        return $@"{entityInfo.DbTableName}_{id}";
    }

    public static void SetTenantTable(this ISqlSugarClient db, string id)
    {
        var types = GetTenantEntityTypes(TenantTypeEnum.Tables);

        foreach (var type in types)
        {
            db.MappingTables.Add(type.Name, type.GetTenantTableName(db, id));
        }
    }

    public static ConnectionConfig GetConnectionConfig(this SysTenant tenant)
    {
        if (tenant.DbType is null)
        {
            throw new ArgumentException("Tenant DbType Must");
        }

        return new ConnectionConfig()
        {
            ConfigId = tenant.ConfigId,
            DbType = tenant.DbType.Value,
            ConnectionString = tenant.Connection,
            IsAutoCloseConnection = true,
            MoreSettings = new ConnMoreSettings()
            {
                IsAutoRemoveDataCache = true,
                SqlServerCodeFirstNvarchar = true,
            },
        };
    }
}