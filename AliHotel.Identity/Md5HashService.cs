using System.Security.Cryptography;
using System.Text;

namespace AliHotel.Identity
{
    /// <summary>
    /// Hashes password using MD5-method
    /// </summary>
    public class Md5HashService : IHashProvider
    {
        /// <summary>
        /// Hashes password
        /// </summary>
        /// <param name="hashString"></param>
        /// <returns></returns>
        public string GetHash(string hashString)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(hashString));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
