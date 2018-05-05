using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AliHotel.Domain.Entities
{
    /// <summary>
    /// This class describes user
    /// </summary>
    class User
    {
        /// <summary>
        /// Users Id
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Users e-mail
        /// </summary>
        [Required(ErrorMessage = "Please, enter your e-mail")]
        [StringLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Users full name
        /// </summary>
        [Required(ErrorMessage = "Please, enter your full name")]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Describes whether user confirmed his e-mail or not
        /// </summary>
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        /// <summary>
        /// Describes whether user have current booking or not
        /// </summary>
        public bool IsRenter { get; set; }

        /// <summary>
        /// Users birthdate
        /// </summary>
        [Required(ErrorMessage = "Please, enter your birthdate. You must be at least 18 y.o.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Users credit card number
        /// </summary>
        [StringLength(255)]
        public string CreditCard { get; set; }

        /// <summary>
        /// History of all bookings made by the user
        /// </summary>
        public virtual List<Order> Orders { get; set; }

        /// <summary>
        /// Default user constructor
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// User constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="birthDate"></param>
        public User(string name, string email, DateTime birthDate)
        {
            this.Name = name;
            this.Email = email;
            this.BirthDate = birthDate;
        }
    }
}
