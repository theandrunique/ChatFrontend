using System.ComponentModel.DataAnnotations;

namespace ChatFrontend.Models
{
    public class LogInModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public LogInModel()
        {
            Login = string.Empty;
            Password = string.Empty;
        }
    }
}
