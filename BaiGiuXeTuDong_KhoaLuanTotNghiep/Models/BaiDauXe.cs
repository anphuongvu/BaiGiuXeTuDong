//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaiGiuXeTuDong_KhoaLuanTotNghiep.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BaiDauXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiDauXe()
        {
            this.LoaiXes = new HashSet<LoaiXe>();
            this.ViTriDauXes = new HashSet<ViTriDauXe>();
        }
    
        public int MaBaiDauXe { get; set; }
        public Nullable<int> MaLoaiXe { get; set; }
        public Nullable<int> SoChoTrong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoaiXe> LoaiXes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViTriDauXe> ViTriDauXes { get; set; }
    }
}
