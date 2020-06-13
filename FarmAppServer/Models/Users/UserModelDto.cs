using FarmApp.Domain.Core.Entity;
using FarmAppServer.Models.Users;

namespace FarmAppServer.Models
{
    public class UserModelDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public UserRoleDto Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}