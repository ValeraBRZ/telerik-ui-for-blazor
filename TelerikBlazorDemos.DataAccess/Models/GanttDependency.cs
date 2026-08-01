using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class GanttDependency
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("PredecessorID")]
        public int PredecessorId { get; set; }
        [Column("SuccessorID")]
        public int SuccessorId { get; set; }
        public int Type { get; set; }
    }
}
