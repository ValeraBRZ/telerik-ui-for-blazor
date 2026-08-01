using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class ManagerDto
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public static Func<Manager, ManagerDto> ManagerFunc = (manager) =>
            new ManagerDto
            {
                Id = manager.Id,
                Name = manager.Name,
                Color = manager.Color,
            };
    }
}
