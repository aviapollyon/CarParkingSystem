using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class FaultReport
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Location must be a single letter.")]
        [Display(Name = "Parking Section Affected")]
        public string Location { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string Description { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";

        public long? AssignedTech { get; set; } // Nullable 8-digit number of type long
    }
}

