using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }

    }
}
