namespace ShikShaq
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shikshaq.product")]
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            product_in_branch = new HashSet<product_in_branch>();
            tag = new HashSet<tag>();
        }

        public int id { get; set; }

        [StringLength(45)]
        public string name { get; set; }

        [StringLength(45)]
        public string description { get; set; }

        [StringLength(45)]
        public string price { get; set; }

        [StringLength(45)]
        public string color { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_in_branch> product_in_branch { get; set; }

        public virtual product_in_order product_in_order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tag> tag { get; set; }
    }
}
