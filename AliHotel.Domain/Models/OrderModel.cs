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
        /// Describes whether user left hotel or not
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// The final sum that user has to pay for accomodation
        /// </summary>
        public int Bill { get; set; }

        /// <summary>
        /// Rooms Id
        /// </summary>
        public Guid RoomId { get; set; }

        public RoomModel Room { get; set; }
    }
}
