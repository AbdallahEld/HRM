using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR.Domain.Data.Entities
{
    public class Location : BaseEntity
    {
        public bool IsRemote { get; set; }
        public string? Address { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("Location")]
        public List<Attendance> Attendances { get; set; } = new List<Attendance>();
        //-------------------------------------------------------------------------------//
    }
}
