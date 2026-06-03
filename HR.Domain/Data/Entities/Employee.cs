using HR.Domain.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public EmploymentStatus EmployementStatus { get; set; }
        public DateTime? ProbationEndDate { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string Position { get; set; }
        public DateOnly HireDate { get; set; }
        public string Nationality { get; set; }
        //---------------------------Self Relationship-----------------------------------//
        [ForeignKey("manager")]
        public int? ManagerId { get; set; }
        [InverseProperty("employees")]
        public Employee manager { get; set; }
        [InverseProperty("manager")]
        public List<Employee> employees { get; set; }
        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        [InverseProperty("Employees")]
        public Department? department { get; set; }
        //-------------------------------------------------------------------------------//

        //---------------------------One To One Relationship-----------------------------//
        [InverseProperty("Manager")]
        public Department? ManagedDepartment { get; set; }
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("Employee")]
        public List<Attendance> Attendances { get; set; } = new List<Attendance>();
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("Employee")]
        public List<Leave> Leaves { get; set; } = new List<Leave>();
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("Approver")]
        public List<Leave> ApprovedLeaves { get; set; } = new List<Leave>();
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("Employee")]
        public List<Payroll> Payrolls { get; set; } = new List<Payroll>();
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [InverseProperty("Employee")]
        public List<EmployeeTrainings> EmployeeTrainings { get; set; } = new List<EmployeeTrainings>();
        //-------------------------------------------------------------------------------//

    }
}
