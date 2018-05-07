using System.ComponentModel.DataAnnotations;

namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Model for logging in
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User's e-mail
        /// </summary>
        [Required(ErrorMessage = "Please, enter your e-mail")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        [Required(ErrorMessage = "Please, enter your e-mail")]
        [MinLength(8), StringLength(24)]
        public string Password { get; set; }
    }
}
