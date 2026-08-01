using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class SchedulerTaskDto
    {
        public int TaskId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? OwnerId { get; set; }

        public bool IsAllDay { get; set; }

        public string RecurrenceRule { get; set; }

        public int? RecurrenceId { get; set; }

        public string RecurrenceException { get; set; }

        public string StartTimezone { get; set; }

        public string EndTimezone { get; set; }

        public static Func<SchedulerTask, SchedulerTaskDto> SchedulerTaskFunc = (schedulerTask) =>
            new SchedulerTaskDto
            {
                TaskId = schedulerTask.TaskId,
                Start = schedulerTask.Start,
                End = schedulerTask.End,
                Title = schedulerTask.Title,
                Description = schedulerTask.Description,
                OwnerId = schedulerTask.OwnerId,
                IsAllDay = schedulerTask.IsAllDay,
                RecurrenceRule = schedulerTask.RecurrenceRule,
                RecurrenceId = schedulerTask.RecurrenceId,
                RecurrenceException = schedulerTask.RecurrenceException,
                StartTimezone = schedulerTask.StartTimezone,
                EndTimezone = schedulerTask.EndTimezone,
            };
    }
}
