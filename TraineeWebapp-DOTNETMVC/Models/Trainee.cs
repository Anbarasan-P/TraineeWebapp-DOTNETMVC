using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TraineeWebapp_DOTNETMVC.Models
{
    public class Trainee
    {
        public int TraineeID { get; set; }  
        public string Name { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,MaxLength(10),MinLength(10)]
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