using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Model for registering user
    /// </summary>
    public class UserRegisterModel
    {
        /// <summary>
        /// User's full name
        /// </summary>
        [Required(ErrorMessage = "Please, enter your full name")]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// User's e-mail
        /// </summary>
        [Required(ErrorMessage = "Please, enter your e-mail")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User's birthdate
        /// </summary>
        [Required(ErrorMessage = "Please, enter your birthdate. You must be at least 18 y.o.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// User's credit card number
        /// </summary>
        [StringLength(255)]
        public string CreditCard { get; set; }
    }
}
