using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RezervasyonApp.Data;
using RezervasyonApp.Dtos;
using RezervasyonApp.Entities;
using System.Security.Claims;

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
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (userId is null) // eğer sid değerine ulaşamazsak
            {
                HttpContext.SignOutAsync(); // oturumu kapat
                return RedirectToAction("Login", "Account"); // logine yönlendir
            }
            var list = _context.Employees
                .Select(c => new EmployeeSelectDto // dto lar veritabanındaki veriler kullanılacağı yerdeki nesneye dönüştürmemizi sağlar.
                {
                    Id = c.Id,
                    Name = "Uz. Dr. " + c.Name + " " + c.Surname
                }).ToList();
            ViewData["EmployeeId"] = new SelectList(list, "Id", "Name");
            var model = new Reservation();
            model.User = _context.Users.Find(Convert.ToInt32(userId));
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now.AddHours(1);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(Reservation reservation)
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (userId is null) // eğer sid değerine ulaşamazsak
            {
                await HttpContext.SignOutAsync(); // oturumu kapat
                return RedirectToAction("Login", "Account"); // logine yönlendir
            }
            reservation.UserId = Convert.ToInt32(userId);
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.AddAsync(reservation);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Rezervasyon işlemi başarılı! </strong> 
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div> ";
                    return RedirectToAction("Index", "Account");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu!");
                }
            }
            var list = _context.Employees
                .Select(c => new EmployeeSelectDto
                {
                    Id = c.Id,
                    Name = "Uz. Dr. " + c.Name + " " + c.Surname
                }).ToList();
            ViewData["EmployeeId"] = new SelectList(list, "Id", "Name");
            return View(reservation);
        }
    }
}
