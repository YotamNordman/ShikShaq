using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShikShaq.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;

namespace ShikShaq.Controllers
{
    public class ProductTagsController : Controller
    {
        private readonly ShikShaqContext _context;

        public ProductTagsController(ShikShaqContext context)
        {
            _context = context;
        }

        // GET: ProductTags
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductTag.Include(o => o.Product).Include(o => o.Tag).ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string name)
        {
            var productTagsList = _context.ProductTag.ToListAsync();

            if (name != null)
            {
                productTagsList = _context.ProductTag.Where(tag => tag.Tag.Name.ToLower().Contains(name.ToLower())).ToListAsync();

            }

            return View("Index", await productTagsList);
        }

        // GET: ProductTags/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTag.Include(o => o.Product).Include(o => o.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productTag == null)
            {
                return NotFound();
            }

            return View(productTag);
        }

        // GET: ProductTags/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_context.Product.ToList(), "Id", "Name");
            ViewBag.Tags = new SelectList(_context.Tag.ToList(), "Id", "Name");

            return View();
        }

        // POST: ProductTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id")] ProductTag productTag, int ProductId, int TagId)
        {
            if (ModelState.IsValid)
            {
                productTag.Product = _context.Product.First(u => u.Id == ProductId);
                productTag.Tag = _context.Tag.First(u => u.Id == TagId);

                _context.Add(productTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTag);
        }

        // GET: ProductTags/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Products = new SelectList(_context.Product.ToList(), "Id", "Name");
            ViewBag.Tags = new SelectList(_context.Tag.ToList(), "Id", "Name");

            if (id == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTag.FindAsync(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        // POST: ProductTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] ProductTag productTag, int ProductId, int TagId)
        {
            if (id != productTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productTag.Product = _context.Product.First(u => u.Id == ProductId);
                    productTag.Tag = _context.Tag.First(u => u.Id == TagId);

                    _context.Update(productTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTagExists(productTag.Id))
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
            return View(productTag);
        }

        // GET: ProductTags/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productTag == null)
            {
                return NotFound();
            }

            return View(productTag);
        }

        // POST: ProductTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productTag = await _context.ProductTag.FindAsync(id);
            _context.ProductTag.Remove(productTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTagExists(int id)
        {
            return _context.ProductTag.Any(e => e.Id == id);
        }
    }
}
