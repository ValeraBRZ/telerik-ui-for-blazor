using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    class GanttResourceDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Color { get; set; }


        public static Func<GanttResource, GanttResourceDto> GanttResourceFunc = (ganttResource) =>
            new GanttResourceDto
            {
                Id = ganttResource.Id,
                Name = ganttResource.Name,
                Color = ganttResource.Color,
            };
    }
}
