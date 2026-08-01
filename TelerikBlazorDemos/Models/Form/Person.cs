using System;
using System.ComponentModel.DataAnnotations;

namespace TelerikBlazorDemos.Models.Form
{
    public class Person
    {
        public Person()
        {
            HireDate = MeetingDate = DateTime.Now;
        }

        [Editable(false)]
        [Display(Name = "Employee ID")]
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "Enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter an int age")]
        [Range(0, 200, ErrorMessage = "Nobody is that old")]
        [Display(Name = "Age")]
        public int? AgeInYears { get; set; }

        [Required(ErrorMessage = "Enter a graduate grade.")]
        [Range(3, 6, ErrorMessage = "Grades vary between 3 and 6.")]
        [Display(Name = "Graduate Grade")]
        public decimal? GraduateGrade { get; set; }

        [Required(ErrorMessage = "Enter a hire date")]
        [Range(typeof(DateTime), "10/10/2016", "10/10/2020", ErrorMessage = "Hire Date must be between 10/10/2016 and 10/10/2020")]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Enter interview meeting")]
        [Range(typeof(DateTime), "03/03/2019", "03/10/2019", ErrorMessage = "Interview meeting should be between 03/03/2019 and 03/10/2019")]
        [Display(Name = "Meeting Date")]
        public DateTime MeetingDate { get; set; }
    }
}
