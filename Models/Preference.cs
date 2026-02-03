using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models
{
    [Table("PREFERENCE")]
    public class Preference
    {
        [Key]
        public int PreferenceID { get; set; }

        [Required]
        public int MemberID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Display(Name = "Added Date")]
        public DateTime AddedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        [ForeignKey("MemberID")]
        public virtual Member Member { get; set; } = null!;

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; } = null!;
    }
}
