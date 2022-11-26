using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Magda.Data;
using Magda.Models;

namespace Magda.Controllers
{
    public class GuestLists : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuestLists(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GuestLists
        public async Task<IActionResult> Index()
        {
              return _context.GuestList != null ? 
                          View(await _context.GuestList.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GuestList'  is null.");
        }

        // GET: GuestLists/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.GuestList == null)
            {
                return NotFound();
            }

            var guestList = await _context.GuestList
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (guestList == null)
            {
                return NotFound();
            }

            return View(guestList);
        }

        // GET: GuestLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuestLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId")] GuestList guestList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guestList);
        }

        // GET: GuestLists/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.GuestList == null)
            {
                return NotFound();
            }

            var guestList = await _context.GuestList.FindAsync(id);
            if (guestList == null)
            {
                return NotFound();
            }
            return View(guestList);
        }

        // POST: GuestLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ListId")] GuestList guestList)
        {
            if (id != guestList.ListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestListExists(guestList.ListId))
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
            return View(guestList);
        }

        // GET: GuestLists/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.GuestList == null)
            {
                return NotFound();
            }

            var guestList = await _context.GuestList
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (guestList == null)
            {
                return NotFound();
            }

            return View(guestList);
        }

        // POST: GuestLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.GuestList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GuestList'  is null.");
            }
            var guestList = await _context.GuestList.FindAsync(id);
            if (guestList != null)
            {
                _context.GuestList.Remove(guestList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestListExists(string id)
        {
          return (_context.GuestList?.Any(e => e.ListId == id)).GetValueOrDefault();
        }
    }
}
