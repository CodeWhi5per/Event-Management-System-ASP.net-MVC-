using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models
{
    [Table("BOOKING_DETAIL")]
    public class BookingDetail
    {
        [Key]
        public int DetailID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Seat Type")]
        public string SeatType { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        // Navigation Properties
        [ForeignKey("BookingID")]
        public virtual Booking Booking { get; set; } = null!;
    }
}
