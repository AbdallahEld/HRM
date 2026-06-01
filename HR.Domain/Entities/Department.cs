using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("department")]
        public List<Employee> Employees { get; set; } = new List<Employee>();
        //-------------------------------------------------------------------------------//

        //----------------------------One To One Relationship----------------------------//
        [ForeignKey("Employee")]
        public int ManagerId { get; set; }
        [InverseProperty("ManagedDepartment")]
        public Employee? Manager { get; set; }
        //-------------------------------------------------------------------------------//

    }
}
