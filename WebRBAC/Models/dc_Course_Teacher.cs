//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebRBAC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class dc_Course_Teacher
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int TeacherID { get; set; }
    
        public virtual dc_Course dc_Course { get; set; }
        public virtual dc_Teacher dc_Teacher { get; set; }
    }
}
