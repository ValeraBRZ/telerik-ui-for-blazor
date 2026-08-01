using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class GanttResourceAssignment
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("TaskID")]
        public int TaskId { get; set; }
        [Column("ResourceID")]
        public int ResourceId { get; set; }
        public double Units { get; set; }
    }
}
