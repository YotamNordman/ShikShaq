using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace ShikShaq.Data
{
    public class ShikShaqContext : DbContext
    {
        public ShikShaqContext (DbContextOptions<ShikShaqContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.Branch> Branch { get; set; }

        public DbSet<WebApplication1.Models.Order> Order { get; set; }

        public DbSet<WebApplication1.Models.Product> Product { get; set; }

        public DbSet<WebApplication1.Models.ProductInBranch> ProductInBranch { get; set; }

        public DbSet<WebApplication1.Models.ProductInOrder> ProductInOrder { get; set; }

        public DbSet<WebApplication1.Models.ProductTag> ProductTag { get; set; }

        public DbSet<WebApplication1.Models.Tag> Tag { get; set; }

        public DbSet<WebApplication1.Models.User> User { get; set; }
    }
}
