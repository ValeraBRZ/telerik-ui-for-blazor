using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class GanttTask
    {
        public GanttTask()
        {
            InverseParent = new HashSet<GanttTask>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("ParentID")]
        public int? ParentId { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Start { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime End { get; set; }
        public double PercentComplete { get; set; }

        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(GanttTask.InverseParent))]
        public virtual GanttTask Parent { get; set; }
        [InverseProperty(nameof(GanttTask.Parent))]
        public virtual ICollection<GanttTask> InverseParent { get; set; }
    }
}
