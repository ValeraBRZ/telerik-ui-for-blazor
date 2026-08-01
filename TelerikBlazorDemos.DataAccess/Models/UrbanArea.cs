using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class UrbanArea
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string City { get; set; }
        [Required]
        [StringLength(256)]
        public string Country { get; set; }
        [Column(TypeName = "decimal(9, 6)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(9, 6)")]
        public decimal Longitude { get; set; }
        [Required]
        [Column("Country_ISO3")]
        [StringLength(3)]
        public string CountryIso3 { get; set; }
        public int Pop1950 { get; set; }
        public int Pop1955 { get; set; }
        public int Pop1960 { get; set; }
        public int Pop1965 { get; set; }
        public int Pop1970 { get; set; }
        public int Pop1975 { get; set; }
        public int Pop1980 { get; set; }
        public int Pop1985 { get; set; }
        public int Pop1990 { get; set; }
        public int Pop1995 { get; set; }
        public int Pop2000 { get; set; }
        public int Pop2005 { get; set; }
        public int Pop2010 { get; set; }
        public int Pop2015 { get; set; }
        public int Pop2020 { get; set; }
        public int Pop2025 { get; set; }
        public int Pop2050 { get; set; }
    }
}
