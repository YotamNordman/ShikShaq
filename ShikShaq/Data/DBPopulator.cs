using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace ShikShaq.Data
{
    public static class DBPopulator
    {

        public static void Populate(ShikShaqContext context)
        {
            //If You dont have a DB in your environment, init one without need for user involvement.
            context.Database.EnsureCreated();
            //Check if theres any Data
            if (!(context.Product.Any()))
            {
                context.User.AddRange(
                new List<User>() { 
                    new User {
                        Name = "Yotam",
                        Email = "Yotam@nordman.co.il",
                        Password = "Admin",
                        Birthday = new DateTime(1997,9,17),
                        Address = "Address 1",
                        Height = 186,
                        Weight = 88,
                        IsAdmin = "Y"
                    },
                    new User {
                        Name = "Customer1",
                        Email = "Customer1@nordman.co.il",
                        Password = "Customer1",
                        Birthday = new DateTime(1992,9,17),
                        Address = "Address 2",
                        Height = 66,
                        Weight = 188,
                        IsAdmin = "N"
                    },
                    new User {
                        Name = "Customer2",
                        Email = "Customer2@nordman.co.il",
                        Password = "Customer2",
                        Birthday = new DateTime(1992,6,12),
                        Address = "Address 3",
                        Height = 166,
                        Weight = 50,
                        IsAdmin = "N"
                    },
                    new User {
                        Name = "admin",
                        Email = "admin@admin.co.il",
                        Password = "admin",
                        Birthday = new DateTime(2000,1,1),
                        Address = "admin",
                        Height = 100,
                        Weight = 100,
                        IsAdmin = "Y"
                    },
                }
                );
                context.Tag.AddRange(
                new List<Tag>()
                {
                    new Tag{
                    Name = "Basketball"
                    },
                    new Tag{
                    Name = "Football"
                    },
                    new Tag{
                    Name = "Hockey"
                    },
                    new Tag{
                    Name = "Running"
                    },
                }
                );
                context.Product.AddRange(
                new List<Product>()
                {
                    new Product{ 
                    Name = "Basketball ball",
                    Description = "Ball for basketball",
                    Price = 9,
                    Color = "Orange"
                    },
                    new Product{
                    Name = "Football ball",
                    Description = "Ball for basketball",
                    Price = 19,
                    Color = "Yellow"
                    },
                    new Product{
                    Name = "Hockey puck",
                    Description = "Size 2",
                    Price = 29,
                    Color = "Black"
                    },
                    new Product{
                    Name = "Running shoes",
                    Description = "Nikey",
                    Price = 99,
                    Color = "Black"
                    },
                }
                );
                context.Branch.AddRange(
                new List<Branch>()
                {
                    new Branch{
                    Name = "TLV",
                    Address = "Port TLV",
                    DateOpened = new DateTime(2000,1,1),
                    Lat = 122,
                    Lng = 122,
                    },
                    new Branch{
                    Name = "Petah tikba",
                    Address = "Aba Hilel 23",
                    DateOpened = new DateTime(2012,12,12),
                    Lat = 1222,
                    Lng = 122
                    },
                    new Branch{
                    Name = "Eilat",
                    Address = "Hof almog",
                    DateOpened = new DateTime(2010,2,12),
                    Lat = 1232,
                    Lng = 12522,
                    }
                }
                );
                context.Order.AddRange(
                new List<Order>() {
                    new Order{
                    OrderDate = new DateTime(2020,2,29),
                    FinalPrice = 160
                    }
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
