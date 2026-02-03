using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get upcoming featured events
            var upcomingEvents = await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Category)
                .Where(e => e.Status == "Upcoming" && e.EventDate >= DateTime.Today)
                .OrderBy(e => e.EventDate)
                .Take(6)
                .ToListAsync();

            ViewBag.UpcomingEvents = upcomingEvents;

            // Get statistics
            ViewBag.TotalEvents = await _context.Events.CountAsync();
            ViewBag.TotalMembers = await _context.Members.Where(m => m.Status == "Active").CountAsync();
            ViewBag.TotalVenues = await _context.Venues.CountAsync();

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
