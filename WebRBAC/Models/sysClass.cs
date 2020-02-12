using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebRBAC.Models
{
    [Table("sysClass")]
    public class sysClass
    {
        public sysClass()
        {
            this.RolesClass = new List<RolesClass>();
        }

        [Key]
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int ParentID { get; set; }
        public string ParentPath { get; set; }
        public int Depth { get; set; }
        public List<RolesClass> RolesClass { get; set; }
    }
}