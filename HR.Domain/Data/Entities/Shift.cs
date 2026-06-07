using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR.Domain.Data.Entities
{
    public class Shift : BaseEntity
    {
        public string Name { get; set; }
        public bool IsFlexible { get; set; } = false;
        public int? RequiredHours { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public int GracePeriodMinutes { get; set; } = 0;

        //---------------------------One To Many Relationship----------------------------//
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship ---------------------------//
        [InverseProperty("DefaultShift")]
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        //-------------------------------------------------------------------------------//
    }
}
