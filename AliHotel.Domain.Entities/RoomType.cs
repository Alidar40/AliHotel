using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AliHotel.Domain.Entities
{
    class RoomType
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
        public int Price { get; set; }

        /// <summary>
        /// Price for each additional person
        /// </summary>
        public int PricePerMen { get; set; }
    }
}
