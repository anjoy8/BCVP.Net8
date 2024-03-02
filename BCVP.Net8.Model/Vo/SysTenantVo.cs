using BCVP.Net8.Model.Tenants;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCVP.Net8.Model
{
    public class SysTenantVo
    {
        public string Name { get; set; }
        public TenantTypeEnum TenantType { get; set; }
        public string ConfigId { get; set; }
        public string Host { get; set; }
        public DbType? DbType { get; set; }
        public string Connection { get; set; }
        public bool Status { get; set; } = true;
        public string Remark { get; set; }
    }
}