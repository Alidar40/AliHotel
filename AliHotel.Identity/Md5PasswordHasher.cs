using AliHotel.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AliHotel.Identity
{
    public class Md5PasswordHasher : IPasswordHasher<User>
    {
        private readonly IHashProvider _hashProvider;

        public Md5PasswordHasher(IHashProvider hashProvider)
        {
            _hashProvider = hashProvider;
        }

        /// <summary>
        /// Hashes password+salt
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(User user, string password)
        {
            if(user == null)
            {
                return _hashProvider.GetHash(password);
            }
            return _hashProvider.GetHash(password + user.PasswordSalt);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            return _hashProvider.GetHash(providedPassword + user.PasswordSalt) == hashedPassword
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
    }
}
