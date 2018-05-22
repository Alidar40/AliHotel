﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AliHotel.Domain.Entities
{
    /// <summary>
    /// This class describes user
    /// </summary>
    public class User// : IdentityUser<Guid>
    {
        /// <summary>
        /// User's Id
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

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

        public string PasswordHash { get; set; }

        /// <summary>
        /// Describes whether user have current booking or not
        /// </summary>
        public bool IsRenter { get; set; }

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
        public User(string name, string email, DateTime birthDate)
        {
            this.Name = name;
            this.Email = email;
            this.BirthDate = birthDate;
        }
    }
}
