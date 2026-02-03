using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models.ViewModels
{
    public class EventSearchViewModel
    {
        [Display(Name = "Category")]
        public int? CategoryID { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }

        [Display(Name = "Date From")]
        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "Date To")]
        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string? City { get; set; }

        [Display(Name = "Minimum Price")]
        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Maximum Price")]
        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        public decimal? MaxPrice { get; set; }


        // Results
        public List<Event> Results { get; set; } = new List<Event>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
