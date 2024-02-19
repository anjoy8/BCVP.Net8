using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCVP.Net8.Model
{
    public class UserVo
    {
        public long Id { get; set; }
        public string UserName { get; set; }

        public string LoginName { get; set; }

        public string LoginPWD { get; set; }

        public string RealName { get; set; }

        public int Status { get; set; } = 0;

        public long DepartmentId { get; set; } = -1;

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public DateTime CriticalModifyTime { get; set; } = DateTime.Now;

        public DateTime LastErrorTime { get; set; } = DateTime.Now;

        public int ErrorCount { get; set; } = 0;

        public string Name { get; set; }

        public int Sex { get; set; } = 0;

        public int Age { get; set; }

        public DateTime Birth { get; set; } = DateTime.Now;

        public string Address { get; set; }

        public bool Enable { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public long TenantId { get; set; }

        public List<string> RoleNames { get; set; }

        public List<long> Dids { get; set; }

        public string DepartmentName { get; set; }

        public List<long> RIDs { get; set; }
    }

}
