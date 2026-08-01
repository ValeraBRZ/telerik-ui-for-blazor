using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class SchedulerTask
    {
        public SchedulerTask()
        {
            InverseRecurrence = new HashSet<SchedulerTask>();
        }

        [Key]
        [Column("TaskID")]
        public int TaskId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Start { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime End { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Title { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        [Column("OwnerID")]
        public int? OwnerId { get; set; }
        public bool IsAllDay { get; set; }
        [Column(TypeName = "ntext")]
        public string RecurrenceRule { get; set; }
        [Column("RecurrenceID")]
        public int? RecurrenceId { get; set; }
        [Column(TypeName = "ntext")]
        public string RecurrenceException { get; set; }
        [Column(TypeName = "ntext")]
        public string StartTimezone { get; set; }
        [Column(TypeName = "ntext")]
        public string EndTimezone { get; set; }

        [ForeignKey(nameof(RecurrenceId))]
        [InverseProperty(nameof(SchedulerTask.InverseRecurrence))]
        public virtual SchedulerTask Recurrence { get; set; }
        [InverseProperty(nameof(SchedulerTask.Recurrence))]
        public virtual ICollection<SchedulerTask> InverseRecurrence { get; set; }
    }
}
