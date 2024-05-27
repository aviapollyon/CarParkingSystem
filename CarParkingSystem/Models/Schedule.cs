using System.ComponentModel.DataAnnotations.Schema;

namespace CarParkingSystem.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public long orgNum { get; set; }
        public int shiftId { get; set; }

        [ForeignKey("shiftId")] // Assuming shiftId is the foreign key for the Shift navigation property
        public Shift Shift { get; set; }
    }

}
