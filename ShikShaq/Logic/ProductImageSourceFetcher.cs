using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace ShikShaq.Logic
{
    public static class ProductImageSourceFetcher
    {

        public static String Fetch(Product product)
        {
            string imgsrc = "/images/no-image.png";
            if (product != null && product.Image != null)
            {
                var imgBase64 = Convert.ToBase64String(product.Image);
                imgsrc = string.Format("data:image/gif;base64,{0}", imgBase64);
            }

            return imgsrc;
        }

    }
}
