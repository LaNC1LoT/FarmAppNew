using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string UserName { get; set; } //first name + last name
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }
        public int RoleId { get; set; } = 2;
        public bool? IsDeleted { get; set; } = false;
        public virtual Role Role { get; set; }
    }
}
