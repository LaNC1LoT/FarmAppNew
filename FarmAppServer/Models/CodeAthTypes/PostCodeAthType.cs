using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.CodeAthTypes
{
    public class PostCodeAthType
    {
        [Required] public int ParentId { get; set; }//parent id
        [Required] public string Code { get; set; }//code //A01
        [Required] public string NameAth { get; set; }//стоматологические препараты
    }
}
