using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    [Table("Weather")]
    public partial class Weather
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Station { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("TMax", TypeName = "decimal(5, 2)")]
        public decimal Tmax { get; set; }
        [Column("TMin", TypeName = "decimal(5, 2)")]
        public decimal Tmin { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Wind { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Gust { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Rain { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Snow { get; set; }
        [StringLength(255)]
        public string Events { get; set; }
    }
}
