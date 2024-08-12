using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMaster.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebMaster.Controllers
{
    // [Authorize(Roles = "Admin,Seller")]
    public class SalesProductsController : Controller
    {
        private readonly AppDbContext _context;

        public SalesProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SalesProducts
        // [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SalesProducts.Include(s => s.Products).Include(s => s.Sales);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SalesProducts/Details/5
        // [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesProduct = await _context.SalesProducts
                .Include(s => s.Products)
                .Include(s => s.Sales)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesProduct == null)
            {
                return NotFound();
            }

            return View(salesProduct);
        }

        // GET: SalesProducts/Create
        // [Authorize(Roles = "Admin,Seller")]
        public IActionResult Create()
        {
            ViewData["ProductsId"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["SalesId"] = new SelectList(_context.Sales, "Id", "Id");
            return View();
        }

        // POST: SalesProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [Authorize(Roles = "Admin,Seller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SalesId,ProductsId")] SalesProduct salesProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductsId"] = new SelectList(_context.Products, "Id", "Id", salesProduct.ProductsId);
            ViewData["SalesId"] = new SelectList(_context.Sales, "Id", "Id", salesProduct.SalesId);
            return View(salesProduct);
        }

        // GET: SalesProducts/Edit/5
        // [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesProduct = await _context.SalesProducts.FindAsync(id);
            if (salesProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductsId"] = new SelectList(_context.Products, "Id", "Id", salesProduct.ProductsId);
            ViewData["SalesId"] = new SelectList(_context.Sales, "Id", "Id", salesProduct.SalesId);
            return View(salesProduct);
        }

        // POST: SalesProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [Authorize(Roles = "Admin,Seller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SalesId,ProductsId")] SalesProduct salesProduct)
        {
            if (id != salesProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesProductExists(salesProduct.Id))
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
            ViewData["ProductsId"] = new SelectList(_context.Products, "Id", "Id", salesProduct.ProductsId);
            ViewData["SalesId"] = new SelectList(_context.Sales, "Id", "Id", salesProduct.SalesId);
            return View(salesProduct);
        }

        // GET: SalesProducts/Delete/5
        // [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesProduct = await _context.SalesProducts
                .Include(s => s.Products)
                .Include(s => s.Sales)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesProduct == null)
            {
                return NotFound();
            }

            return View(salesProduct);
        }

        // POST: SalesProducts/Delete/5
        // [Authorize(Roles = "Admin,Seller")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesProduct = await _context.SalesProducts.FindAsync(id);
            if (salesProduct != null)
            {
                _context.SalesProducts.Remove(salesProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesProductExists(int id)
        {
            return _context.SalesProducts.Any(e => e.Id == id);
        }
    }
}
