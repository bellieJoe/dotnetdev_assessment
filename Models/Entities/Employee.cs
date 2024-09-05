using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetdev_assessment.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public required string Username { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public required string PasswordHash { get; set; }

        public required bool IsAdmin { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public required string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public required string Department { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public required string Position { get; set; }

    }
}
