using System;
using System.ComponentModel.DataAnnotations;

namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Model for registering
    /// </summary>
    public class RegisterModel
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
        /// User's birthdate
        /// </summary>
        [Required(ErrorMessage = "Please, enter your birthdate. You must be at least 18 y.o.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        [Required]
        [MinLength(8), StringLength(24)]
        public string Password { get; set; }
    }
}
