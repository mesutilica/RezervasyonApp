using Microsoft.AspNetCore.Mvc;
using RezervasyonApp.Data;

namespace RezervasyonApp.ViewComponents
{
    public class Services : ViewComponent
    {
        private readonly DatabaseContext _context;

        public Services(DatabaseContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Services.Where(c => c.IsActive && c.IsTopMenu));
        }
    }
}
