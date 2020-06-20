﻿namespace FarmAppServer.Models.Users
{
    public class UserFilterByRoleDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public bool IsDeleted { get; set; }
        public UserRoleDto Role { get; set; }
    }
}
