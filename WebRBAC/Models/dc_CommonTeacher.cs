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
    
    public partial class dc_CommonTeacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dc_CommonTeacher()
        {
            this.dc_Common_Course_Teacher = new HashSet<dc_Common_Course_Teacher>();
        }
    
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string NamePY { get; set; }
        public string SubTitle { get; set; }
        public string TeacherTel { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherIntro { get; set; }
        public string TeacherDetail { get; set; }
        public string TeacherPhoto { get; set; }
        public bool IsHome { get; set; }
        public System.DateTime AddTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dc_Common_Course_Teacher> dc_Common_Course_Teacher { get; set; }
    }
}
