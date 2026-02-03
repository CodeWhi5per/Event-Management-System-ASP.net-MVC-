using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models
{
    [Table("INQUIRY")]
    public class Inquiry
    {
        [Key]
        public int InquiryID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        [Phone]
        public string? Phone { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "text")]
        public string Message { get; set; } = string.Empty;

        [Display(Name = "Inquiry Date")]
        public DateTime InquiryDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        [Display(Name = "Response Date")]
        public DateTime? ResponseDate { get; set; }

        [Column(TypeName = "text")]
        public string? Response { get; set; }
    }
}
