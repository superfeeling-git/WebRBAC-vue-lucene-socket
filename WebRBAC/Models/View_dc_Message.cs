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
    
    public partial class View_dc_Message
    {
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public byte UserType { get; set; }
        public string TeacherName { get; set; }
        public string Tel { get; set; }
        public bool Pick { get; set; }
        public string Content { get; set; }
        public string IP { get; set; }
        public System.DateTime AddTime { get; set; }
        public byte Source { get; set; }
        public string SourceName { get; set; }
    }
}
