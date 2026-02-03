using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Data;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class InquiryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InquiryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inquiry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inquiry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inquiry inquiry)
        {
            if (ModelState.IsValid)
            {
                inquiry.InquiryDate = DateTime.Now;
                inquiry.Status = "Pending";

                _context.Inquiries.Add(inquiry);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Your inquiry has been submitted successfully. We will get back to you soon!";
                return RedirectToAction("Index", "Home");
            }

            return View(inquiry);
        }

        // GET: Inquiry/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
