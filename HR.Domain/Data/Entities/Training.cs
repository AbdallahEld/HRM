using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class Training : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly Date { get; set; }
        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("Training")]
        public List<EmployeeTrainings> EmployeeTrainings { get; set; } = new List<EmployeeTrainings>();
        //-------------------------------------------------------------------------------//
    }
}
