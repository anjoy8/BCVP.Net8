using SqlSugar;

namespace BCVP.Net8.Model
{
    public class SysUserInfo : RootEntityTkey<long>
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "登录账号")]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string LoginPWD { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string RealName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 部门
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public long DepartmentId { get; set; } = -1;

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 关键业务修改时间
        /// </summary>
        public DateTime CriticalModifyTime { get; set; } = DateTime.Now;

        /// <summary>
        ///最后异常时间 
        /// </summary>
        public DateTime LastErrorTime { get; set; } = DateTime.Now;

        /// <summary>
        ///错误次数 
        /// </summary>
        public int ErrorCount { get; set; } = 0;


        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Name { get; set; }

        // 性别
        [SugarColumn(IsNullable = true)]
        public int Sex { get; set; } = 0;

        // 年龄
        [SugarColumn(IsNullable = true)]
        public int Age { get; set; }

        // 生日
        [SugarColumn(IsNullable = true)]
        public DateTime Birth { get; set; } = DateTime.Now;

        // 地址
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Address { get; set; }

        [SugarColumn(DefaultValue = "1")]
        public bool Enable { get; set; } = true;

        [SugarColumn(IsNullable = true)]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 租户Id
        /// </summary>
        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public long TenantId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<string> RoleNames { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<long> Dids { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string DepartmentName { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<long> RIDs { get; set; }
    }
}
