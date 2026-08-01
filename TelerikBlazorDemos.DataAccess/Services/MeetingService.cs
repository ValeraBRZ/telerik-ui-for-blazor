using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class MeetingService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<MeetingDto> _meetings;

        public MeetingService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<MeetingDto> GetMeetings()
        {
            return GetMeetingsInternal();
        }

        public void CreateMeeting(MeetingDto meeting)
        {
            if (!_meetings.Any())
            {
                meeting.Id = 1;
            }
            else
            {
                meeting.Id = _meetings.Max(p => p.Id) + 1;
            }

            _meetings.Insert(0, meeting);
        }

        public void UpdateMeeting(MeetingDto meeting)
        {
            var target = _meetings.FirstOrDefault(a => a.Id == meeting.Id);
            if (target != null)
            {
                target.Title = meeting.Title;
                target.Description = meeting.Description;
                target.Start = meeting.Start;
                target.End = meeting.End;
                target.IsAllDay = meeting.IsAllDay;
                target.RecurrenceExceptions = meeting.RecurrenceExceptions;
                target.RecurrenceRule = meeting.RecurrenceRule;
                target.Department = meeting.Department;
                target.StartTimezone = meeting.StartTimezone;
                target.EndTimezone = meeting.EndTimezone;
            }
        }

        public void DeleteMeeting(MeetingDto meeting)
        {
            var target = _meetings.FirstOrDefault(a => a.Id == meeting.Id);
            if (target != null)
            {
                _meetings.Remove(target);
            }
        }

        private IEnumerable<MeetingDto> GetMeetingsInternal()
        {
            if (_meetings == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                _meetings = dbContext.Meetings.ToList().Select(MeetingDto.MeetingFunc).ToList();
            }

            return _meetings;
        }
    }
}
