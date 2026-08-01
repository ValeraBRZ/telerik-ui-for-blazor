using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class AppointmentService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;
        private List<AppointmentDto> _appointments;

        public AppointmentService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<AppointmentDto> GetAppointments()
        {
            return GetAppointmentsInternal();
        }

        public void CreateAppointment(AppointmentDto appointment)
        {
            if (!_appointments.Any())
            {
                appointment.Id = 1;
            }
            else
            {
                appointment.Id = _appointments.Max(p => p.Id) + 1;
            }

            _appointments.Insert(0, appointment);
        }

        public void UpdateAppointment(AppointmentDto appointment)
        {
            var target = _appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (target != null)
            {
                target.Title = appointment.Title;
                target.Description = appointment.Description;
                target.Start = appointment.Start;
                target.End = appointment.End;
                target.IsAllDay = appointment.IsAllDay;
                target.RecurrenceExceptions = appointment.RecurrenceExceptions;
                target.RecurrenceRule = appointment.RecurrenceRule;
                target.ManagerId = appointment.ManagerId;
                target.RoomId = appointment.RoomId;
                target.StartTimezone = appointment.StartTimezone;
                target.EndTimezone = appointment.EndTimezone;
            }
        }

        public void DeleteAppointment(AppointmentDto appointment)
        {
            var target = _appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (target != null)
            {
                _appointments.Remove(target);
            }
        }

        private IEnumerable<AppointmentDto> GetAppointmentsInternal()
        {
            if (_appointments == null)
            {
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                using var dbContext = _contextFactory.CreateDbContext();
                _appointments = dbContext.Appointments.ToList().Select(AppointmentDto.AppointmentFunc).ToList();
            }

            return _appointments;
        }
    }
}
