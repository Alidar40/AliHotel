using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Model for room
    /// </summary>
    public class RoomModel
    {
        /// <summary>
        /// Describes whether room is occupied or not
        /// </summary>
        public bool IsOccupied { get; set; }

        /// <summary>
        /// Rooms number
        /// </summary>
        [Required]
        public int Number { get; set; }

        /// <summary>
        /// Describes how many people the room is designed for
        /// </summary>
        [Required]
        public int Capacity { get; set; }

        /// <summary>
        /// RoomTypes Id
        /// </summary>
        public Guid RoomTypeId { get; set; }

        public RoomTypeModel RoomType { get; set; }

        /// <summary>
        /// All images related to this room
        /// </summary>
        public List<ImageModel> Images { get; set; }
    }
}
