using HR.Domain.Data.Entities.Identity;

namespace HR.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateTokenAsync(User user);
    }
}
