using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RezervasyonApp.Data;
using RezervasyonApp.Entities;
using System.Security.Claims;

namespace RezervasyonApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (userId is null) // eğer sid değerine ulaşamazsak
            {
                HttpContext.SignOutAsync(); // oturumu kapat
                return RedirectToAction("Login", "Account"); // logine yönlendir
            }
            var model = _context.Users.Find(Convert.ToInt32(userId));
            if (model == null)
            {
                HttpContext.SignOutAsync(); // oturumu kapat
                return RedirectToAction("Login", "Account"); // logine yönlendir
            }
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> IndexAsync(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu!");
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password, string ReturnUrl)
        {
            var kullanici = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.IsActive);
            if (kullanici != null)
            {
                var haklar = new List<Claim>() // kullanıcı hakları tanımladık
                    {
                        new(ClaimTypes.Sid, kullanici.Id.ToString()),
                        new(ClaimTypes.Name, kullanici.Name + " " + kullanici.Surname),
                        new(ClaimTypes.Email, kullanici.Email), // claim = hak(kullanıcıya tanımlalan haklar)
                        new(ClaimTypes.Role, kullanici.IsAdmin ? "Admin" : "User"), // giriş yapan kullanıcı admin ise admin yetkisiyle değilse user yetkisiyle giriş yasın.
                        new(ClaimTypes.UserData, kullanici.UserGuid.Value.ToString())
                    };
                var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); // kullanıcı için bir kimlik oluşturduk
                ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi);
                HttpContext.SignInAsync(claimsPrincipal); // yukardaki yetkilerle sisteme giriş yaptık
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Message"] = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
  <strong>Giriş Başarısız!</strong> 
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div> ";
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(); // çıkış yap
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.IsActive = true;
                    user.IsAdmin = false;
                    user.UserType = UserType.Customer;
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Kayıt işlemi başarılı! Giriş yapabilirsiniz.</strong> 
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div> ";
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu!");
                }
            }
            return View(user);
        }
    }
}
