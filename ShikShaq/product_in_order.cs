namespace ShikShaq
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shikshaq.product_in_order")]
    public partial class product_in_order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        public int order_id { get; set; }

        public int? quantity { get; set; }

        public virtual order order { get; set; }

        public virtual product product { get; set; }
    }
}
