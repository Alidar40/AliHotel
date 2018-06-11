using System;
using System.Collections.Generic;
using System.Text;

namespace AliHotel.Identity
{
    /// <summary>
    /// Interface for password hashing by MD5-method
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        /// Returns password hash using MD5-method
        /// </summary>
        /// <param name="hashString"></param>
        /// <returns></returns>
        string GetHash(string hashString);
    }
}
