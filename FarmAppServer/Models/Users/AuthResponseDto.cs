namespace FarmAppServer.Models.Users
{
    public class AuthResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public UserRoleDto Role { get; set; }
    }
}
