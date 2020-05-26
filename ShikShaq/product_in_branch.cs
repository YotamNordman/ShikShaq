namespace ShikShaq
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shikshaq.product_in_branch")]
    public partial class product_in_branch
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int branch_id { get; set; }

        public int? quantity { get; set; }

        public virtual branch branch { get; set; }

        public virtual product product { get; set; }
    }
}
