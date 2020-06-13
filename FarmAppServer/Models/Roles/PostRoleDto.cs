using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.Roles
{
    public class PostRoleDto
    {
        [Required] public string RoleName { get; set; }
    }
}
