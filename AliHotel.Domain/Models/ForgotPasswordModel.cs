using System;
using System.Collections.Generic;
using System.Text;

namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Model for password reseting
    /// </summary>
    public class ForgotPasswordModel
    {
        /// <summary>
        /// Email of user who forgot the password
        /// </summary>
        public string Email{ get; set; }
        
        /// <summary>
        /// Code given to user to restore password
        /// </summary>
        public string Code { get; set; }
    }
}
