namespace ShikShaq
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DbModel")
        {
        }

        public virtual DbSet<branch> branches { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<product_in_order> product_in_order { get; set; }
        public virtual DbSet<tag> tags { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<product_in_branch> product_in_branch { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<branch>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<branch>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<branch>()
                .HasMany(e => e.product_in_branch)
                .WithRequired(e => e.branch)
                .HasForeignKey(e => e.branch_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<branch>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.branch)
                .HasForeignKey(e => e.branch_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<order>()
                .HasMany(e => e.product_in_order)
                .WithRequired(e => e.order)
                .HasForeignKey(e => e.order_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.product_in_branch)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasOptional(e => e.product_in_order)
                .WithRequired(e => e.product);

            modelBuilder.Entity<product>()
                .HasMany(e => e.tags)
                .WithMany(e => e.products)
                .Map(m => m.ToTable("product_tag"));

            modelBuilder.Entity<tag>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.is_admin)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<ShikShaq.product_tag> product_tag { get; set; }
    }
}
