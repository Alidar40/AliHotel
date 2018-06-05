using System;
using System.Collections.Generic;
using System.Text;

namespace AliHotel.Domain.Utils
{
    /// <summary>
    /// Static class for returning randomized strings
    /// </summary>
    public static class Randomizer
    {
        /// <summary>
        /// Returns random string
        /// </summary>
        /// <param name="lenght">Length of returned string</param>
        /// <returns></returns>
        public static string GetRandString(int lenght)
        {
            StringBuilder result = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < lenght; i++)
            {
                result.Append((char)rand.Next('a', 'z'));
            }
            return result.ToString();
        }

        /// <summary>
        /// Returns random string of numbers
        /// </summary>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static string GetRandNumbers(int lenght)
        {
            StringBuilder result = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < lenght; i++)
            {
                result.Append(rand.Next(0, 9));
            }
            return result.ToString();
        }
    }
}
