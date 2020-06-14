using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.CodeAthTypes
{
    public class PostCodeAthType
    {
        public int ParentCodeAthId { get; set; }//parent id
        public string Code { get; set; }//code //A01
        public string NameAth { get; set; }//стоматологические препараты
    }
}
