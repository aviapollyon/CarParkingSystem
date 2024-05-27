#nullable disable
using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class EventCheckIn
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^(?:CA|CAM|CAR|CAW|CAG|CBL|CBM|CBR|CBS|CBT|CBY|CCA|CCC|CCD|CCK|CCM|CCO|CCP|CEA|CEG|CEM|CEO|CER|CES|CEX|CEY|CF|CFA|CFG|CFM|CFP|CFR|CG|CJ|CK|CL|CN|CO|CR|CS|CT|CV|CW|CX|CY|CZ|CCT|NA|NB|NBA|NC|NCO|NCH|NCW|ND|NDE|NDH|NDW|NE|NES|NF|NGL|NH|NHL|NIM|NIN|NIP|NIX|NJ|NK|NKA|NKK|NKR|NKU|NM|NMA|NMG|NMR|NMZ|NN|NND|NO|NP|NPG|NPN|NPP|NR|NRB|NS|NPS|NSC|NT|NTU|NU|NUB|NUD|NUF|NUL|NUM|NUR|NUT|NUZ|NV|NW|NX|NZ|B|DBN|EC|FS|GP|KZN|L|MP|NC|NW|ZA)\s?\d{3}[\s-]?\d{3}$", ErrorMessage = "Please enter a valid South African license plate number.")]
        public string RegPlate { get; set; }    
        public string CheckInTime {  get; set; }
        public string CheckOutTime {  get; set; }   
        public int EventId {  get; set; }   
    }
}
