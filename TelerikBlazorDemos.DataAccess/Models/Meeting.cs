using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class Meeting
    {
        public Meeting()
        {
            InverseRecurrence = new HashSet<Meeting>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Start { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime End { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Department { get; set; }
        public bool IsAllDay { get; set; }
        [StringLength(255)]
        public string RecurrenceRule { get; set; }
        [Column("RecurrenceID")]
        public int? RecurrenceId { get; set; }
        [StringLength(255)]
        public string RecurrenceExceptions { get; set; }
        [StringLength(255)]
        public string StartTimezone { get; set; }
        [StringLength(255)]
        public string EndTimezone { get; set; }

        [ForeignKey(nameof(RecurrenceId))]
        [InverseProperty(nameof(Meeting.InverseRecurrence))]
        public virtual Meeting Recurrence { get; set; }
        [InverseProperty(nameof(Meeting.Recurrence))]
        public virtual ICollection<Meeting> InverseRecurrence { get; set; }
    }
}
