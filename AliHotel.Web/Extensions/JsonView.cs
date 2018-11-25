using System;
using AliHotel.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace AliHotel.Web.Extensions
{
    public static class JsonView
    {
        /// <summary>
        /// Method for displaying information about an order
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object OrderView(this Order obj)
        {
            if (obj != null)
            {
                return new
                {
                    obj.Id,
                    obj.Bill,
                    obj.ArrivalDate,
                    obj.DepartureDate,
                    obj.RoomId,
                    obj.Room,
                    obj.Room.Number,
                    obj.Room.RoomType.Name,
                    obj.UserId,
                    UserName = obj.User.Name,
                    obj.IsClosed
                };
            }
            return null;
        }
    }
}
