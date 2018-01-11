//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemirbasTakipOtomasyonu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Demirbas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Demirbas()
        {
            this.OdaDemirbasAtama = new HashSet<OdaDemirbasAtama>();
        }
    
        public int Id { get; set; }
        public string Ad { get; set; }
        public decimal Fiyat { get; set; }
        public System.DateTime AlımTarihi { get; set; }
        public int Adet { get; set; }
        public int DemirbasTurId { get; set; }
        public int FakulteId { get; set; }
        public int DepartmanId { get; set; }
    
        public virtual DemirbasTuru DemirbasTuru { get; set; }
        public virtual Departman Departman { get; set; }
        public virtual Fakulte Fakulte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OdaDemirbasAtama> OdaDemirbasAtama { get; set; }
    }
}
