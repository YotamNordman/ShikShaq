﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShikShaq.Data;

namespace ShikShaq.Migrations
{
    [DbContext(typeof(ShikShaqContext))]
    [Migration("20200606103725_ChangePriceToFloat")]
    partial class ChangePriceToFloat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication1.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("DateOpened");

                    b.Property<float>("Lat");

                    b.Property<float>("Lng");

                    b.Property<string>("Name")
                        .HasMaxLength(45);

                    b.HasKey("Id");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("WebApplication1.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("WebApplication1.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BranchId");

                    b.Property<float>("FinalPrice");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("WebApplication1.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasMaxLength(45);

                    b.Property<string>("Description")
                        .HasMaxLength(45);

                    b.Property<string>("Name")
                        .HasMaxLength(45);

                    b.Property<float>("Price");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("WebApplication1.Models.ProductInBranch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BranchId");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInBranch");
                });

            modelBuilder.Entity("WebApplication1.Models.ProductInOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInOrder");
                });

            modelBuilder.Entity("WebApplication1.Models.ProductTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductId");

                    b.Property<int?>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTag");
                });

            modelBuilder.Entity("WebApplication1.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(45);

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("WebApplication1.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<float>("Height");

                    b.Property<string>("IsAdmin")
                        .HasMaxLength(1);

                    b.Property<string>("Name")
                        .HasMaxLength(45);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebApplication1.Models.CartItem", b =>
                {
                    b.HasOne("WebApplication1.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("WebApplication1.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApplication1.Models.Order", b =>
                {
                    b.HasOne("WebApplication1.Models.Branch", "Branch")
                        .WithMany("Orders")
                        .HasForeignKey("BranchId");

                    b.HasOne("WebApplication1.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApplication1.Models.ProductInBranch", b =>
                {
                    b.HasOne("WebApplication1.Models.Branch", "Branch")
                        .WithMany("ProductInBranch")
                        .HasForeignKey("BranchId");

                    b.HasOne("WebApplication1.Models.Product", "Product")
                        .WithMany("ProductInBranch")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("WebApplication1.Models.ProductInOrder", b =>
                {
                    b.HasOne("WebApplication1.Models.Order", "Order")
                        .WithMany("ProductInOrders")
                        .HasForeignKey("OrderId");

                    b.HasOne("WebApplication1.Models.Product", "Product")
                        .WithMany("ProductInOrders")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("WebApplication1.Models.ProductTag", b =>
                {
                    b.HasOne("WebApplication1.Models.Product", "Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ProductId");

                    b.HasOne("WebApplication1.Models.Tag", "Tag")
                        .WithMany("ProductTags")
                        .HasForeignKey("TagId");
                });
#pragma warning restore 612, 618
        }
    }
}
