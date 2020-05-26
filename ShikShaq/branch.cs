namespace ShikShaq
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shikshaq.branch")]
    public partial class branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public branch()
        {
            product_in_branch = new HashSet<product_in_branch>();
            order = new HashSet<order>();
        }

        public int id { get; set; }

        [StringLength(45)]
        public string name { get; set; }

        public DateTime? date_opened { get; set; }

        [StringLength(45)]
        public string address { get; set; }

        public float? lat { get; set; }

        public float? lng { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_in_branch> product_in_branch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> order { get; set; }
    }
}
