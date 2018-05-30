using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AliHotel.Domain.Entities
{
    /// <summary>
    /// Describes user's role
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Role Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public RolesOptions Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Users that have this role
        /// </summary>
        public virtual List<User> Users { get; set; }

        public Role()
        {

        }

        /// <summary>
        /// Role constructor
        /// </summary>
        /// <param name="role">Role itself</param>
        /// <param name="roleName">Role's name</param>
        public Role(RolesOptions role, string roleName)
        {
            Id = role;
            RoleName = roleName;
        }
    }
}
