using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
