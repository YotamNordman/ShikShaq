using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShikShaq.Data;
using WebApplication1.Models;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductInBranch.ToListAsync());
        }

        public async Task<IActionResult> Search(int quantity)
        {       
          var productsInBranchList = _context.ProductInBranch.Where(product => product.Quantity == quantity).ToListAsync();

          return View("Index", await productsInBranchList);
        }

        // GET: ProductInBranches/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: ProductInBranches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductInBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity")] ProductInBranch productInBranch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productInBranch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productInBranch);
        }

        // GET: ProductInBranches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity")] ProductInBranch productInBranch)
        {
            if (id != productInBranch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
