using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nancy.Json;
using ShikShaq.Data;
using WebApplication1.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Web.WebPages;
using Microsoft.AspNetCore.Authorization;
using Nancy.Bootstrapper;

namespace ShikShaq.Controllers
{
    
    public class ProductsController : Controller
    {
        private ShikShaqContext writecontext;
        private readonly ShikShaqContext _context;
        public const string SessionViewedTags = "tagsViewedByUser";
        public ProductsController(ShikShaqContext context)
        {
            _context = context;
            writecontext = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        public async Task<IActionResult> Search(string name, string description, string color, float price)
        {
            var productsList = _context.Product.Where(e => true);

            if(name != null)
            {
                productsList = productsList.Where(product => product.Name.ToLower().Contains(name.ToLower()));
            }

            if(description != null)
            {
                productsList = productsList.Where(product => product.Description.ToLower().Contains(description.ToLower()));
            }

            if(color != null)
            {
                productsList = productsList.Where(product => product.Color.ToLower().Contains(color.ToLower()));
            }

            if (price != 0)
            {
                productsList = productsList.Where(product => product.Price == price);

            }

            return View("Index", await productsList.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            addProductTagsToUserSession(product);

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Color")] Product product, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {

                if (Image != null && Image.Count > 0)
                {
                    var img = Image.First();
                    if (img.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await img.CopyToAsync(stream);
                            product.Image = stream.ToArray();
                        }
                    }
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Color")] Product product, List<IFormFile> Image)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null && Image.Count > 0)
                    {
                        var img = Image.First();
                            if (img.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                {
                                    await img.CopyToAsync(stream);
                                    product.Image = stream.ToArray();
                                }
                            }
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private void addProductTagsToUserSession(Product viewedProduct)
        {
            string stringOfTagsViewedByUser = HttpContext.Session.GetString(SessionViewedTags);
            List<Tag> tagsViewedByUser = getListOfViewedTags(stringOfTagsViewedByUser);

            tagsViewedByUser.AddRange(getListOfProductTags(viewedProduct));

            string updatedStringOfTagsViewedByUser = new JavaScriptSerializer().Serialize(tagsViewedByUser);

            HttpContext.Session.SetString(SessionViewedTags, updatedStringOfTagsViewedByUser);
        }

        private List<Tag> getListOfProductTags(Product viewedProduct)
        {
            if (viewedProduct.ProductTags != null)
            {
                return viewedProduct.ProductTags
                    .Select(productTag => {
                        productTag.Tag.ProductTags = null;
                        return productTag.Tag;
                    }).ToList();
            }

            return new List<Tag>();
        }

        private List<Tag> getListOfViewedTags(string stringOfTagsViewedByUser)
        {
            if (stringOfTagsViewedByUser != null)
            {
                return Newtonsoft.Json.JsonConvert
                    .DeserializeObject<List<Tag>>(stringOfTagsViewedByUser);
            }

            return new List<Tag>();
        }

        [HttpGet]
        public List<Product> RecommendedProducts()
        {
            string stringOfTagsViewedByUser = HttpContext.Session.GetString(SessionViewedTags);
            List<Tag> tagsViewedByUser = getListOfViewedTags(stringOfTagsViewedByUser);

            List<int> orderedTagIds = tagsViewedByUser
                .GroupBy(i => i.Id)
                .Select(group => new {
                    Metric = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(group => group.Count)
                .Select(group => group.Metric).ToList();

            return getRecommendedProductsByTag(getMostCommonTagOrDefault(orderedTagIds));
        }

        private Tag getMostCommonTagOrDefault(List<int> orderedTagIds)
        {
            return orderedTagIds.Count > 0 ? _context.Tag.Find(orderedTagIds[0]) : _context.Tag.First();
        }

        public List<Product> getRecommendedProductsByTag(Tag tag)
        {
            int numberOfRecommendedProducts = 3;

            var recommendedProducts = _context.Product
                .Where(p => (p.ProductTags.Any(t => t.Tag.Id == tag.Id)))
                .Take(numberOfRecommendedProducts).ToList();

            return recommendedProducts;
        }
        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            int userId = HttpContext.User.Claims.ToList()[0].Value.AsInt();
            int productId = id;
            int quantity = 0;
            var product = await _context.Product
            .FirstOrDefaultAsync(m => m.Id == productId);
            var user = await _context.User
            .FirstOrDefaultAsync(m => m.Id == userId);
            var existingcartitem = _context.CartItem.SingleOrDefault(p => p.ProductId == productId && p.UserId == userId);
            // If product is already in cart
            if (existingcartitem != null)
            {
                existingcartitem.Quantity++;
                await _context.SaveChangesAsync();
            }
            else
            {
                quantity = 1;
                var cartitem = new CartItem
                {
                    //Product = product,
                    //User = user,
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity
                };
                writecontext.CartItem.Add(cartitem);
                await writecontext.SaveChangesAsync();
            }


            return RedirectToAction("Details",product);
        }
    }
}
