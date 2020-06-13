using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models
{
    public class AuthenticateModelDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}