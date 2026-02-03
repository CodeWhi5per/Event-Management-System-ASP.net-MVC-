
using EventManagementSystem.Models;

namespace EventManagementSystem.Data
{
    public static class DbSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Check if data already exists
            if (context.Members.Any() || context.Events.Any())
            {
                return; // Database has been seeded
            }

            // Seed Categories
            var categories = new List<Category>
            {
                new Category { CategoryName = "Concert", Description = "Live music performances", IsActive = true },
                new Category { CategoryName = "Theater", Description = "Theatrical performances and plays", IsActive = true },
                new Category { CategoryName = "Exhibition", Description = "Art and cultural exhibitions", IsActive = true },
                new Category { CategoryName = "Workshop", Description = "Educational and skill-building workshops", IsActive = true },
                new Category { CategoryName = "Festival", Description = "Cultural festivals and celebrations", IsActive = true }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Seed Venues
            var venues = new List<Venue>
            {
                new Venue 
                { 
                    VenueName = "Grand Theatre", 
                    Address = "123 Main Street", 
                    City = "Colombo", 
                    PostalCode = "00100",
                    Capacity = 500,
                    Facilities = "Parking, Restaurant, Wheelchair Access",
                    ContactPhone = "011-2345678",
                    ContactEmail = "info@grandtheatre.lk"
                },
                new Venue 
                { 
                    VenueName = "Cultural Center", 
                    Address = "456 Galle Road", 
                    City = "Colombo", 
                    PostalCode = "00300",
                    Capacity = 300,
                    Facilities = "AC Hall, Cafeteria, Parking",
                    ContactPhone = "011-3456789",
                    ContactEmail = "contact@culturalcenter.lk"
                },
                new Venue 
                { 
                    VenueName = "Arts Pavilion", 
                    Address = "789 Independence Avenue", 
                    City = "Colombo", 
                    PostalCode = "00700",
                    Capacity = 200,
                    Facilities = "Gallery Space, Workshop Room",
                    ContactPhone = "011-4567890",
                    ContactEmail = "info@artspavilion.lk"
                },
                new Venue 
                { 
                    VenueName = "Music Hall", 
                    Address = "321 Duplication Road", 
                    City = "Colombo", 
                    PostalCode = "00400",
                    Capacity = 800,
                    Facilities = "VIP Lounge, Bar, Premium Sound System",
                    ContactPhone = "011-5678901",
                    ContactEmail = "bookings@musichall.lk"
                }
            };
            context.Venues.AddRange(venues);
            context.SaveChanges();

            // Seed Members
            var members = new List<Member>
            {
                new Member 
                { 
                    FirstName = "John", 
                    LastName = "Doe", 
                    Email = "john.doe@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Phone = "0771234567",
                    JoinDate = DateTime.Now.AddMonths(-6),
                    Status = "Active"
                },
                new Member 
                { 
                    FirstName = "Jane", 
                    LastName = "Smith", 
                    Email = "jane.smith@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Phone = "0772345678",
                    JoinDate = DateTime.Now.AddMonths(-4),
                    Status = "Active"
                },
                new Member 
                { 
                    FirstName = "Mike", 
                    LastName = "Johnson", 
                    Email = "mike.johnson@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Phone = "0773456789",
                    JoinDate = DateTime.Now.AddMonths(-2),
                    Status = "Active"
                }
            };
            context.Members.AddRange(members);
            context.SaveChanges();

            // Seed Events
            var events = new List<Event>
            {
                new Event 
                { 
                    VenueID = 1, 
                    CategoryID = 1,
                    EventName = "Classical Music Night",
                    Description = "An evening of classical music featuring renowned artists",
                    EventDate = DateTime.Today.AddDays(15),
                    StartTime = new TimeSpan(19, 0, 0),
                    EndTime = new TimeSpan(22, 0, 0),
                    TicketPrice = 2500.00M,
                    AvailableSeats = 450,
                    TotalSeats = 500,
                    Status = "Upcoming",
                    ImageURL = "/images/events/classical-music.jpg",
                    CreatedDate = DateTime.Now
                },
                new Event 
                { 
                    VenueID = 2, 
                    CategoryID = 2,
                    EventName = "Shakespeare's Hamlet",
                    Description = "A modern adaptation of the classic tragedy",
                    EventDate = DateTime.Today.AddDays(20),
                    StartTime = new TimeSpan(18, 30, 0),
                    EndTime = new TimeSpan(21, 0, 0),
                    TicketPrice = 1500.00M,
                    AvailableSeats = 280,
                    TotalSeats = 300,
                    Status = "Upcoming",
                    ImageURL = "/images/events/hamlet.jpg",
                    CreatedDate = DateTime.Now
                },
                new Event 
                { 
                    VenueID = 3, 
                    CategoryID = 3,
                    EventName = "Contemporary Art Exhibition",
                    Description = "Featuring works from emerging local artists",
                    EventDate = DateTime.Today.AddDays(10),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(18, 0, 0),
                    TicketPrice = 500.00M,
                    AvailableSeats = 150,
                    TotalSeats = 200,
                    Status = "Upcoming",
                    ImageURL = "/images/events/art-exhibition.jpg",
                    CreatedDate = DateTime.Now
                },
                new Event 
                { 
                    VenueID = 4, 
                    CategoryID = 1,
                    EventName = "Rock Concert 2026",
                    Description = "The biggest rock bands performing live",
                    EventDate = DateTime.Today.AddDays(30),
                    StartTime = new TimeSpan(20, 0, 0),
                    EndTime = new TimeSpan(23, 30, 0),
                    TicketPrice = 3500.00M,
                    AvailableSeats = 700,
                    TotalSeats = 800,
                    Status = "Upcoming",
                    ImageURL = "/images/events/rock-concert.jpg",
                    CreatedDate = DateTime.Now
                },
                new Event 
                { 
                    VenueID = 2, 
                    CategoryID = 4,
                    EventName = "Photography Workshop",
                    Description = "Learn professional photography techniques from experts",
                    EventDate = DateTime.Today.AddDays(7),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    TicketPrice = 4500.00M,
                    AvailableSeats = 25,
                    TotalSeats = 30,
                    Status = "Upcoming",
                    ImageURL = "/images/events/photography-workshop.jpg",
                    CreatedDate = DateTime.Now
                },
                new Event 
                { 
                    VenueID = 1, 
                    CategoryID = 5,
                    EventName = "Cultural Festival 2026",
                    Description = "A celebration of diverse cultures with music, dance, and food",
                    EventDate = DateTime.Today.AddDays(45),
                    StartTime = new TimeSpan(11, 0, 0),
                    EndTime = new TimeSpan(20, 0, 0),
                    TicketPrice = 1000.00M,
                    AvailableSeats = 450,
                    TotalSeats = 500,
                    Status = "Upcoming",
                    ImageURL = "/images/events/cultural-festival.jpg",
                    CreatedDate = DateTime.Now
                }
            };
            context.Events.AddRange(events);
            context.SaveChanges();

            // Seed some bookings
            var bookings = new List<Booking>
            {
                new Booking
                {
                    MemberID = 1,
                    EventID = 1,
                    BookingDate = DateTime.Now.AddDays(-5),
                    TotalTickets = 2,
                    TotalAmount = 5000.00M,
                    Status = "Confirmed",
                    BookingReference = "BK" + DateTime.Now.ToString("yyyyMMdd") + "001"
                },
                new Booking
                {
                    MemberID = 2,
                    EventID = 2,
                    BookingDate = DateTime.Now.AddDays(-3),
                    TotalTickets = 4,
                    TotalAmount = 6000.00M,
                    Status = "Confirmed",
                    BookingReference = "BK" + DateTime.Now.ToString("yyyyMMdd") + "002"
                }
            };
            context.Bookings.AddRange(bookings);
            context.SaveChanges();

            // Seed reviews
            var reviews = new List<Review>
            {
                new Review
                {
                    MemberID = 1,
                    EventID = 1,
                    Rating = 5,
                    Comment = "Amazing performance! The artists were incredible.",
                    ReviewDate = DateTime.Now.AddDays(-2),
                    IsApproved = true
                },
                new Review
                {
                    MemberID = 2,
                    EventID = 2,
                    Rating = 4,
                    Comment = "Great modern interpretation of a classic. Highly recommended!",
                    ReviewDate = DateTime.Now.AddDays(-1),
                    IsApproved = true
                }
            };
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
        }
    }
}
