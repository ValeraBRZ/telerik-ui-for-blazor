using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class GanttTaskDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public double PercentComplete { get; set; }

        public static Func<GanttTask, GanttTaskDto> GanttTaskFunc = (ganttTask) =>
            new GanttTaskDto
            {
                Id = ganttTask.Id,
                ParentId = ganttTask.ParentId,
                Title = ganttTask.Title,
                Start = ganttTask.Start,
                End = ganttTask.End,
                PercentComplete = ganttTask.PercentComplete,
            };
    }
}
