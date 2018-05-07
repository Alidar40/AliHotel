using System;

namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Images' model
    /// </summary>
    public class ImageModel
    {
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
