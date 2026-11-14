using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class Location : BaseEntity
    {
        public bool IsRemote { get; set; } = false;
        public string? Address { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("Location")]
        public List<LocationShifts> LocationShifts { get; set; } = new List<LocationShifts>();
        //-------------------------------------------------------------------------------//
    }
}
