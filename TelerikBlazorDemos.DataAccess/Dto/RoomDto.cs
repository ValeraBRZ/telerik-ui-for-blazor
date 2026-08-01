using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public static Func<Room, RoomDto> RoomFunc = (room) =>
            new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Color = room.Color,
            };
    }
}
