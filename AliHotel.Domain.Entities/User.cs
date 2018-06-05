using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AliHotel.Domain.Entities
{
    /// <summary>
    /// This class describes user
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's Id
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Role Id
        /// </summary>
        [ForeignKey(nameof(Role))]
        public RolesOptions RoleId { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        public virtual Role Role { get; set; }

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
        /// Describes whether user confirmed his e-mail or not
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Password salt
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Password hash
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Describes whether user have current booking or not
        /// </summary>
        public bool IsRenter { get; set; }

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

        /// <summary>
        /// History of all bookings made by the user
        /// </summary>
        public List<Order> Orders { get; set; }

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
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="passwordSalt">Salt for password</param>
        /// <param name="passworhHash">Password hash</param>
        /// <param name="role">Role</param>
        public User(string name, string email, DateTime birthDate, string phoneNumber, string passwordSalt, string passworhHash, RolesOptions role)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            PasswordSalt = passwordSalt;
            PasswordHash = passworhHash;
            RoleId = role;
        }
    }
}
