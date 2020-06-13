using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.CodeAthTypes
{
    public class CodeAthTypeDto
    {
        public int  Id { get; set; }
        public int ParentCodeAthId { get; set; }//parent id
        public string ParentCodeName { get; set; }//paretn code // A
        public string Code { get; set; }//code //A01
        public string NameAth { get; set; }//стоматологические препараты
        public bool IsDeleted { get; set; }
    }
}
