//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace chuong_trinh_PT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MangXaHoi
    {
        public int MXHID { get; set; }
        public Nullable<int> KhachHangID { get; set; }
        public string TenMangXaHoi { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
    }
}
