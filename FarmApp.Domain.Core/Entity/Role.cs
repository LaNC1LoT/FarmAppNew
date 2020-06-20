using System.Collections.Generic;

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
        public bool? IsDeleted { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ApiMethodRole> ApiMethodRoles { get; set; }
    }
}
