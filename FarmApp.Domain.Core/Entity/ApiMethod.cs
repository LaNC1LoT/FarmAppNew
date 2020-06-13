using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class ApiMethod
    {
        public ApiMethod()
        {
            ApiMethodRoles = new HashSet<ApiMethodRole>();
        }
        public int Id { get; set; }
        public string ApiMethodName { get; set; }
        public string Description { get; set; }
        public string StoredProcedureName { get; set; }
        public string PathUrl { get; set; }
        public string HttpMethod { get; set; }
        public bool? IsNotNullParam { get; set; }
        public bool? IsNeedAuthentication { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public virtual ICollection<ApiMethodRole> ApiMethodRoles { get; set; }
    }
}
