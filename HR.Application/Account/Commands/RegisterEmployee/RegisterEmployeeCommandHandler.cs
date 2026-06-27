using HR.Application.Account.DTOs;
using HR.Domain.Data.Entities;
using HR.Domain.Data.Entities.Identity;
using HR.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Account.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommandHandler(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork) : IRequestHandler<RegisterEmployeeCommand, RegistrationResult>
    {
        public async Task<RegistrationResult> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            var newEmployee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                NationalId = request.NationalId,
                Nationality = request.Nationality,
                EmploymentType = request.EmploymentType,
                EmploymentStatus = request.EmploymentStatus,
                Position = request.Position,
                HireDate = request.HireDate,
                ProbationEndDate = request.ProbationEndDate,
                BaseSalary = request.BaseSalary,
                HourlyRate = request.HourlyRate,
                DepartmentId = request.DepartmentId,
                DefaultShiftId = request.DefaultShiftId,
                ManagerId = request.ManagerId
            };

            try
            {
                await unitOfWork._EmployeeRepository.AddAsync(newEmployee);
                await unitOfWork.SaveChangesAsync();
            }
            catch
            {

                return new RegistrationResult
                {
                    IsSuccess = false,
                    Errors = new List<string> { "An unexpected error occurred during registration. The process was aborted." }
                };
            }

            var newUser = new User
            {
                UserName = request.Email,
                Email = request.Email,
                EmployeeId = newEmployee.Id,
            };

            var identityResult = await userManager.CreateAsync(newUser, request.Password);

            if (!identityResult.Succeeded)
            {
                unitOfWork._EmployeeRepository.DeleteAsync(newEmployee);
                await unitOfWork.SaveChangesAsync();

                return new RegistrationResult
                {
                    IsSuccess = false,
                    Errors = identityResult.Errors.Select(e => e.Description).ToList()
                };
            }

            await userManager.AddToRoleAsync(newUser, "Employee");

            return new RegistrationResult { IsSuccess = true };
        }
    }
}
