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
    
    public partial class dc_Notice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dc_Notice()
        {
            this.dc_Notice_Staff = new HashSet<dc_Notice_Staff>();
        }
    
        public int NoticeID { get; set; }
        public int EnterpriseID { get; set; }
        public string Title { get; set; }
        public string TitleColor { get; set; }
        public string Content { get; set; }
        public bool IsHome { get; set; }
        public bool IsTop { get; set; }
        public System.DateTime AddTime { get; set; }
    
        public virtual dc_Enterprise dc_Enterprise { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dc_Notice_Staff> dc_Notice_Staff { get; set; }
    }
}
