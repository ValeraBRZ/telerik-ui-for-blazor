using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    [Table("Intraday")]
    public partial class Intraday
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Symbol { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal Open { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal High { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal Low { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal Close { get; set; }
        public long Volume { get; set; }
    }
}
