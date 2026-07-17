using HR.Domain.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
