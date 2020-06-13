using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Log
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string HttpMethod { get; set; }
        public string PathUrl { get; set; }
        public string MethodRoute { get; set; }
        public string HeaderRequest { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? FactTime { get; set; }
        public string Param { get; set; }
        public int? StatusCode { get; set; }
        public string HeaderResponse { get; set; }
        public Guid? ResponseId { get; set; }
        public DateTime? ResponseTime { get; set; }
        public string Header { get; set; }
        public string Result { get; set; }
        public string Exception { get; set; }
    }
}
