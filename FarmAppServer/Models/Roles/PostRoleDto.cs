using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models.Roles
{
    public class PostRoleDto
    {
        [Required] public string RoleName { get; set; }
    }
}
