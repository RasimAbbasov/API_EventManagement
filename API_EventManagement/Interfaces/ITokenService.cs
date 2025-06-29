using API_EventManagement.Models;
namespace API_EventManagement.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(AppUser user);
    }
}
