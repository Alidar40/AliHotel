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
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt): base(opt)
        {
        }

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public DbSet<Role> Roles { get; set; }

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
            modelBuilder.Entity<User>().HasKey(x => new { x.RoleId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
