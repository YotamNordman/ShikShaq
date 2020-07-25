using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ShikShaq.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace ShikShaq.Controllers
{
    [Route("api/statisticsapi")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StatisticsAPIController : ControllerBase
    {
        protected ShikShaqContext context;
        public StatisticsAPIController(ShikShaqContext context)
        {
            this.context = context;
        }
        [HttpGet("productorders")]
        public async Task<ActionResult<Dictionary<string, int>>> ProductsOrders()
        {
            //A Product and its count sorted by name and count
            return context.ProductInOrder.Include(productsinorder => productsinorder.Product).
                GroupBy(productsinorder => productsinorder.Product).
                Select(o => new { name = o.Key.Name, count = o.Sum(productsinorder => productsinorder.Quantity) }).
                OrderBy(a => a.count).
                ToDictionary(a => a.name, a => a.count);
        }
        [HttpGet("populartags")]
        public async Task<ActionResult<Dictionary<string, int>>> PopularTags()
        {
            return context.ProductInOrder.Join(context.ProductTag,
                productfromorder => productfromorder.Product.Id,
                productfromtag => productfromtag.Product.Id,
                (productfromorder, productfromtag) => new
                {
                    product = productfromorder.Product,
                    productquantity = productfromorder.Quantity,
                    tag = productfromtag.Tag,
                }).
                GroupBy(t => t.tag).
                Select(o => new { name = o.Key.Name, count = o.Sum(productquantity => productquantity.productquantity) }).
                Distinct().
                ToDictionary(a => a.name, a => a.count);

        }
    }
}
