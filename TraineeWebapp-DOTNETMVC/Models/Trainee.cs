using System;
using System.ComponentModel.DataAnnotations;

namespace TraineeWebapp_DOTNETMVC.Models
{
    public class Trainee
    {
        public int TraineeID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }

        [Required]
        public string Gender { get; set; }

        public byte[] Photo { get; set; }
    }
}
