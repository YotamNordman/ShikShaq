using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ProductTag
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public Tag Tag { get; set; }

    }
}
