using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    /// <summary>
    /// Роли
    /// </summary>
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
            ApiMethodRoles = new HashSet<ApiMethodRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ApiMethodRole> ApiMethodRoles { get; set; }
    }
}
