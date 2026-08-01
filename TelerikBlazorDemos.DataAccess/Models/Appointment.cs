using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            InverseRecurrence = new HashSet<Appointment>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Start { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column("ManagerID")]
        public int? ManagerId { get; set; }
        [Column("RoomID")]
        public int? RoomId { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; }
        [Column("RecurrenceID")]
        public int? RecurrenceId { get; set; }
        public string RecurrenceExceptions { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }

        [ForeignKey(nameof(RecurrenceId))]
        [InverseProperty(nameof(Appointment.InverseRecurrence))]
        public virtual Appointment Recurrence { get; set; }
        [InverseProperty(nameof(Appointment.Recurrence))]
        public virtual ICollection<Appointment> InverseRecurrence { get; set; }
    }
}
