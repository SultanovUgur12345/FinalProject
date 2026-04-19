using FinalProjectApi.Models;

namespace FinalProjectApi.Services.Interfaces
{
    public interface IJwtService
    {
        (string token, DateTime expireDate) GenerateToken(AppUser user, IList<string> roles);
    }
}
