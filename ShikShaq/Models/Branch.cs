using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

        public DateTime? DateOpened { get; set; }

        [StringLength(45)]
        public string Address { get; set; }

        public float Lat { get; set; }

        public float Lng { get; set; }

        public ICollection<ProductInBranch> ProductInBranch { get; set; }

        public ICollection<Order> Orders { get; set; }


    }
}
