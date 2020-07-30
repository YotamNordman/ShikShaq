using ShikShaq.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace ShikShaq.Logic
{
    public class ShikShaqContextInitializer
    {

        public void Initialize (ShikShaqContext context)
        {

            //If You dont have a DB in your environment, init one without need for user involvement.
            context.Database.EnsureCreated();

            if (HasToInitialized(context))
            {

                InitializeUsers(context);
                context.SaveChanges();
            }
        }

        private bool HasToInitialized(ShikShaqContext context)
        {
            bool hasToInitialized = true;
            hasToInitialized = hasToInitialized && !(context.User.Count() > 0);
            hasToInitialized = hasToInitialized && !(context.Tag.Count() > 0);
            hasToInitialized = hasToInitialized && !(context.Order.Count() > 0);
            hasToInitialized = hasToInitialized && !(context.Product.Count() > 0);
            hasToInitialized = hasToInitialized && !(context.Branch.Count() > 0);
            hasToInitialized = hasToInitialized && !(context.ProductInBranch.Count() > 0);
            hasToInitialized = hasToInitialized && !(context.ProductInOrder.Count() > 0);
            hasToInitialized = hasToInitialized && !(context.ProductTag.Count() > 0);

            return hasToInitialized;
        }

        private void InitializeUsers(ShikShaqContext context)
        {
            User userAdmin = new User { Name = "Admin", Address = "Maze Pinat Mapo 40", Email = "Admin@gmail.com", Password = "1234", IsAdmin = "Y", Height = 167, Weight = 83, Birthday = new DateTime(2000,1,1) };
            User userRegular = new User { Name = "Regular", Address = "Hell Street 666", Email = "Regular@gmail.com", Password = "1234", IsAdmin = "N", Height = 150, Weight = 100, Birthday = new DateTime(1980, 5, 12) };
            User userShaked = new User { Name = "Shaked", Address = "Sderot Ben Gurion 40", Email = "shvshv44@gmail.com", Password = "209081900", IsAdmin = "N", Height = 163, Weight = 80, Birthday = new DateTime(1997, 11, 6) };

            context.User.Add(userAdmin);
            context.User.Add(userRegular);
            context.User.Add(userShaked);
        }

    }
}
