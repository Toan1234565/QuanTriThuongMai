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
    
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            this.LichSuDonHang = new HashSet<LichSuDonHang>();
        }
    
        public int DonHangID { get; set; }
        public Nullable<int> KhachHangID { get; set; }
        public Nullable<System.DateTime> NgayDatHang { get; set; }
        public string TrangThai { get; set; }
        public Nullable<double> TongTien { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuDonHang> LichSuDonHang { get; set; }
    }
}
