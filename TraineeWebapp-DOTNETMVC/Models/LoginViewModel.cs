using System.ComponentModel.DataAnnotations; // This line is necessary for data annotations like [Required] and [DataType]

namespace TraineeWebapp_DOTNETMVC.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required] // If I remove the system component model data annotations, it will not work and throw compile time error
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
