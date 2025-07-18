using System.Security.Claims;

namespace DotNetTraining.Services
{
    public interface ITokenService
    {

        public string GenerateToken(string userId, string email, IList<string> roles);
        public ClaimsPrincipal? ValidateToken(string token);
    }
}
