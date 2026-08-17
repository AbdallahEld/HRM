using FluentValidation;
using FluentValidation.Results;
using HR.Application.Behaviors;
using HR.Application.Features.Departments.Commands.CreateDepartment;
using HR.Application.Features.Departments.Commands.DeleteDepartment;
using HR.Application.Features.Departments.Commands.UpdateDepartment;
using HR.Application.Features.Departments.Queries.GetAllDepartments;
using HR.Application.Features.Departments.Queries.GetDepartmentById;
using HR.Application.Features.Departments.Services;
using HR.Application.Shared;
using HR.Domain.Data.Entities;
using HR.Domain.UnitOfWork;
using MediatR;
using Moq;

namespace HR.Tests
{
    public class DepartmentHierarchyTests
    {
        public class ValidationDummyCommand : IRequest<ApiResponse<int>>
        {
            public string Name { get; set; }
        }

        [Fact]
        public async Task Handle_WhenDepartmentExists_ShouldReturnsSuccessResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            int departmentId = 1;
            var fakeDepartment = new Department
            {
                Id = departmentId,
                Name = "IT",
            };

            mockUnitOfWork.Setup(u => u._DepartmentRepository.GetByIdAsync(departmentId))
                          .ReturnsAsync(fakeDepartment);

            var handler = new GetDepartmentByIdQueryHandler(mockUnitOfWork.Object);
            var query = new GetDepartmentByIdQuery(departmentId);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);

            Assert.True(result.Success);

            Assert.Equal("IT", result.Data.Name);
            Assert.Equal(departmentId, result.Data.Id);

            mockUnitOfWork.Verify(u => u._DepartmentRepository.GetByIdAsync(departmentId), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenDepartmentDoesNotExists_ShouldReturnFailureResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            int invalidDepartmentId = 999;

            mockUnitOfWork.Setup(u => u._DepartmentRepository.GetByIdAsync(invalidDepartmentId))
                          .ReturnsAsync((Department)null);

            var handler = new GetDepartmentByIdQueryHandler(mockUnitOfWork.Object);
            var query = new GetDepartmentByIdQuery(invalidDepartmentId);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);

            Assert.False(result.Success);

            mockUnitOfWork.Verify(u => u._DepartmentRepository.GetByIdAsync(invalidDepartmentId), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenGettingAllDepartments_ShouldReturnSuccessResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var fakeDepartments = new List<Department>()
            {
                new Department() { Name = "Software Development" },
                new Department() { Name = ".NET"},
                new Department() { Name = "JAVA"},
            };

            mockUnitOfWork.Setup(u => u._DepartmentRepository.GetAllAsync())
                          .ReturnsAsync(fakeDepartments);

            var handler = new GetAllDepartmentsQueryHandler(mockUnitOfWork.Object);
            var query = new GetAllDepartmentsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);

            Assert.True(result.Success);

            Assert.Equal(3, result.Data.Count());

            mockUnitOfWork.Verify(u => u._DepartmentRepository.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenCreatingDepartment_ShouldReturnSuccessResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCapacityChecker = new Mock<IDepartmentCapacityChecker>();

            var command = new CreateDepartmentCommand
            {
                Name = "IT",
                CostCenter = "IT011",
                HeadCount = 30
            };

            mockUnitOfWork.Setup(u => u._DepartmentRepository.AddAsync(It.IsAny<Department>()))
                          .Returns(Task.CompletedTask);

            mockUnitOfWork.Setup(u => u.SaveChangesAsync())
                          .ReturnsAsync(1);

            var handler = new CreateDepartmentCommandHandler(mockUnitOfWork.Object, mockCapacityChecker.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Success);

            Assert.Equal(0, result.Data);

            mockUnitOfWork.Verify(u => u._DepartmentRepository.AddAsync(It.IsAny<Department>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenDepartmentCapacityInvalid_ReturnFailureResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCapacityChecker = new Mock<IDepartmentCapacityChecker>();

            var command = new CreateDepartmentCommand
            {
                Name = "HR Sub-Department",
                CostCenter = "HR002",
                HeadCount = 100,
                ParentDepartmentId = 1
            };

            var fakeFailureResponse = ApiResponse<int>.FailureResponse(
                    new List<string> { "Headcount exceeds parent capacity." },
                    "Validation Failed"
                );

            mockCapacityChecker.Setup(c => c.ValidateChildCapacityAsync(command.ParentDepartmentId.Value, command.HeadCount))
                               .ReturnsAsync(fakeFailureResponse);

            var handler = new CreateDepartmentCommandHandler(mockUnitOfWork.Object, mockCapacityChecker.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);

            Assert.Equal("Validation Failed", result.Message);

            mockUnitOfWork.Verify(u => u._DepartmentRepository.AddAsync(It.IsAny<Department>()), Times.Never);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);

            mockCapacityChecker.Verify(c => c.ValidateChildCapacityAsync(command.ParentDepartmentId.Value, command.HeadCount), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenValidationFails_ShouldReturnFailureResponse_AndShortCircuitPipeline()
        {
            var request = new ValidationDummyCommand { Name = "" };

            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Name cannot be empty")
            };
            var validationResult = new ValidationResult(failures);

            var validatorMock = new Mock<IValidator<ValidationDummyCommand>>();
            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<ValidationDummyCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            var validators = new List<IValidator<ValidationDummyCommand>> { validatorMock.Object };
            var behavior = new ValidationBehavior<ValidationDummyCommand, ApiResponse<int>>(validators);

            
            bool isNextCalled = false;
            RequestHandlerDelegate<ApiResponse<int>> next = delegate
            {
                isNextCalled = true; 
                return Task.FromResult(ApiResponse<int>.SuccessResponse(1, "Success"));
            };

            var result = await behavior.Handle(request, next, CancellationToken.None);


            Assert.NotNull(result);

            Assert.False(result.Success);
            Assert.Equal("Validation failed", result.Message);

            Assert.Contains("Name: Name cannot be empty", result.Errors);

            Assert.False(isNextCalled);
        }

        [Fact]
        public async Task Handle_Update_WhenDepartmentNotFound_ShouldThrowException()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCapacityChecker = new Mock<IDepartmentCapacityChecker>();

            var command = new UpdateDepartmentCommand { Id = 999 };

            mockUnitOfWork.Setup(u => u._DepartmentRepository.GetByIdAsync(command.Id))
                          .ReturnsAsync((Department)null);

            var handler = new UpdateDepartmentCommandHandler(mockUnitOfWork.Object, mockCapacityChecker.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal($"Department With Id = {command.Id} is not Found", exception.Message);

            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_WhenChildCapacityInvalid_ShouldReturnFailureResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCapacityChecker = new Mock<IDepartmentCapacityChecker>();

            var command = new UpdateDepartmentCommand { Id = 1, ParentDepartmentId = 2, HeadCount = 50 };
            var fakeDepartment = new Department { Id = 1 };

            mockUnitOfWork.Setup(u => u._DepartmentRepository.GetByIdAsync(command.Id))
                          .ReturnsAsync(fakeDepartment);

            var fakeFailure = ApiResponse<int>.FailureResponse(new List<string> { "Error" }, "Child Capacity Failed");
            mockCapacityChecker.Setup(c => c.ValidateChildCapacityAsync(command.ParentDepartmentId.Value, command.HeadCount, command.Id))
                               .ReturnsAsync(fakeFailure);

            var handler = new UpdateDepartmentCommandHandler(mockUnitOfWork.Object, mockCapacityChecker.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("Child Capacity Failed", result.Message);

            mockCapacityChecker.Verify(c => c.ValidateParentCapacityAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_WhenValidationsPass_ShouldUpdateAndReturnSuccess()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCapacityChecker = new Mock<IDepartmentCapacityChecker>();

            var command = new UpdateDepartmentCommand
            {
                Id = 1,
                Name = "New IT",
                CostCenter = "123",
                HeadCount = 10,
                ParentDepartmentId = 2
            };
            var fakeDepartment = new Department { Id = 1, Name = "Old IT" };

            mockUnitOfWork.Setup(u => u._DepartmentRepository.GetByIdAsync(command.Id))
                          .ReturnsAsync(fakeDepartment);

            mockCapacityChecker.Setup(c => c.ValidateChildCapacityAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                               .ReturnsAsync(ApiResponse<int>.SuccessResponse(0, ""));
            mockCapacityChecker.Setup(c => c.ValidateParentCapacityAsync(It.IsAny<int>(), It.IsAny<int>()))
                               .ReturnsAsync(ApiResponse<int>.SuccessResponse(0, ""));

            var handler = new UpdateDepartmentCommandHandler(mockUnitOfWork.Object, mockCapacityChecker.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Equal(fakeDepartment.Id, result.Data);

            Assert.Equal("New IT", fakeDepartment.Name);

            mockUnitOfWork.Verify(u => u._DepartmentRepository.UpdateAsync(fakeDepartment), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Delete_WhenDepartmentNotFound_ShouldThrowException()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var command = new DeleteDepartmentCommand(999);

            mockUnitOfWork.Setup(u => u._DepartmentRepository.GetByIdAsync(command.Id))
                          .ReturnsAsync((Department)null);

            var handler = new DeleteDepartmentCommandHandler(mockUnitOfWork.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal($"Department with Id = {command.Id} not Found", exception.Message);
            mockUnitOfWork.Verify(u => u._DepartmentRepository.DeleteAsync(It.IsAny<Department>()), Times.Never);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_Delete_WhenDepartmentExists_ShouldDeleteAndSaveChanges()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var command = new DeleteDepartmentCommand (1);
            var fakeDepartment = new Department { Id = 1, Name = "IT" };

            mockUnitOfWork.Setup(u => u._DepartmentRepository.GetByIdAsync(command.Id))
                          .ReturnsAsync(fakeDepartment);

            var handler = new DeleteDepartmentCommandHandler(mockUnitOfWork.Object);

            await handler.Handle(command, CancellationToken.None);

            mockUnitOfWork.Verify(u => u._DepartmentRepository.DeleteAsync(fakeDepartment), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
