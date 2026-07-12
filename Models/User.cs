using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace ProductsAPI.Models
{
    public class User
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
