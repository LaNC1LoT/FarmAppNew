using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models
{
    public class RegisterModelDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}