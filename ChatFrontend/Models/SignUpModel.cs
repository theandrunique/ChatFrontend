using System.ComponentModel.DataAnnotations;

namespace ChatFrontend.Models
{
    public class SignUpModel
    {
        private const int UsernameMinLength = 3;
        private const int UsernameMaxLength = 32;
        private const string UsernamePattern = @"^[a-zA-Z0-9_]+$";

        private const int PasswordMinLength = 8;
        private const int PasswordMaxLength = 32;
        private const string PasswordPattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        [Required(ErrorMessage = "Username is required")]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = "Username must be between 3 and 32 characters")]
        [RegularExpression(UsernamePattern, ErrorMessage = "Username can only contain letters, numbers, and underscores")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = "Password must be between 8 and 32 characters")]
        [RegularExpression(PasswordPattern, ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        public string Password { get; set; }
        public SignUpModel()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
