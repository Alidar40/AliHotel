using AliHotel.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AliHotel.Identity
{
    public class Md5PasswordValidator : IPasswordValidator<User>
    {
        private readonly IHashProvider _hashProvider;
        public Md5PasswordValidator(IHashProvider hashProvider)
        {
            _hashProvider = hashProvider;
        }

        Task<IdentityResult> IPasswordValidator<User>.ValidateAsync(UserManager<User> manager, User user, string password)
        {
            var hash = _hashProvider.GetHash(user.PasswordSalt + password);

            if (hash == user.PasswordHash)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed());
            }
        }
    }
}
