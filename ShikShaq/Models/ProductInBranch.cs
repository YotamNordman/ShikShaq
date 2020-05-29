using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ProductInBranch
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public Branch Branch { get; set; }

        public int Quantity { get; set; }
    }
}
