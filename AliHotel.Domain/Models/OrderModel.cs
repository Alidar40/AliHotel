using System;

namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Model for order
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// Users Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Date when user settles in the hotel
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Date when user leaves the hotel
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Rooms Id
        /// </summary>
        public Guid RoomId { get; set; }
    }
}
