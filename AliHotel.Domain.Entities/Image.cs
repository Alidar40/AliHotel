using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AliHotel.Domain.Entities
{
    class Image
    {
        /// <summary>
        /// Image Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Image name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Image extension
        /// </summary>
        public string Extention { get; set; }

        /// <summary>
        /// Describes when the images was created
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
