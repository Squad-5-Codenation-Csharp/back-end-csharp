using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public class AuthService : IAuthService
    {
        private const int IterationCount = 10000;

        public string Hash(string password)
        {
            var salt = GenerateRandomSalt();

            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: IterationCount,
            numBytesRequested: 256 / 8));

           return $"{IterationCount}.{Convert.ToBase64String(salt)}.{passwordHash}";
        }

        private byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[128 / 8]; // 128bit salt
            RandomNumberGenerator.Create().GetBytes(salt);

            return salt;
        }
    }
}
