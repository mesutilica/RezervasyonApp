using Microsoft.AspNetCore.Mvc;

namespace RezervasyonApp.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
