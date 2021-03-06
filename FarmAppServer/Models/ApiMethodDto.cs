﻿namespace FarmAppServer.Models
{
    public class ApiMethodDto
    {
        public int Id { get; set; }
        public string ApiMethodName { get; set; }
        public string Description { get; set; }
        public string PathUrl { get; set; }
        public string HttpMethod { get; set; }
        public bool? IsNeedAuthentication { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
