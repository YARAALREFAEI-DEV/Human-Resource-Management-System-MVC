using System.ComponentModel.DataAnnotations;

namespace HRMS_FinalProject.Models.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Enter Email Address")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
