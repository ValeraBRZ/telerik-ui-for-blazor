using System;
using System.ComponentModel.DataAnnotations;

namespace TelerikBlazorDemos.Models.Form
{
    public class Application
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int? Hours { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public Position? Position { get; set; }

        public bool LettersConfirmation { get; set; }
    }

    public enum Position
    {
        Sales,
        Accounting,
        Marketing,
        Engineering
    }
}
