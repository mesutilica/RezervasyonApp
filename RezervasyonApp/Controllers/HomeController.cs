using Microsoft.AspNetCore.Mvc;
using RezervasyonApp.Data;
using RezervasyonApp.Models;
using System.Diagnostics;
using System.Linq;

namespace RezervasyonApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Sliders = _context.Sliders.ToList(),
                Services = _context.Services.Where(x => x.IsHome && x.IsActive).ToList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
