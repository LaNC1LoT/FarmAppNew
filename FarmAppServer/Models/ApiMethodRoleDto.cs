namespace FarmAppServer.Models
{
    public class ApiMethodRoleDto
    {
        public int Id { get; set; }
        public int ApiMethodId { get; set; }
        public string ApiMethodName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
