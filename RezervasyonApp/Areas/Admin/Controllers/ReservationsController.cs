using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RezervasyonApp.Data;
using RezervasyonApp.Entities;

namespace RezervasyonApp.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ReservationsController : Controller
    {
        private readonly DatabaseContext _context;

        public ReservationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Reservations
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Reservations.Include(r => r.Customer).Include(r => r.Employee).Include(r => r.User);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Employee)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Admin/Reservations/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Admin/Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", reservation.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", reservation.EmployeeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", reservation.UserId);
            return View(reservation);
        }

        // GET: Admin/Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", reservation.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", reservation.EmployeeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", reservation.UserId);
            return View(reservation);
        }

        // POST: Admin/Reservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", reservation.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", reservation.EmployeeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", reservation.UserId);
            return View(reservation);
        }

        // GET: Admin/Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Employee)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Admin/Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
