using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Clock-In Time")]
        public TimeSpan inTime { get; set; }
        [Display(Name = "Clock-In Time")]
        public TimeSpan outTime { get; set; }
        [Display(Name = "Break Start Time")]
        public TimeSpan inBreak { get; set; }
        [Display(Name = "Break End Time")]
        public TimeSpan outBreak { get; set; }
    }

}
