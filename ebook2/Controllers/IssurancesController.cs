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
    public class IssurancesController : Controller
    {
        private readonly ebookdatabaseContext _context;

        public IssurancesController(ebookdatabaseContext context)
        {
            _context = context;
        }

        // GET: Issurances
        public async Task<IActionResult> Index()
        {
            var ebookdatabaseContext = _context.Issurance.Include(i => i.Redemption).Include(i => i.Student);
            return View(await ebookdatabaseContext.ToListAsync());
        }

        // GET: Issurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issurance = await _context.Issurance
                .Include(i => i.Redemption)
                .Include(i => i.Student)
                .FirstOrDefaultAsync(m => m.IssuranceId == id);
            if (issurance == null)
            {
                return NotFound();
            }

            return View(issurance);
        }

        // GET: Issurances/Create
        public IActionResult Create()
        {
            ViewData["RedemptionId"] = new SelectList(_context.Redemption, "RedemptionId", "BookTitle");
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "LastName");
            return View();
        }

        // POST: Issurances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssuranceId,StudentId,RedemptionId")] Issurance issurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RedemptionId"] = new SelectList(_context.Redemption, "RedemptionId", "RedemptionId", issurance.RedemptionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", issurance.StudentId);
            return View(issurance);
        }

        // GET: Issurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issurance = await _context.Issurance.FindAsync(id);
            if (issurance == null)
            {
                return NotFound();
            }
            ViewData["RedemptionId"] = new SelectList(_context.Redemption, "RedemptionId", "BookTitle", issurance.RedemptionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "LastName", issurance.StudentId);
            return View(issurance);
        }

        // POST: Issurances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssuranceId,StudentId,RedemptionId")] Issurance issurance)
        {
            if (id != issurance.IssuranceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssuranceExists(issurance.IssuranceId))
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
            ViewData["RedemptionId"] = new SelectList(_context.Redemption, "RedemptionId", "BookTitle", issurance.RedemptionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "LastName", issurance.StudentId);
            return View(issurance);
        }

        // GET: Issurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issurance = await _context.Issurance
                .Include(i => i.Redemption)
                .Include(i => i.Student)
                .FirstOrDefaultAsync(m => m.IssuranceId == id);
            if (issurance == null)
            {
                return NotFound();
            }

            return View(issurance);
        }

        // POST: Issurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issurance = await _context.Issurance.FindAsync(id);
            _context.Issurance.Remove(issurance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssuranceExists(int id)
        {
            return _context.Issurance.Any(e => e.IssuranceId == id);
        }
    }
}
