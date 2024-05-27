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
        [RegularExpression(@"^(?:CA|CAM|CAR|CAW|CAG|CBL|CBM|CBR|CBS|CBT|CBY|CCA|CCC|CCD|CCK|CCM|CCO|CCP|CEA|CEG|CEM|CEO|CER|CES|CEX|CEY|CF|CFA|CFG|CFM|CFP|CFR|CG|CJ|CK|CL|CN|CO|CR|CS|CT|CV|CW|CX|CY|CZ|CCT|NA|NB|NBA|NC|NCO|NCH|NCW|ND|NDE|NDH|NDW|NE|NES|NF|NGL|NH|NHL|NIM|NIN|NIP|NIX|NJ|NK|NKA|NKK|NKR|NKU|NM|NMA|NMG|NMR|NMZ|NN|NND|NO|NP|NPG|NPN|NPP|NR|NRB|NS|NPS|NSC|NT|NTU|NU|NUB|NUD|NUF|NUL|NUM|NUR|NUT|NUZ|NV|NW|NX|NZ|B|DBN|EC|FS|GP|KZN|L|MP|NC|NW|ZA)\s?\d{3}[\s-]?\d{3}$", ErrorMessage = "Please enter a valid South African license plate number.")]
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
