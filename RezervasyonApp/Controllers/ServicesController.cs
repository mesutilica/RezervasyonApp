using Microsoft.AspNetCore.Mvc;
using RezervasyonApp.Data;

namespace RezervasyonApp.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DatabaseContext _context;

        public ServicesController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Services);
        }
        public IActionResult Details(int? id)
        {
            return View(_context.Services.Find(id));
        }
    }
}
