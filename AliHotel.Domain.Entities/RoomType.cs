﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AliHotel.Domain.Entities
{
    /// <summary>
    /// This class describes type of rooms
    /// </summary>
    public class RoomType
    {
        /// <summary>
        /// RoomTypes Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base price (dollars per day)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Price for each additional person
        /// </summary>
        public int PricePerMen { get; set; }
    }
}
