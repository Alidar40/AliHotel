using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AliHotel.Domain.Entities
{
    /// <summary>
    /// This class describes hotel's room
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Rooms Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

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
        [ForeignKey(nameof(RoomType))]
        public Guid RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }

        /// <summary>
        /// All images related to this room
        /// </summary>
        public List<Image> Images { get; set; }
    }
}
