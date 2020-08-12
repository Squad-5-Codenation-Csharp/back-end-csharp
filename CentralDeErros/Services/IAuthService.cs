using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public interface IAuthService
    {
        string Hash(string password);

        public (bool Verified, bool NeedsUpgrade) ComparePassword(string hashedPassword, string Password);
    }
}
