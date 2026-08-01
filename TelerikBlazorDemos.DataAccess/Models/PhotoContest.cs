using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    public partial class PhotoContest
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(250)]
        public string Title { get; set; }
        public int Participants { get; set; }
        [Column("PhotoID")]
        [StringLength(50)]
        public string PhotoId { get; set; }
    }
}
