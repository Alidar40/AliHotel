namespace AliHotel.Domain.Models
{
    /// <summary>
    /// Model for room type
    /// </summary>
    public class RoomTypeModel
    {
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
        public decimal PricePerMen { get; set; }
    }
}
