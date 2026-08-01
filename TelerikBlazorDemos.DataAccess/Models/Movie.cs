using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class Movie
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(250)]
        public string Title { get; set; }
        public int? Position { get; set; }
        public double? Rating { get; set; }
        [StringLength(500)]
        public string Summary { get; set; }
    }
}
