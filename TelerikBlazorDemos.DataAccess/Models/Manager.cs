using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class Manager
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
    }
}
