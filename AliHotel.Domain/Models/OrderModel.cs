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
        /// Room type
        /// </summary>
        public string RoomTypeName { get; set; }

        /// <summary>
        /// Number of people who wants to settle
        /// </summary>
        public int PeopleCount { get; set; }
    }
}
