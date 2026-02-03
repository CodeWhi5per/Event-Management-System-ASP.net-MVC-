using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using EventManagementSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace EventManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Account/Register
        public async Task<IActionResult> Register()
        {
            var model = new RegisterViewModel
            {
                AvailableCategories = await _context.Categories
                    .Where(c => c.IsActive)
                    .ToListAsync()
            };
            return View(model);
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (await _context.Members.AnyAsync(m => m.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email address is already registered");
                    model.AvailableCategories = await _context.Categories.Where(c => c.IsActive).ToListAsync();
                    return View(model);
                }

                // Create new member
                var member = new Member
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    JoinDate = DateTime.Now,
                    Status = "Active"
                };

                _context.Members.Add(member);
                await _context.SaveChangesAsync();

                // Add preferences
                if (model.SelectedCategories != null && model.SelectedCategories.Any())
                {
                    foreach (var categoryId in model.SelectedCategories)
                    {
                        var preference = new Preference
                        {
                            MemberID = member.MemberID,
                            CategoryID = categoryId,
                            AddedDate = DateTime.Now
                        };
                        _context.Preferences.Add(preference);
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["Success"] = "Registration successful! Please login.";
                return RedirectToAction(nameof(Login));
            }

            model.AvailableCategories = await _context.Categories.Where(c => c.IsActive).ToListAsync();
            return View(model);
        }

        // GET: Account/Login
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var member = await _context.Members
                    .FirstOrDefaultAsync(m => m.Email == model.Email && m.Status == "Active");

                if (member != null && BCrypt.Net.BCrypt.Verify(model.Password, member.Password))
                {
                    // Set session
                    _httpContextAccessor.HttpContext?.Session.SetInt32("MemberID", member.MemberID);
                    _httpContextAccessor.HttpContext?.Session.SetString("MemberName", member.FullName);
                    _httpContextAccessor.HttpContext?.Session.SetString("MemberEmail", member.Email);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password");
            }

            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Profile
        public async Task<IActionResult> Profile()
        {
            var memberId = _httpContextAccessor.HttpContext?.Session.GetInt32("MemberID");
            if (memberId == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var member = await _context.Members
                .Include(m => m.Preferences)
                    .ThenInclude(p => p.Category)
                .Include(m => m.Bookings)
                    .ThenInclude(b => b.Event)
                .FirstOrDefaultAsync(m => m.MemberID == memberId);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }
    }
}
