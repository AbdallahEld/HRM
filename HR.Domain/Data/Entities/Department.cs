using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string CostCenter { get; set; }
        public int HeadCount { get; set; } = 0;
        //---------------------------Self Relationship-----------------------------------//
        [ForeignKey("ParentDepartment")]
        public int? ParentDepartmentId { get; set; }
        [InverseProperty("SubDepartments")]
        public Department? ParentDepartment { get; set; }
        [InverseProperty("ParentDepartment")]
        public List<Department> SubDepartments { get; set; } = new List<Department>();
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("Department")]
        public List<Employee> Employees { get; set; } = new List<Employee>();
        //-------------------------------------------------------------------------------//

        //----------------------------One To One Relationship----------------------------//
        [ForeignKey("Employee")]
        public int? ManagerId { get; set; }
        [InverseProperty("ManagedDepartment")]
        public Employee? Manager { get; set; }
        //-------------------------------------------------------------------------------//

    }
}
