using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class MeetingDto
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Department { get; set; }

        public bool IsAllDay { get; set; }

        public string RecurrenceRule { get; set; }

        public int? RecurrenceId { get; set; }

        public string RecurrenceExceptions { get; set; }

        public string StartTimezone { get; set; }

        public string EndTimezone { get; set; }

        public static Func<Meeting, MeetingDto> MeetingFunc = (meeting) =>
            new MeetingDto
            {
                Id = meeting.Id,
                Start = meeting.Start,
                End = meeting.End,
                Title = meeting.Title,
                Description = meeting.Description,
                Department = meeting.Department,
                IsAllDay = meeting.IsAllDay,
                RecurrenceRule = meeting.RecurrenceRule,
                RecurrenceId = meeting.RecurrenceId,
                RecurrenceExceptions = meeting.RecurrenceExceptions,
                StartTimezone = meeting.StartTimezone,
                EndTimezone = meeting.EndTimezone,
            };
    }
}
