using CentralDeErros.Api.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Text;
using CentralDeErros.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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

        public (bool Verified, bool NeedsUpgrade) ComparePassword(string hashedPassword, string Password)
        {
            var parts = hashedPassword.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Hash no formato incorreto. Favor contatar um administrador.");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != IterationCount;

            var verified = KeyDerivation.Pbkdf2(
                Password,
                salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: iterations,
                numBytesRequested: 256 / 8
            ).SequenceEqual(key);
            
            return (verified, needsUpgrade);
        }

        public string Authenticate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Variables.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.Now.AddHours(Variables.TokenExpireTime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
