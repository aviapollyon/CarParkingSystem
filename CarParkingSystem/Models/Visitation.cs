#nullable disable

using System;
using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class Visitation
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstNmae { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastNmae { get; set; }

        [Display(Name = "Purpose of visit")]
        [Required]
        public string purpose { get; set; }

        [Display(Name = "Registration Plate")]
        [Required]
        public string RegistrationPlate { get; set; }

        public string ExitTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Entry Date")]
        [CustomValidation(typeof(Visitation), nameof(ValidateEntryDate))]
        public DateTime EntryDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Entry Time")]
        public string EntryTime { get; set; }

        public string ParkingBay { get; set; }

        public static ValidationResult ValidateEntryDate(DateTime entryDate, ValidationContext context)
        {
            if (entryDate.Date < DateTime.Now.Date)
            {
                return new ValidationResult("Entry date cannot be in the past.");
            }
            return ValidationResult.Success;
        }
    }
}
