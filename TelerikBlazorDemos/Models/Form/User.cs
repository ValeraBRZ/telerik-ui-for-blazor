using System;
using System.ComponentModel.DataAnnotations;

namespace TelerikBlazorDemos.Models.Form
{
    public class User
    {
        [Display(Name = "First Name*:")]
        [Required(ErrorMessage = "You must specify your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name:")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name*:")]
        [Required(ErrorMessage = "You must specify your last name.")]
        public string LastName { get; set; }

        [Display(Name = "Email*:")]
        [Required(ErrorMessage = "You must specify email.")]
        public string Email { get; set; }

        [Display(Name = "Password*:")]
        [MinLength(8, ErrorMessage = "Your password should be at least 8 characters.")]
        [Required(ErrorMessage = "Your password should be at least 8 characters.")]
        public string Password { get; set; }

        [Display(Name = "Birth Date*:")]
        [Required(ErrorMessage = "You must specify birth date.")]
        public DateTime? BirthDate { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree with the terms.")]
        [Display(Name = "Agree to Terms*:")]
        public bool AcceptTerms { get; set; }

        [Display(Name = "Country*:")]
        [Required]
        public string Country { get; set; }

        [Display(Name = "City*:")]
        [Required]
        public string City { get; set; }

        [Display(Name = "Address Line*:")]
        [Required]
        public string AddressLine { get; set; }

        [Display(Name = "Address Line 2:")]
        public string AddressLine2 { get; set; }

        public User()
        {
        }
    }
}
