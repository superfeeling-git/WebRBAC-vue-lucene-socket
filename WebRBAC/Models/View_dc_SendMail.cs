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
    
    public partial class View_dc_SendMail
    {
        public int MailID { get; set; }
        public byte Source { get; set; }
        public int UID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime AddTime { get; set; }
        public int SendID { get; set; }
        public string ToMail { get; set; }
        public Nullable<System.DateTime> SendTime { get; set; }
        public bool SendState { get; set; }
        public Nullable<bool> ResultState { get; set; }
    }
}
