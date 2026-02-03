using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models
{
    [Table("REVIEW")]
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int MemberID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "Your Review")]
        public string? Comment { get; set; }

        [Display(Name = "Review Date")]
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        [Display(Name = "Approved")]
        public bool IsApproved { get; set; } = false;

        // Navigation Properties
        [ForeignKey("MemberID")]
        public virtual Member Member { get; set; } = null!;

        [ForeignKey("EventID")]
        public virtual Event Event { get; set; } = null!;
    }
}
