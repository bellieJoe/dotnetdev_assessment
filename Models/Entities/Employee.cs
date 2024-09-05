using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetdev_assessment.Models.Entities
{
    public class Employee
    {
        [Required]
        public required int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public required string Username { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public required string PasswordHash { get; set; }

        [Required]
        public required bool IsAdmin { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public required string Department { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public required string Position { get; set; }

    }
}
