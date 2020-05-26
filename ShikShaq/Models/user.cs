namespace ShikShaq
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shikshaq.user")]
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            order = new HashSet<order>();
        }

        public int id { get; set; }

        [StringLength(45)]
        public string name { get; set; }

        [StringLength(45)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required!")]
        public string email { get; set; }

        public DateTime? birthday { get; set; }

        [StringLength(45)]
        public string address { get; set; }

        public float? height { get; set; }

        public float? weight { get; set; }

        [StringLength(45)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required!")]
        public string password { get; set; }

        [StringLength(1)]
        public string is_admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> order { get; set; }
    }
}
