using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ebook2.Models;

namespace ebook2.Controllers
{
    public class RedemptionsController : Controller
    {
        private readonly ebookdatabaseContext _context;

        public RedemptionsController(ebookdatabaseContext context)
        {
            _context = context;
        }

        // GET: Redemptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Redemption.ToListAsync());
        }

        // GET: Redemptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var redemption = await _context.Redemption
                .FirstOrDefaultAsync(m => m.RedemptionId == id);
            if (redemption == null)
            {
                return NotFound();
            }

            return View(redemption);
        }

        // GET: Redemptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Redemptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RedemptionId,Code,BookTitle,Isbn")] Redemption redemption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(redemption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(redemption);
        }

        // GET: Redemptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var redemption = await _context.Redemption.FindAsync(id);
            if (redemption == null)
            {
                return NotFound();
            }
            return View(redemption);
        }

        // POST: Redemptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RedemptionId,Code,BookTitle,Isbn")] Redemption redemption)
        {
            if (id != redemption.RedemptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(redemption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RedemptionExists(redemption.RedemptionId))
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
            return View(redemption);
        }

        // GET: Redemptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var redemption = await _context.Redemption
                .FirstOrDefaultAsync(m => m.RedemptionId == id);
            if (redemption == null)
            {
                return NotFound();
            }

            return View(redemption);
        }

        // POST: Redemptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var redemption = await _context.Redemption.FindAsync(id);
            _context.Redemption.Remove(redemption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RedemptionExists(int id)
        {
            return _context.Redemption.Any(e => e.RedemptionId == id);
        }
    }
}
