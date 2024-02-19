using BCVP.Net8.Model;
using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 用户跟角色关联表
    /// </summary>
    public class UserRoleVo
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }

        public bool? IsDeleted { get; set; }

        public long? CreateId { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? ModifyId { get; set; }

        public string ModifyBy { get; set; }

        public DateTime? ModifyTime { get; set; }

    }
}
