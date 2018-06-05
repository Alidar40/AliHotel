using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Model for editing user's data
    /// </summary>
    public class UserEditModel
    {
        /// <summary>
        /// User's e-mail
        /// </summary>
        [Required(ErrorMessage = "Please, enter your e-mail")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User's full name
        /// </summary>
        [Required(ErrorMessage = "Please, enter your full name")]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User's credit card number
        /// </summary>
        [StringLength(255)]
        public string CreditCard { get; set; }
    }
}
