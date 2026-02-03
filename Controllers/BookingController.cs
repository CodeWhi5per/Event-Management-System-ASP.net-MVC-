using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Data;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Helper method to check if user is logged in
        private int? GetLoggedInMemberId()
        {
            return _httpContextAccessor.HttpContext?.Session.GetInt32("MemberID");
        }

        // GET: Booking/Create/5
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
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (eventItem == null || eventItem.Status != "Upcoming" || eventItem.AvailableSeats == 0)
            {
                TempData["Error"] = "This event is not available for booking.";
                return RedirectToAction("Index", "Event");
            }

            ViewBag.Event = eventItem;
            ViewBag.MaxQuantity = Math.Min(eventItem.AvailableSeats, 10);

            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int eventId, int quantity, string seatType = "Standard")
        {
            var memberId = GetLoggedInMemberId();
            if (memberId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var eventItem = await _context.Events.FindAsync(eventId);
            if (eventItem == null || quantity > eventItem.AvailableSeats || quantity <= 0)
            {
                TempData["Error"] = "Invalid booking request.";
                return RedirectToAction("Create", new { id = eventId });
            }

            // Calculate pricing based on seat type
            decimal unitPrice = eventItem.TicketPrice;
            if (seatType == "Premium")
                unitPrice *= 1.5m;
            else if (seatType == "VIP")
                unitPrice *= 2.0m;

            decimal totalAmount = unitPrice * quantity;

            // Create booking
            var booking = new Booking
            {
                MemberID = memberId.Value,
                EventID = eventId,
                BookingDate = DateTime.Now,
                TotalTickets = quantity,
                TotalAmount = totalAmount,
                Status = "Confirmed",
                BookingReference = GenerateBookingReference()
            };

            _context.Bookings.Add(booking);

            // Create booking detail
            var detail = new BookingDetail
            {
                Booking = booking,
                SeatType = seatType,
                Quantity = quantity,
                UnitPrice = unitPrice,
                Subtotal = totalAmount
            };

            _context.BookingDetails.Add(detail);

            // Update available seats
            eventItem.AvailableSeats -= quantity;

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "Booking confirmed successfully!";
                return RedirectToAction(nameof(Confirmation), new { id = booking.BookingID });
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while processing your booking.";
                return RedirectToAction("Create", new { id = eventId });
            }
        }

        // GET: Booking/Confirmation/5
        public async Task<IActionResult> Confirmation(int? id)
        {
            var memberId = GetLoggedInMemberId();
            if (memberId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                    .ThenInclude(e => e.Venue)
                .Include(b => b.Event)
                    .ThenInclude(e => e.Category)
                .Include(b => b.BookingDetails)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(b => b.BookingID == id && b.MemberID == memberId);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Booking/History
        public async Task<IActionResult> History()
        {
            var memberId = GetLoggedInMemberId();
            if (memberId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bookings = await _context.Bookings
                .Include(b => b.Event)
                    .ThenInclude(e => e.Venue)
                .Include(b => b.Event)
                    .ThenInclude(e => e.Category)
                .Where(b => b.MemberID == memberId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();

            return View(bookings);
        }

        // Helper method to generate booking reference
        private string GenerateBookingReference()
        {
            return $"BK-{DateTime.Now:yyyy}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }
}
