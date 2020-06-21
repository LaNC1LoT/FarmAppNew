using System;

namespace FarmApp.Domain.Core.Entity
{
    public class Log
    {
        public int Id { get; set; }
        public Guid GroupLogId { get; set; }
        public string LogType { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public int? StatusCode { get; set; }
        public string PathUrl { get; set; }
        public string HttpMethod { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Exception { get; set; }
    }
}
