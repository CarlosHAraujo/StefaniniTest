using System.ComponentModel.DataAnnotations;

namespace StefaniniTestProject.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O login é obrigatório.")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }
    }
}