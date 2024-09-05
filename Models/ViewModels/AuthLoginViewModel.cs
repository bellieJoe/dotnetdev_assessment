
using System.ComponentModel.DataAnnotations;

namespace dotnetdev_assessment.Models.ViewModels
{
    public class AuthLoginViewModel
    {
        [Required]
        [StringLength(100)]
        public required string Username { get; set; }    

        [Required]
        [StringLength(100)]
        public required string Password { get; set; }
    }
}
