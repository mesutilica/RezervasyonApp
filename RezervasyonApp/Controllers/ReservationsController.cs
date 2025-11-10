using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RezervasyonApp.Data;
using RezervasyonApp.Entities;

namespace RezervasyonApp.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly DatabaseContext _context;

        public ReservationsController(DatabaseContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", reservation.EmployeeId);
            return View(reservation);
        }
    }
}
