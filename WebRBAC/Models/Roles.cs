using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRBAC.Models
{
    [Table("Roles")]
    public class Roles
    {
        public Roles()
        {
            this.RolesClass = new List<RolesClass>();
        }

        [Key]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "请输入角色名称")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "请选择添加时间")]
        public DateTime AddTime { get; set; }
        public List<RolesClass> RolesClass { get; set; }
    }

    public class RolesClass
    {

        [Key]
        [Column(Order = 1)]
        public int RoleID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ClassID { get; set; }
    }
}