using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models
{
    [Table("EVENT")]
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        public int VenueID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; } = string.Empty;

        [Column(TypeName = "text")]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue)]
        [Display(Name = "Ticket Price")]
        [DataType(DataType.Currency)]
        public decimal TicketPrice { get; set; }

        [Required]
        [Display(Name = "Available Seats")]
        public int AvailableSeats { get; set; }

        [Required]
        [Display(Name = "Total Seats")]
        public int TotalSeats { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Upcoming";

        [StringLength(255)]
        [Display(Name = "Image URL")]
        public string? ImageURL { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        [ForeignKey("VenueID")]
        public virtual Venue Venue { get; set; } = null!;

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Computed Properties
        [NotMapped]
        public bool IsSoldOut => AvailableSeats == 0;

        [NotMapped]
        public int SeatsSold => TotalSeats - AvailableSeats;

        [NotMapped]
        public double OccupancyRate => TotalSeats > 0 ? (double)SeatsSold / TotalSeats * 100 : 0;
    }
}
