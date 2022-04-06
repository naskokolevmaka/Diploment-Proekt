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

namespace Airport.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schedules
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.schedules.Include(s => s.Airline).Include(s => s.Destination);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.schedules
                .Include(s => s.Airline)
                .Include(s => s.Destination)
                .FirstOrDefaultAsync(m => m.flightId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            ViewData["airlineId"] = new SelectList(_context.airlines, "airlineId", "airlineName");
            ViewData["destinationId"] = new SelectList(_context.destinations, "destinationId", "city");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("flightId,airlineId,destinationId,price,scheduleDate,departureTime")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["airlineId"] = new SelectList(_context.airlines, "airlineId", "airlineName", schedule.airlineId);
            ViewData["destinationId"] = new SelectList(_context.destinations, "destinationId", "city", schedule.destinationId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["airlineId"] = new SelectList(_context.airlines, "airlineId", "airlineName", schedule.airlineId);
            ViewData["destinationId"] = new SelectList(_context.destinations, "destinationId", "city", schedule.destinationId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("flightId,airlineId,destinationId,price,scheduleDate,departureTime")] Schedule schedule)
        {
            if (id != schedule.flightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.flightId))
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
            ViewData["airlineId"] = new SelectList(_context.airlines, "airlineId", "airlineName", schedule.airlineId);
            ViewData["destinationId"] = new SelectList(_context.destinations, "destinationId", "city", schedule.destinationId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.schedules
                .Include(s => s.Airline)
                .Include(s => s.Destination)
                .FirstOrDefaultAsync(m => m.flightId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.schedules.FindAsync(id);
            _context.schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.schedules.Any(e => e.flightId == id);
        }
    }
}
