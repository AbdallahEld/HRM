namespace HR.Application.Features.Account.DTOs
{
    public class RegistrationResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
