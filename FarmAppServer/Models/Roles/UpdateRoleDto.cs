using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models.Roles
{
    public class UpdateRoleDto
    {
        [Required] public string RoleName { get; set; }
    }
}
