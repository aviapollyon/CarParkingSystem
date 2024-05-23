using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class FaultReportMessage
    {
        public int Id { get; set; }

        public int FaultId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Report can't exceed 500 characters.")]
        public string MessageReport { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public byte[]? Image { get; set; }

    }
}
