#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarParkingSystem.Models
{
    public class StudentEvent
    {
        [Key]
        public int Id { get; set; }
        [Required]      
        public string EventName { get; set; }
        [Required]
        public string Purpose {  get; set; }
        [Required]
        public string ParkingBayId {  get; set; }
        [Required]
        public string VehicleArrive {  get; set; }
        [Required]
        public string EventDescription { get; set; }
        [Required]
        public DateTime EventStartDate { get; set; }
        [Required]
        public DateTime EventEndDate { get; set; }  
        public string EventStatus {  get; set; }    
        public bool isDelete {  get; set; }
        [Required]
        public long OrgNumber {  get; set; }    
        public decimal AmountPaid {  get; set; }
        [NotMapped]
        [Required]
        public string StartTime {  get; set; }
        [NotMapped]
        [Required]
        public string EndTime { get; set; }

        public static ValidationResult ValidateEventEndDate(DateTime eventEndDate, ValidationContext context)
        {
            var model = (StudentEvent)context.ObjectInstance;
            if (eventEndDate <= DateTime.Today)
            {
                return new ValidationResult("Event end date must be a future date.");
            }

            return ValidationResult.Success;
        }
    }
}
