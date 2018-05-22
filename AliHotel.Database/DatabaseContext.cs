using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AliHotel.Domain.Entities;

namespace AliHotel.Database
{
    /// <summary>
    /// Context of database
    /// </summary>
    public class DatabaseContext: DbContext//IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt): base(opt)
        {
        }

        /// <summary>
        /// List of orders
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// List of rooms
        /// </summary>
        public DbSet<Room> Rooms { get; set; }

        /// <summary>
        /// List of room types
        /// </summary>
        public DbSet<RoomType> RoomTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(x => new { x.UserId, x.RoomId });
            modelBuilder.Entity<Room>().HasKey(x => new { x.RoomTypeId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
