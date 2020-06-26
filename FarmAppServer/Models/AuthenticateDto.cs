namespace FarmAppServer.Models
{
    public class AuthenticateDto
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public string PathUrl { get; set; }
        public string HttpMethod { get; set; }
    }
}
