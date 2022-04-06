using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airport.Data;
using Airport.Models;
using Microsoft.AspNetCore.Authorization;
using Airport.Areas.Identity.Data;

namespace Airport.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tickets.Where(t => t.User.Email == User.Identity.Name).Include(t => t.Schedule).Include(t => t.User);
            if (User.IsInRole("Admin"))
            {
                applicationDbContext = _context.tickets.Include(t => t.Schedule).Include(t => t.User);
            }
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets
                .Include(t => t.Schedule)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ticketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["flightId"] = new SelectList(_context.schedules, "flightId", "flightId");
            ViewData["UserId"] = new SelectList(_context.Users, "Email", "Email");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ticketId,UserId,flightId,ticketCount,dateOfJourney")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(user => user.Email == ticket.UserId);
                ticket.UserId = user.First().Id;
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["flightId"] = new SelectList(_context.schedules, "flightId", "flightId", ticket.flightId);
            ViewData["UserId"] = new SelectList(_context.Users, "Email", "Email", ticket.UserId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["flightId"] = new SelectList(_context.schedules, "flightId", "flightId", ticket.flightId);
            ViewData["UserId"] = new SelectList(_context.Users, "Email", "Email", ticket.UserId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ticketId,UserId,flightId,ticketCount,dateOfJourney")] Ticket ticket)
        {
            if (id != ticket.ticketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.Users.Where(user => user.Email == ticket.UserId);
                    ticket.UserId = user.First().Id;
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.ticketId))
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
            ViewData["flightId"] = new SelectList(_context.schedules, "flightId", "flightId", ticket.flightId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticket.UserId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets
                .Include(t => t.Schedule)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ticketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.tickets.FindAsync(id);
            if (User.IsInRole("Admin") || ticket.User.Email == User.Identity.Name)
            {
                _context.tickets.Remove(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return RedirectToPage("Identity/Account/AccessDenied");
            }

            
        }

        private bool TicketExists(int id)
        {
            return _context.tickets.Any(e => e.ticketId == id);
        }
    }
}
