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
        /// RoomTypes Id
        /// </summary>
        [ForeignKey(nameof(RoomType))]
        public Guid RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }

        /// <summary>
        /// Describes whether room is occupied or not (true means is occupied)
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
        [Range(0, Int32.MaxValue)]
        public int Capacity { get; set; }
        
        /// <summary>
        /// All images related to this room
        /// </summary>
        public List<Image> Images { get; set; }
        
        public Room()
        {

        }

        /// <summary>
        /// Room constructor
        /// </summary>
        /// <param name="id">Номер комнаты</param>
        /// <param name="maxCount">Максимальное число людей, которые могут проживать в комнате</param>
        /// <param name="price">Цена за комнату</param>
        /// <param name="roomOption">Тип комнаты</param>
        /// <param name="isFree">Свободна ли комната</param>
        public Room(Guid id, int maxCount, bool isOccupied, RoomType roomType)
        {
            Id = id;
            Capacity = maxCount;
            IsOccupied = isOccupied;
            RoomTypeId = roomType.Id;
        }
    }
}
