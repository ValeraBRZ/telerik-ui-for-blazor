using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class EmployeeDirectoryDto
    {
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

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(255)]
        public string Position { get; set; }

        public static Func<EmployeeDirectory, EmployeeDirectoryDto> EmployeeDirectoryFunc = (employeeDirectory) =>
            new EmployeeDirectoryDto
            {
                EmployeeId = employeeDirectory.EmployeeId,
                FirstName = employeeDirectory.FirstName,
                LastName = employeeDirectory.LastName,
                ReportsTo = employeeDirectory.ReportsTo,
                Address = employeeDirectory.Address,
                City = employeeDirectory.City,
                Country = employeeDirectory.Country,
                Phone = employeeDirectory.Phone,
                Extension = employeeDirectory.Extension,
                BirthDate = employeeDirectory.BirthDate,
                HireDate = employeeDirectory.HireDate,
                Position = employeeDirectory.Position,
            };
    }
}
