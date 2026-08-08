using HR.Application.Features.Location.DTOs;
using HR.Application.Features.Shift.DTOs;

namespace HR.Application.Features.LocationShifts.DTOs
{
    public class ShiftLocationDTO
    {
        public ShiftReadDTO Shift { get; set; }
        public LocationReadDTO Location { get; set; }
    }
}
