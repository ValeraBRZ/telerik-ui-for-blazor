using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TelerikBlazorDemos.DataAccess.Models
{
    [Table("EmployeeDirectory")]
    public partial class EmployeeDirectory
    {
        public EmployeeDirectory()
        {
            InverseReportsToNavigation = new HashSet<EmployeeDirectory>();
        }

        [Key]
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        public int? ReportsTo { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string City { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }
        public int? Extension { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BirthDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HireDate { get; set; }
        [StringLength(255)]
        public string Position { get; set; }

        [ForeignKey(nameof(ReportsTo))]
        [InverseProperty(nameof(EmployeeDirectory.InverseReportsToNavigation))]
        public virtual EmployeeDirectory ReportsToNavigation { get; set; }
        [InverseProperty(nameof(EmployeeDirectory.ReportsToNavigation))]
        public virtual ICollection<EmployeeDirectory> InverseReportsToNavigation { get; set; }
    }
}
