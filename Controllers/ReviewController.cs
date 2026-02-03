using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Data;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int? GetLoggedInMemberId()
        {
            return _httpContextAccessor.HttpContext?.Session.GetInt32("MemberID");
        }

        // GET: Review/Create/5
        public async Task<IActionResult> Create(int? id)
        {
            var memberId = GetLoggedInMemberId();
            if (memberId == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Create", new { id }) });
            }

            if (id == null)
            {
                return NotFound();
            }

            var eventItem = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (eventItem == null)
            {
                return NotFound();
            }

            // Check if user has booked this event
            var hasBooking = await _context.Bookings
                .AnyAsync(b => b.MemberID == memberId && b.EventID == id && b.Status == "Confirmed");

            if (!hasBooking)
            {
                TempData["Error"] = "You can only review events you have attended.";
                return RedirectToAction("Details", "Event", new { id });
            }

            // Check if user has already reviewed
            var existingReview = await _context.Reviews
                .AnyAsync(r => r.MemberID == memberId && r.EventID == id);

            if (existingReview)
            {
                TempData["Error"] = "You have already reviewed this event.";
                return RedirectToAction("Details", "Event", new { id });
            }

            ViewBag.Event = eventItem;

            var review = new Review { EventID = id.Value };
            return View(review);
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            var memberId = GetLoggedInMemberId();
            if (memberId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                review.MemberID = memberId.Value;
                review.ReviewDate = DateTime.Now;
                review.IsApproved = false; // Reviews need approval

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thank you for your review! It will be published after approval.";
                return RedirectToAction("Details", "Event", new { id = review.EventID });
            }

            var eventItem = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(e => e.EventID == review.EventID);

            ViewBag.Event = eventItem;
            return View(review);
        }

        // GET: Review/Index
        public async Task<IActionResult> Index(int? eventId)
        {
            var query = _context.Reviews
                .Include(r => r.Event)
                .Include(r => r.Member)
                .Where(r => r.IsApproved);

            if (eventId.HasValue)
            {
                query = query.Where(r => r.EventID == eventId.Value);
            }

            var reviews = await query
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();

            ViewBag.Events = await _context.Events
                .Where(e => e.Status == "Completed")
                .OrderBy(e => e.EventName)
                .ToListAsync();

            return View(reviews);
        }
    }
}
