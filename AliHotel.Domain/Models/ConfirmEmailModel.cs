using System;

namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Model for email confirmation
    /// </summary>
    public class ConfirmEmailModel
    {
        /// <summary>
        /// User's id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Part of user's token
        /// </summary>
        public string Code { get; set; }
    }
}
