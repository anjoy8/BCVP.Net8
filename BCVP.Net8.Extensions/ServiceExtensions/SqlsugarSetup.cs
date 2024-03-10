using BCVP.Net8.Common;
using BCVP.Net8.Common.Caches;
using BCVP.Net8.Common.Core;
using BCVP.Net8.Common.DB;
using Blog.Core.Common.Const;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Text.RegularExpressions;

namespace BCVP.Net8.Extensions.ServiceExtensions
{
    /// <summary>
    /// SqlSugar 启动服务
    /// </summary>
    public static class SqlsugarSetup
    {
        public static void AddSqlsugarSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 默认添加主数据库连接
            if (!string.IsNullOrEmpty(AppSettings.app("MainDB")))
            {
                MainDb.CurrentDbConnId = AppSettings.app("MainDB");
            }

            BaseDBConfig.MutiConnectionString.allDbs.ForEach(m =>
            {
                var config = new ConnectionConfig()
                {
                    ConfigId = m.ConnId.ObjToString().ToLower(),
                    ConnectionString = m.Connection,
                    DbType = (DbType)m.DbType,
                    IsAutoCloseConnection = true,
                    MoreSettings = new ConnMoreSettings()
                    {
                        IsAutoRemoveDataCache = true,
                        SqlServerCodeFirstNvarchar = true,
                    },
                    // 自定义特性
                    ConfigureExternalServices = new ConfigureExternalServices()
                    {
                        DataInfoCacheService = new SqlSugarCacheService(),
                    },
                    InitKeyType = InitKeyType.Attribute
                };
                if (SqlSugarConst.LogConfigId.ToLower().Equals(m.ConnId.ToLower()))
                {
                    BaseDBConfig.LogConfig = config;
                }
                else
                {
                    BaseDBConfig.ValidConfig.Add(config);
                }

                BaseDBConfig.AllConfigs.Add(config);
            });

            if (BaseDBConfig.LogConfig is null)
            {
                throw new ApplicationException("未配置Log库连接");
            }

            // SqlSugarScope是线程安全，可使用单例注入
            // 参考：https://www.donet5.com/Home/Doc?typeId=1181
            services.AddSingleton<ISqlSugarClient>(o =>
            {
                return new SqlSugarScope(BaseDBConfig.AllConfigs, db =>
                {
                    BaseDBConfig.ValidConfig.ForEach(config =>
                    {
                        var dbProvider = db.GetConnectionScope((string)config.ConfigId);
                        // 配置实体数据权限（多租户）
                        RepositorySetting.SetTenantEntityFilter(dbProvider);

                        // 打印SQL语句
                        dbProvider.Aop.OnLogExecuting = (s, parameters) =>
                        {
                            SqlSugarAop.OnLogExecuting(dbProvider, App.User?.Name.ObjToString(), ExtractTableName(s),
                                Enum.GetName(typeof(SugarActionType), dbProvider.SugarActionType), s, parameters,
                                config);
                        };
                    });
                });
            });
        }
        private static string ExtractTableName(string sql)
        {
            // 匹配 SQL 语句中的表名的正则表达式
            //string regexPattern = @"\s*(?:UPDATE|DELETE\s+FROM|SELECT\s+\*\s+FROM)\s+(\w+)";
            string regexPattern = @"(?i)(?:FROM|UPDATE|DELETE\s+FROM)\s+`(.+?)`";
            Regex regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(sql);

            if (match.Success)
            {
                // 提取匹配到的表名
                return match.Groups[1].Value;
            }
            else
            {
                // 如果没有匹配到表名，则返回空字符串或者抛出异常等处理
                return string.Empty;
            }
        }

    }
}