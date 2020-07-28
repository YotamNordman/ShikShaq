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
    public class ProductInBranchesController : Controller
    {
        private readonly ShikShaqContext _context;

        public ProductInBranchesController(ShikShaqContext context)
        {
            _context = context;
        }

        // GET: ProductInBranches
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductInBranch.Include(o => o.Product).Include(o => o.Branch).ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(int quantity)
        {       
          var productsInBranchList = _context.ProductInBranch.Where(product => product.Quantity == quantity).ToListAsync();

          return View("Index", await productsInBranchList);
        }

        [Authorize(Roles = "Admin")]
        // GET: ProductInBranches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInBranch = await _context.ProductInBranch.Include(o => o.Product).Include(o => o.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInBranch == null)
            {
                return NotFound();
            }

            return View(productInBranch);
        }

        // GET: ProductInBranches/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_context.Product.ToList(), "Id", "Name");
            ViewBag.Branches = new SelectList(_context.Branch.ToList(), "Id", "Name");

            return View();
        }

        // POST: ProductInBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Quantity")] ProductInBranch productInBranch, int ProductId, int BranchId)
        {
            if (ModelState.IsValid)
            {
                productInBranch.Product = _context.Product.First(u => u.Id == ProductId);
                productInBranch.Branch = _context.Branch.First(u => u.Id == BranchId);

                _context.Add(productInBranch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productInBranch);
        }

        // GET: ProductInBranches/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Products = new SelectList(_context.Product.ToList(), "Id", "Name");
            ViewBag.Branches = new SelectList(_context.Branch.ToList(), "Id", "Name");

            if (id == null)
            {
                return NotFound();
            }

            var productInBranch = await _context.ProductInBranch.FindAsync(id);
            if (productInBranch == null)
            {
                return NotFound();
            }
            return View(productInBranch);
        }

        // POST: ProductInBranches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity")] ProductInBranch productInBranch, int ProductId, int BranchId)
        {
            if (id != productInBranch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productInBranch.Product = _context.Product.First(u => u.Id == ProductId);
                    productInBranch.Branch = _context.Branch.First(u => u.Id == BranchId);

                    _context.Update(productInBranch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInBranchExists(productInBranch.Id))
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
            return View(productInBranch);
        }

        // GET: ProductInBranches/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInBranch = await _context.ProductInBranch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInBranch == null)
            {
                return NotFound();
            }

            return View(productInBranch);
        }

        // POST: ProductInBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productInBranch = await _context.ProductInBranch.FindAsync(id);
            _context.ProductInBranch.Remove(productInBranch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInBranchExists(int id)
        {
            return _context.ProductInBranch.Any(e => e.Id == id);
        }
    }
}
