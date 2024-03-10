
using SqlSugar;

namespace BCVP.Net8.Common.DB;

public static class SqlSugarAop
{
    public static void OnLogExecuting(ISqlSugarClient sqlSugarScopeProvider, string user, string table, string operate, string sql, SugarParameter[] p, ConnectionConfig config)
    {
        try
        {
            var logConsole = string.Format($"------------------ \r\n User:[{user}]  Table:[{table}]  Operate:[{operate}] " +
                $"ConnId:[{config.ConfigId}]【SQL语句】: " +
                $"\r\n {UtilMethods.GetNativeSql(sql, p)}");
            Console.WriteLine(logConsole);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured OnLogExcuting:" + e);
        }
    }

}