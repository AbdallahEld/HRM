namespace HR.Application.Features.Location.DTOs
{
    public class LocationReadDTO
    {
        public int Id { get; set; }
        public bool IsRemote { get; set; } = false;
        public string? Address { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
    }
}
