using System.ComponentModel.DataAnnotations;

namespace yeaa2.Models
{
    public class UserRegistrationModel
    {
        [Required] [StringLength(50)] public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
    }
    
}