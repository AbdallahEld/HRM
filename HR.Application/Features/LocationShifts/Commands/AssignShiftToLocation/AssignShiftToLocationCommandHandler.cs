using HR.Application.Features.LocationShifts.DTOs;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.LocationShifts.Commands.AssignShiftToLocation
{
    public class AssignShiftToLocationCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<AssignShiftToLocationCommand, ApiResponse<ShiftLocationDTO>>
    {
        public async Task<ApiResponse<ShiftLocationDTO>> Handle(AssignShiftToLocationCommand request, CancellationToken cancellationToken)
        {
            var shift = await unitOfWork._ShiftRepository.GetByIdAsync(request.ShiftId);
            if (shift == null)
            {
                return ApiResponse<ShiftLocationDTO>.FailureResponse(new List<string> { "Shift not found" }, "Shift not found");
            }

            var location = await unitOfWork._LocationRepository.GetByIdAsync(request.LocationId);
            if (location == null)
            {
                return ApiResponse<ShiftLocationDTO>.FailureResponse(new List<string> { "Location not found" }, "Location not found");
            }

            var locationShift = new HR.Domain.Data.Entities.LocationShifts
            {
                ShiftId = request.ShiftId,
                LocationId = request.LocationId
            };

            await unitOfWork._LocationShiftsRepositroy.AddAsync(locationShift);
            await unitOfWork.SaveChangesAsync();

            ShiftLocationDTO shiftLocationDTO = new ShiftLocationDTO
            {
                Shift = new Shift.DTOs.ShiftReadDTO
                {
                    Id = shift.Id,
                    Name = shift.Name,
                    StartTime = shift.StartTime,
                    EndTime = shift.EndTime
                },
                Location = new Location.DTOs.LocationReadDTO
                {
                    Id = location.Id,
                    IsRemote = location.IsRemote,
                    Address = location.Address,
                    Lat = location.Lat,
                    Long = location.Long
                }
            };

            return ApiResponse<ShiftLocationDTO>.SuccessResponse(shiftLocationDTO, "Shift assigned to location successfully");
        }
    }
}
