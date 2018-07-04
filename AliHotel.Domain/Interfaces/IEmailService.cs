using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AliHotel.Domain.Interfaces
{
    /// <summary>
    /// Interface for working with emails
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
