using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using EventManagementSystem.Models.ViewModels;

namespace EventManagementSystem.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index(int? categoryId, string city)
        {
            var query = _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Category)
                .Include(e => e.Reviews)
                .Where(e => e.Status == "Upcoming" && e.EventDate >= DateTime.Today);

            if (categoryId.HasValue)
            {
                query = query.Where(e => e.CategoryID == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(e => e.Venue.City.ToLower().Contains(city.ToLower()));
            }

            var events = await query
                .OrderBy(e => e.EventDate)
                .ToListAsync();

            ViewBag.Categories = await _context.Categories
                .Where(c => c.IsActive)
                .ToListAsync();
            ViewBag.SelectedCategory = categoryId;
            ViewBag.City = city;

            return View(events);
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Category)
                .Include(e => e.Reviews.Where(r => r.IsApproved))
                    .ThenInclude(r => r.Member)
                .FirstOrDefaultAsync(m => m.EventID == id);

            if (eventItem == null)
            {
                return NotFound();
            }

            // Calculate average rating
            if (eventItem.Reviews.Any())
            {
                ViewBag.AverageRating = eventItem.Reviews.Average(r => r.Rating);
            }

            return View(eventItem);
        }

        // GET: Event/Search
        public async Task<IActionResult> Search()
        {
            var model = new EventSearchViewModel
            {
                Categories = await _context.Categories.Where(c => c.IsActive).ToListAsync()
            };

            return View(model);
        }

        // POST: Event/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(EventSearchViewModel model)
        {
            var query = _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Category)
                .Where(e => e.Status == "Upcoming" && e.EventDate >= DateTime.Today);

            if (model.CategoryID.HasValue)
            {
                query = query.Where(e => e.CategoryID == model.CategoryID.Value);
            }

            // Use DateFrom and DateTo for date range filtering
            if (model.DateFrom.HasValue)
            {
                query = query.Where(e => e.EventDate >= model.DateFrom.Value);
            }

            if (model.DateTo.HasValue)
            {
                query = query.Where(e => e.EventDate <= model.DateTo.Value);
            }

            // Case-insensitive city search
            if (!string.IsNullOrEmpty(model.City))
            {
                query = query.Where(e => e.Venue.City.ToLower().Contains(model.City.ToLower()));
            }

            if (model.MinPrice.HasValue)
            {
                query = query.Where(e => e.TicketPrice >= model.MinPrice.Value);
            }

            if (model.MaxPrice.HasValue)
            {
                query = query.Where(e => e.TicketPrice <= model.MaxPrice.Value);
            }


            model.Results = await query
                .OrderBy(e => e.EventDate)
                .ToListAsync();

            model.Categories = await _context.Categories.Where(c => c.IsActive).ToListAsync();

            return View(model);
        }
    }
}
