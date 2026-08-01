using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.Resources;

namespace TelerikBlazorDemos.Models.Form
{
    public class LocalizedUser
    {
        [Display(ResourceType = typeof(TelerikDemoMessages), Name = "Form_FirstName")]
        [Required(ErrorMessageResourceName = "Form_FirstName_Error", ErrorMessageResourceType = typeof(TelerikDemoMessages))]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(TelerikDemoMessages), Name = "Form_MiddleName")]
        public string MiddleName { get; set; }

        [Display(ResourceType = typeof(TelerikDemoMessages), Name = "Form_LastName")]
        [Required(ErrorMessageResourceName = "Form_LastName_Error", ErrorMessageResourceType = typeof(TelerikDemoMessages))]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(TelerikDemoMessages), Name = "Form_Email")]
        [Required(ErrorMessageResourceName = "Form_Email_Error", ErrorMessageResourceType = typeof(TelerikDemoMessages))]
        public string Email { get; set; }

        [Display(ResourceType = typeof(TelerikDemoMessages), Name = "Form_Password")]
        [MinLength(8, ErrorMessageResourceName = "Form_Password_Error", ErrorMessageResourceType = typeof(TelerikDemoMessages))]
        [Required(ErrorMessageResourceName = "Form_Password_Error", ErrorMessageResourceType = typeof(TelerikDemoMessages))]
        public string Password { get; set; }

        [Display(ResourceType = typeof(TelerikDemoMessages), Name = "Form_BirthDate")]
        [Required(ErrorMessageResourceName = "Form_BirthDate_Error", ErrorMessageResourceType = typeof(TelerikDemoMessages))]
        public DateTime? BirthDate { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessageResourceName = "Form_AcceptTerms_Error", ErrorMessageResourceType = typeof(TelerikDemoMessages))]
        [Display(ResourceType = typeof(TelerikDemoMessages), Name = "Form_AcceptTerms")]
        public bool AcceptTerms { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string AddressLine { get; set; }

        public string AddressLine2 { get; set; }

        public LocalizedUser()
        {
        }
    }
}
