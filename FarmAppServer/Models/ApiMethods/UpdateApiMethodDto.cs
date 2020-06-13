using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.ApiMethods
{
    public class UpdateApiMethodDto
    {
        [Required] public string ApiMethodName { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string StoredProcedureName { get; set; }
        [Required] public string PathUrl { get; set; }
        [Required] public string HttpMethod { get; set; }
        [Required] public bool IsNotNullParam { get; set; }
        [Required] public bool IsNeedAuthentication { get; set; }
    }
}
