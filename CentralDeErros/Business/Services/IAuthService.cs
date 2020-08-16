using CentralDeErros.Api.Models;

namespace CentralDeErros.Services
{
    public interface IAuthService
    {
        string Hash(string password);

        public (bool Verified, bool NeedsUpgrade) ComparePassword(string hashedPassword, string Password);

        public string Authenticate(User user);
    }
}
