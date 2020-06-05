using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }

        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public Branch Branch { get; set; }

        public float FinalPrice { get; set; }

        public ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}
