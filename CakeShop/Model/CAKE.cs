//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CakeShop.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAKE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAKE()
        {
            this.RECEIPT_DETAIL = new HashSet<RECEIPT_DETAIL>();
        }
    
        public int ID { get; set; }
        public string C_NAME { get; set; }
        public Nullable<int> TYPEID { get; set; }
        public Nullable<int> PRICE { get; set; }
        public string IMG { get; set; }
    
        public virtual CAKE_TYPE CAKE_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECEIPT_DETAIL> RECEIPT_DETAIL { get; set; }
    }
}
