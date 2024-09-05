using System.ComponentModel.DataAnnotations;

namespace dotnetdev_assessment.Models.FormModels
{
    public class EditEmployee
    {
        [Required]
        public required int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Username { get; set; }

        [Required]
        public required bool IsAdmin { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string Department { get; set; }

        [Required]
        [StringLength(100)]
        public required string Position { get; set; }
    }
}
