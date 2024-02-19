using SqlSugar;

namespace BCVP.Net8.Model;

public class AuditSqlLogVo
{
    public DateTime? DateTime { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public string MessageTemplate { get; set; }
    public string Properties { get; set; }
}