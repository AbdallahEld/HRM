using HR.Application.Features.Account.DTOs;
using HR.Application.Shared;
using HR.Domain.Data.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Features.Account.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommand : IRequest<ApiResponse<int>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string NationalId { get; set; }
        public string Nationality { get; set; }

        public EmploymentType EmploymentType { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public string Position { get; set; }
        public DateOnly HireDate { get; set; }
        public DateTime? ProbationEndDate { get; set; }

        public decimal? BaseSalary { get; set; }
        public decimal? HourlyRate { get; set; }

        public int DepartmentId { get; set; }
        public int DefaultShiftId { get; set; }
        public int? ManagerId { get; set; }

    }
}
