using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Magda.Data;
using Magda.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static Magda.Models.User;
using Microsoft.AspNetCore.Authorization;

namespace Magda.Controllers
{
    [Authorize]
    public class GuestLists : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public GuestLists(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            Console.WriteLine(userId);
            //Console.WriteLine(HttpContext.User);
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> UnAuth()
        {
            return View(UnAuth);
        }

        // GET: GuestLists
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.userRoleType == Role.Worker)
            {
                return _context.GuestList != null ? 
                          View(await _context.GuestList.Include(c => c.Guests).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GuestList'  is null.");
            }
            return RedirectToAction(nameof(UnAuth));
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
                return RedirectToAction(nameof(Edit), new { id = guestList.ListId});
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
            await _context.Entry(guestList).Collection(c => c.Guests).LoadAsync();
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
