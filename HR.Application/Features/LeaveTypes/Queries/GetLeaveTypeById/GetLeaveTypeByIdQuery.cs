using HR.Application.Features.LeaveTypes.DTOs;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Queries.GetLeaveTypeById
{
    public class GetLeaveTypeByIdQuery (int id) : IRequest<ApiResponse<LeaveTypeReadDTO>>
    {
        public int Id { get; } = id;
    }
}
