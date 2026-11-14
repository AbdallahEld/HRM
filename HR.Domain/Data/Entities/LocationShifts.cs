using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class LocationShifts
    {
        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        [InverseProperty("LocationShifts")]
        public Location Location { get; set; }
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        [InverseProperty("LocationShifts")]
        public Shift Shift { get; set; }
        //-------------------------------------------------------------------------------//
        public ICollection<Attendance> Attendances { get; set; }
    }
}
