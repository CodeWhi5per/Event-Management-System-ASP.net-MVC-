using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models
{
    [Table("BOOKING")]
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        public int MemberID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total tickets must be at least 1")]
        [Display(Name = "Total Tickets")]
        public int TotalTickets { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Confirmed";

        [Required]
        [StringLength(50)]
        [Display(Name = "Booking Reference")]
        public string BookingReference { get; set; } = string.Empty;

        // Navigation Properties
        [ForeignKey("MemberID")]
        public virtual Member Member { get; set; } = null!;

        [ForeignKey("EventID")]
        public virtual Event Event { get; set; } = null!;

        public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
    }
}
