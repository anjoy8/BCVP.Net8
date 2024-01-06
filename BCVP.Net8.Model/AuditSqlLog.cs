
using SqlSugar;

namespace BCVP.Net8.Model;

[Tenant("log")]
[SugarTable("AuditSqlLog_20231201", "Sql审计日志")] //('数据库表名'，'数据库表备注')
public class AuditSqlLog : BaseLog
{

}