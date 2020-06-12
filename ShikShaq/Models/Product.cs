using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

        [StringLength(45)]
        public string Description { get; set; }

        public float Price { get; set; }

        [StringLength(45)]
        public string Color { get; set; }

        public byte [] Image { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }

        public ICollection<ProductInBranch> ProductInBranch { get; set; }

        public ICollection<ProductInOrder> ProductInOrders { get; set; }


    }
}
