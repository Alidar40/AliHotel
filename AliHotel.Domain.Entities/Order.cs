using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AliHotel.Domain.Entities
{
    /// <summary>
    /// This class describes user's order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Orders Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Users Id
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        
        public User User { get; set; }

        /// <summary>
        /// Date when user settles in the hotel
        /// </summary>
        [Required]
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
        [ForeignKey(nameof(Room))]
        public Guid RoomId { get; set; }

        public Room Room { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Order()
        {
        }

        /// <summary>
        /// Order constructor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roomId"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        public Order(Guid userId, Guid roomId, DateTime arrivalDate, DateTime departureDate)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId;
            this.RoomId = roomId;
            this.ArrivalDate = arrivalDate;
            this.DepartureDate = departureDate;
        }
    }
}
