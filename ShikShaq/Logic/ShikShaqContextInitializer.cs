using ShikShaq.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShikShaq.Logic
{
    public class ShikShaqContextInitializer
    {

        public void Initialize (ShikShaqContext context)
        {

            if (HasToInitialized (context))
            {

                Console.WriteLine("hasToInitialized");

            } else
            {
                Console.WriteLine("NOOO!!!!!!");
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

    }
}
