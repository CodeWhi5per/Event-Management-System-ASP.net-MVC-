using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models
{
    [Table("VENUE")]
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [StringLength(10)]
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        [StringLength(500)]
        public string? Facilities { get; set; }

        [StringLength(20)]
        [Phone]
        [Display(Name = "Contact Phone")]
        public string? ContactPhone { get; set; }

        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Contact Email")]
        public string? ContactEmail { get; set; }

        // Navigation Properties
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
