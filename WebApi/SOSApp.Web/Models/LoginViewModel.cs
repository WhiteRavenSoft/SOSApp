using System.ComponentModel.DataAnnotations;

namespace SOSApp.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}