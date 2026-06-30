using HR.Domain.Data.Entities;
using HR.Domain.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR.Application.Features.Employee.DTOs
{
    public class EmployeeReadDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public DateTime? ProbationEndDate { get; set; }
        public decimal? BaseSalary { get; set; }
        public decimal? HourlyRate { get; set; }
        public string Position { get; set; }
        public DateOnly HireDate { get; set; }
        public string Nationality { get; set; }

        public int? ManagerId { get; set; }
        public int DepartmentId { get; set; }
        public int DefaultShiftId { get; set; }
    }
}
