namespace HR.Application.Features.Location.Commands.Shared
{
    public interface ILocationCommand
    {
        public bool IsRemote { get; }
        public string? Address { get; }
        public decimal? Lat { get; }
        public decimal? Long { get; }
    }
}
